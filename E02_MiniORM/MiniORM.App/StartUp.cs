namespace MiniORM.App
{
    using System.Linq;
    using MiniORM.App.Data;
    using MiniORM.App.Data.Entities;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            var connectionString = @"Server=.;Database=MiniORM;Integrated Security=True";

            var context = new AppDbContext(connectionString);

            context.Employees.Add(new Employee
            {
                FirstName = "Boris3",
                LastName = "Inserted",
                DepartmentId = context.Departments.First().Id,
                IsEmployed = true,
            });

            var employee = context.Employees.Last();
            employee.FirstName = "Modified";

            context.SaveChanges();
        }
    }
}