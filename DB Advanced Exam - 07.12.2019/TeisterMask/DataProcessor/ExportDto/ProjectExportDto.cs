using System.Xml.Serialization;

namespace TeisterMask.DataProcessor.ExportDto
{
    [XmlType("Project")]
    public class ProjectExportDto
    {
        [XmlAttribute("TasksCount")]
        public int Count { get; set; }

        public string ProjectName { get; set; }

        public string HasEndDate { get; set; }

        [XmlArray("")]
        public ProjectTasksExportDto[] Tasks { get; set;}
    }
}
