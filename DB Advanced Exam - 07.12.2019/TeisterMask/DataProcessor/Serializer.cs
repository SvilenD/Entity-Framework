namespace TeisterMask.DataProcessor
{
    using System;
    using System.IO;
    using System.Text;
    using System.Linq;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Globalization;
    using Newtonsoft.Json;

    using Data;
    using TeisterMask.DataProcessor.ExportDto;

    public class Serializer
    {
        public static string ExportProjectWithTheirTasks(TeisterMaskContext context)
        {
            var projects = context.Projects
                .Where(p => p.Tasks.Any())
                .Select(p => new ProjectExportDto
                {
                    Count = p.Tasks.Count,
                    ProjectName = p.Name,
                    HasEndDate = p.DueDate != null ? "Yes" : "No",
                    Tasks = p.Tasks.Select(t => new ProjectTasksExportDto
                    {
                        Name = t.Name,
                        Label = t.LabelType.ToString()
                    })
                    .OrderBy(t => t.Name)
                    .ToArray()
                })
                .OrderByDescending(t => t.Count)
                .ThenBy(t => t.ProjectName)
                .ToArray();

            var xmlSerializer = new XmlSerializer(projects.GetType(), new XmlRootAttribute("Projects"));

            var result = new StringBuilder();

            var namespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });

            xmlSerializer.Serialize(new StringWriter(result), projects, namespaces);
            return result.ToString().Trim();
        }

        public static string ExportMostBusiestEmployees(TeisterMaskContext context, DateTime date)
        {
            var top10Employees = context.Employees
                .Where(e => e.EmployeesTasks.Any(t => t.Task.OpenDate >= date))
                .OrderByDescending(e => e.EmployeesTasks.Count(t => t.Task.OpenDate >= date))
                .ThenBy(e => e.Username)
                .Select(e => new EmployeeExportDto
                {
                    Username = e.Username,
                    Tasks = e.EmployeesTasks.Where(t => t.Task.OpenDate >= date)
                        .Select(t => new EmployeeTasksExportDto
                        {
                            TaskName = t.Task.Name,
                            OpenDate = t.Task.OpenDate.ToString("d", CultureInfo.InvariantCulture),
                            DueDate = t.Task.DueDate.ToString("d", CultureInfo.InvariantCulture),
                            LabelType = t.Task.LabelType.ToString(),
                            ExecutionType = t.Task.ExecutionType.ToString(),
                        })
                        .OrderByDescending(t => DateTime.ParseExact(t.DueDate, "d", CultureInfo.InvariantCulture))
                        .ThenBy(t => t.TaskName)
                        .ToArray()
                })
                .Take(10)
                .ToList();
            
            var result = JsonConvert.SerializeObject(top10Employees, Newtonsoft.Json.Formatting.Indented);
                        return result;
        }
    }
}