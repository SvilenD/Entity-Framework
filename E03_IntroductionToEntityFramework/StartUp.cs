﻿namespace SoftUni
{
    using SoftUni.Data;
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Text;

    public class StartUp
    {
        public static void Main()
        {
            var db = new SoftUniContext();

            var result = GetDepartmentsWithMoreThan5Employees(db);

            db.Dispose();

            Console.WriteLine(result);
        }

        //Task3
        public static string GetEmployeesFullInformation(SoftUniContext context)
        {
            StringBuilder result = new StringBuilder();

            var employees = context.Employees.
                Select(e => new
                {
                    e.EmployeeId,
                    e.FirstName,
                    e.LastName,
                    e.MiddleName,
                    e.JobTitle,
                    e.Salary
                })
                .OrderBy(e => e.EmployeeId);

            foreach (var employee in employees)
            {
                result.AppendLine($"{employee.FirstName} {employee.LastName} {employee.MiddleName} {employee.JobTitle} {employee.Salary:F2}");
            }

            return result.ToString().TrimEnd();
        }

        //Task4
        public static string GetEmployeesWithSalaryOver50000(SoftUniContext context)
        {
            StringBuilder result = new StringBuilder();

            var employees = context.Employees.
                Select(e => new
                {
                    e.FirstName,
                    e.Salary
                })
                .Where(e => e.Salary > 50000)
                .OrderBy(e => e.FirstName);

            foreach (var employee in employees)
            {
                result.AppendLine($"{employee.FirstName} - {employee.Salary:F2}");
            }

            return result.ToString().TrimEnd();
        }

        //Task5
        public static string GetEmployeesFromResearchAndDevelopment(SoftUniContext context)
        {
            StringBuilder result = new StringBuilder();

            var employees = context.Employees.
                Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    Department = e.Department.Name,
                    e.Salary
                })
                .Where(e => e.Department == "Research and Development")
                .OrderBy(e => e.Salary)
                .ThenByDescending(e => e.FirstName);

            foreach (var employee in employees)
            {
                result.AppendLine($"{employee.FirstName} {employee.LastName} from {employee.Department} - ${employee.Salary:F2}");
            }

            return result.ToString().TrimEnd();
        }

        //Task6
        public static string AddNewAddressToEmployee(SoftUniContext context)
        {
            StringBuilder result = new StringBuilder();

            var newAddress = new Models.Address
            {
                AddressText = "Vitoshka 15",
                TownId = 14
            };
            context.Addresses.Add(newAddress);

            var employeeToUpdate = context.Employees.
                FirstOrDefault(e => e.LastName == "Nakov");

            employeeToUpdate.Address = newAddress;

            context.SaveChanges();

            var addresses = context.Addresses
                .OrderByDescending(a => a.AddressId)
                .Select(a => a.AddressText)
                .Take(10);

            foreach (var address in addresses)
            {
                result.AppendLine(address);
            }

            return result.ToString().TrimEnd();
        }

        //Task7
        public static string GetEmployeesInPeriod(SoftUniContext context)
        {
            StringBuilder result = new StringBuilder();

            var employees = context.Employees
                .Where(e => e.EmployeesProjects
                                .Any(ep => ep.Project.StartDate.Year >= 2001 && ep.Project.StartDate.Year <= 2003))
                .Select(e => new
                {
                    EmployeeName = e.FirstName + " " + e.LastName,
                    ManagerName = e.Manager.FirstName + " " + e.Manager.LastName,
                    Projects = e.EmployeesProjects.Select(p => new
                    {
                        p.Project.Name,
                        p.Project.StartDate,
                        p.Project.EndDate
                    })
                })
                .Take(10);

            foreach (var emp in employees)
            {
                result.AppendLine($"{emp.EmployeeName} - Manager: {emp.ManagerName}");

                foreach (var project in emp.Projects)
                {
                    var start = project.StartDate.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);
                    var end = project.EndDate.HasValue
                        ? project.EndDate.Value.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture)
                        : "not finished";
                    var name = project.Name;

                    result.AppendLine($"--{name} - {start} - {end}");
                }
            }
            return result.ToString().TrimEnd();
        }

        //Task8
        public static string GetAddressesByTown(SoftUniContext context)
        {
            StringBuilder result = new StringBuilder();

            var addresses = context.Addresses
                .Select(a => new
                {
                    a.AddressText,
                    TownName = a.Town.Name,
                    EmployeeCount = a.Employees.Count
                })
                .OrderByDescending(a => a.EmployeeCount)
                .ThenBy(a => a.TownName)
                .ThenBy(a => a.AddressText)
                .Take(10);

            foreach (var ad in addresses)
            {
                result.AppendLine($"{ad.AddressText}, {ad.TownName} - {ad.EmployeeCount} employees");
            }

            return result.ToString().TrimEnd();
        }

        //Task9
        public static string GetEmployee147(SoftUniContext context)
        {
            StringBuilder result = new StringBuilder();

            var employee147 = context
                .Employees
                .Where(e => e.EmployeeId == 147)
                .Select(e => new
                {

                    e.FirstName,
                    e.LastName,
                    e.JobTitle,
                    Projects = e.EmployeesProjects
                                .Select(p => p.Project.Name)
                                .OrderBy(p => p)
                })
                .FirstOrDefault();

            result.AppendLine($"{employee147.FirstName} {employee147.LastName} - {employee147.JobTitle}");

            foreach (var project in employee147.Projects)
            {
                result.AppendLine(project);
            }

            return result.ToString().TrimEnd();
        }

        //Task10
        public static string GetDepartmentsWithMoreThan5Employees(SoftUniContext context)
        {
            var departments = context
                .Departments
                .Where(d => d.Employees.Count > 5)
                .Select(d => new
                {
                    DepartmentName = d.Name,
                    ManagerName = d.Manager.FirstName + " " + d.Manager.LastName,
                    Employees = d.Employees
                                    .OrderBy(e => e.FirstName)
                                    .ThenBy(e => e.LastName)
                                    .Select(e => new
                                    {
                                        Name = e.FirstName + " " + e.LastName,
                                        e.JobTitle
                                    })
                })
                .OrderBy(d => d.Employees.Count())
                .ThenBy(d => d.DepartmentName);

            StringBuilder result = new StringBuilder();

            foreach (var dep in departments)
            {
                result.AppendLine($"{dep.DepartmentName} - {dep.ManagerName}");

                foreach (var emp in dep.Employees)
                {
                    result.AppendLine($"{emp.Name} - {emp.JobTitle}");
                }
            }

            return result.ToString().TrimEnd();
        }
    }
}