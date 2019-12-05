namespace PetClinic.DataProcessor
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;
    using AutoMapper.QueryableExtensions;
    using Newtonsoft.Json;

    using PetClinic.Data;
    using PetClinic.Models.ExportDtos;

    public class Serializer
    {
        public static string ExportAnimalsByOwnerPhoneNumber(PetClinicContext context, string phoneNumber)
        {
            var animals = context.Passports
                .Where(p => p.OwnerPhoneNumber == phoneNumber)
                .Select(p => new
                {
                    OwnerName = p.OwnerName,
                    AnimalName = p.Animal.Name,
                    Age = p.Animal.Age,
                    SerialNumber = p.SerialNumber,
                    RegisteredOn = p.RegistrationDate.ToString("dd-MM-yyyy")
                })
                .OrderBy(p => p.Age)
                .ThenBy(p => p.SerialNumber)
                .ToArray();

            var animalsJson = JsonConvert.SerializeObject(animals, Newtonsoft.Json.Formatting.Indented);

            return animalsJson;
        }

        public static string ExportAllProcedures(PetClinicContext context)
        {
            var procedures = context.Procedures
                .OrderBy(p=>p.DateTime)
                .ThenBy(p=>p.Animal.PassportSerialNumber)
                .ProjectTo<ProcedureExportDto>()
                .ToArray();
            var serializer = new XmlSerializer(procedures.GetType(), new XmlRootAttribute("Procedures"));
            var namespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            var result = new StringBuilder();

            serializer.Serialize(new StringWriter(result), procedures, namespaces);

            return result.ToString().TrimEnd();
        }
    }
}
