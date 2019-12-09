namespace TeisterMask.DataProcessor
{
    using System;
    using System.IO;
    using System.Text;
    using System.Linq;
    using System.Globalization;
    using System.Xml.Serialization;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Newtonsoft.Json;

    using Data;
    using TeisterMask.Data.Models;
    using TeisterMask.Data.Models.Enums;
    using TeisterMask.DataProcessor.ImportDto;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfullyImportedProject
            = "Successfully imported project - {0} with {1} tasks.";

        private const string SuccessfullyImportedEmployee
            = "Successfully imported employee - {0} with {1} tasks.";

        public static string ImportProjects(TeisterMaskContext context, string xmlString)
        {
            var attr = new XmlRootAttribute("Projects");
            var serializer = new XmlSerializer(typeof(ProjectImportDto[]), attr);

            var projectDtos = (ProjectImportDto[])serializer.Deserialize(new StringReader(xmlString));

            var result = new StringBuilder();
            var projectsList = new List<Project>();

            foreach (var dto in projectDtos)
            {
                if (IsValid(dto) == false)
                {
                    result.AppendLine(ErrorMessage);
                    continue;
                }

                var project = new Project
                {
                    Name = dto.Name,
                    OpenDate = DateTime.ParseExact(dto.OpenDate, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                };

                if (string.IsNullOrEmpty(dto.DueDate) == false)
                {
                    project.DueDate = DateTime.ParseExact(dto.DueDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                }

                foreach (var taskDto in dto.Tasks)
                {
                    try
                    {
                        var taskDtoOpenDate = DateTime.ParseExact(taskDto.OpenDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        var taskDtoDueDate = DateTime.ParseExact(taskDto.DueDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                        if (IsValid(taskDto) == false || taskDtoOpenDate < project.OpenDate || taskDtoDueDate > project.DueDate)
                        {
                            result.AppendLine(ErrorMessage);
                            continue;
                        }

                        var taskToAdd = new Task
                        {
                            Name = taskDto.Name,
                            OpenDate = taskDtoOpenDate,
                            DueDate = taskDtoDueDate,
                            ExecutionType = Enum.Parse<ExecutionType>(taskDto.ExecutionType),
                            LabelType = Enum.Parse<LabelType>(taskDto.LabelType),
                            Project = project
                        };

                        project.Tasks.Add(taskToAdd);
                    }
                    catch (Exception)
                    {
                        result.AppendLine(ErrorMessage);
                        continue;
                    }
                    
                }

                projectsList.Add(project);

                result.AppendLine(string.Format(SuccessfullyImportedProject, project.Name, project.Tasks.Count));
            }

            context.Projects.AddRange(projectsList);
            context.SaveChanges();

            return result.ToString().TrimEnd();
        }

        public static string ImportEmployees(TeisterMaskContext context, string jsonString)
        {
            var employeeDtos = JsonConvert.DeserializeObject<EmployeeImportDto[]>(jsonString);
            var result = new StringBuilder();
            var employeeList = new List<Employee>();
            var tasksIds = context.Tasks
                .Select(t => t.Id).ToList();

            foreach (var dto in employeeDtos)
            {
                if (IsValid(dto) == false)
                {
                    result.AppendLine(ErrorMessage);
                    continue;
                }

                var employee = new Employee
                {
                    Username = dto.Username,
                    Email = dto.Email,
                    Phone = dto.Phone
                };

                foreach (var taskDto in dto.Tasks.Distinct())
                {
                    if (tasksIds.Contains(taskDto) == false || employee.EmployeesTasks.Any(t => t.TaskId == taskDto))
                    {
                        result.AppendLine(ErrorMessage);
                        continue;
                    }
                    employee.EmployeesTasks.Add(new EmployeeTask
                    {
                        TaskId = taskDto
                    });
                }

                employeeList.Add(employee);
                result.AppendLine(String.Format(SuccessfullyImportedEmployee, employee.Username, employee.EmployeesTasks.Count));
            }

            context.Employees.AddRange(employeeList);
            context.SaveChanges();

            return result.ToString().TrimEnd();
        }

        private static bool IsValid(object entity)
        {
            var validationContext = new ValidationContext(entity);
            var validationResult = new List<ValidationResult>();

            var result = Validator.TryValidateObject(entity, validationContext, validationResult, true);

            return result;
        }
    }
}