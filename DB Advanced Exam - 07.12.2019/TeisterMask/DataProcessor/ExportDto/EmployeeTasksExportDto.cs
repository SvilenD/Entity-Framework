namespace TeisterMask.DataProcessor.ExportDto
{
    using Newtonsoft.Json;

    public class EmployeeTasksExportDto
    {
        public string TaskName { get; set; }

        public string OpenDate { get; set; }

        public string DueDate { get; set; }

        public string LabelType { get; set; }

        public string ExecutionType { get; set; }
    }
}