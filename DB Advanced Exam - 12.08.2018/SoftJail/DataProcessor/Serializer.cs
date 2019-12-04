namespace SoftJail.DataProcessor
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;
    using Data;
    using Newtonsoft.Json;
    using SoftJail.DataProcessor.ExportDto;

    public class Serializer
    {
        public static string ExportPrisonersByCells(SoftJailDbContext context, int[] ids)
        {
            var prisoners = context.Prisoners
                .Where(p => ids.Any(i => i == p.Id))
                .OrderBy(p => p.FullName)
                .Select(p => new PrisonerExportDto
                {
                    Id = p.Id,
                    Fullname = p.FullName,
                    CellNumber = p.Cell.CellNumber,
                    Officers = p.PrisonerOfficers.Select(po => new OfficerExportDto
                    {
                        FullName = po.Officer.FullName,
                        Department = po.Officer.Department.Name,
                        Salary = po.Officer.Salary
                    })
                    .OrderBy(o => o.FullName)
                    .ToArray()
                })
                .ToArray();

            var prisonersJson = JsonConvert.SerializeObject(prisoners, Newtonsoft.Json.Formatting.Indented);

            return prisonersJson;
        }

        public static string ExportPrisonersInbox(SoftJailDbContext context, string prisonersNames)
        {
            var names = prisonersNames.Split(',');

            var prisoners = context.Prisoners
                .Where(p => names.Any(n => n == p.FullName))
                .Select(p => new ExportPrisonerXmlDto
                {
                    Id = p.Id,
                    FullName = p.FullName,
                    IncarcerationDate = p.IncarcerationDate.ToString(@"yyyy-MM-dd"),
                    MailDescriptions = p.Mails.Select(m => new ExportMailDto
                    {
                        Description = ReverseString(m.Description)
                    }).ToArray()
                })
                .OrderBy(p=>p.FullName)
                .ThenBy(p=>p.Id)
                .ToArray();

            var serializer = new XmlSerializer(prisoners.GetType(), new XmlRootAttribute("Prisoners"));
            var namespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            var result = new StringBuilder();

            serializer.Serialize(new StringWriter(result), prisoners, namespaces);

            return result.ToString().TrimEnd();
        }

        private static string ReverseString(string description)
        {
            char[] array = description.ToCharArray();
            Array.Reverse(array);
            return new string(array);
        }
    }
}