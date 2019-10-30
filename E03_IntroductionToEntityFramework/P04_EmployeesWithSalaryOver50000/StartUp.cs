namespace SoftUni
{
    using SoftUni.Data;
    using System;
    using System.Linq;
    using System.Text;

    public class StartUp
    {
        public static void Main()
        {
            var db = new SoftUniContext();

            var result = GetEmployeesWithSalaryOver50000(db);

            Console.WriteLine(result);
        }

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
    }
}