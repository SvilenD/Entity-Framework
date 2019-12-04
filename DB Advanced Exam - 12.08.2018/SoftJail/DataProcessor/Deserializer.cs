namespace SoftJail.DataProcessor
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Newtonsoft.Json;

    using Data;
    using SoftJail.Data.Models;
    using SoftJail.DataProcessor.ImportDto;

    using static OutputMessages;
    using System.Xml.Serialization;
    using System.IO;
    using SoftJail.Data.Models.Enums;

    public class Deserializer
    {
        public static string ImportDepartmentsCells(SoftJailDbContext context, string jsonString)
        {
            var departmentDtos = JsonConvert.DeserializeObject<DepartmentImportDto[]>(jsonString);

            var departments = new List<Department>();
            var result = new StringBuilder();
            foreach (var dto in departmentDtos)
            {
                if (IsValid(dto) == false)
                {
                    result.AppendLine(ErrorMessage);
                    continue;
                }
                var validCells = true;
                foreach (var cell in dto.Cells)
                {
                    if (IsValid(cell) == false)
                    {
                        result.AppendLine(ErrorMessage);
                        validCells = false;
                        break;
                    }
                }

                if (validCells)
                {
                    var department = AutoMapper.Mapper.Map<Department>(dto);
                    departments.Add(department);
                    result.AppendLine(String.Format(DepartmentImportMessage, department.Name, department.Cells.Count));
                }
            }

            context.Departments.AddRange(departments);
            context.SaveChanges();

            return result.ToString().TrimEnd();
        }

        public static string ImportPrisonersMails(SoftJailDbContext context, string jsonString)
        {
            var prisonerDtos = JsonConvert.DeserializeObject<PrisonerImportDto[]>(jsonString);

            var prisoners = new List<Prisoner>();
            var result = new StringBuilder();
            foreach (var dto in prisonerDtos)
            {
                if (IsValid(dto) == false)
                {
                    result.AppendLine(ErrorMessage);
                    continue;
                }
                var validMails = true;
                foreach (var cell in dto.Mails)
                {
                    if (IsValid(cell) == false)
                    {
                        result.AppendLine(ErrorMessage);
                        validMails = false;
                        break;
                    }
                }

                if (validMails)
                {
                    var prisoner = AutoMapper.Mapper.Map<Prisoner>(dto);
                    prisoners.Add(prisoner);
                    result.AppendLine(String.Format(PrisonerImportMessage, prisoner.FullName, prisoner.Age));
                }
            }
            context.Prisoners.AddRange(prisoners);
            context.SaveChanges();

            return result.ToString().TrimEnd();
        }

        public static string ImportOfficersPrisoners(SoftJailDbContext context, string xmlString)
        {
            var xmlSerializer = new XmlSerializer(typeof(OfficerImportDto[]), new XmlRootAttribute("Officers"));

            var officerDtos = (OfficerImportDto[])xmlSerializer.Deserialize(new StringReader(xmlString));

            var officers = new List<Officer>();
            var result = new StringBuilder();

            foreach (var dto in officerDtos)
            {
                var isValidPosition = Enum.TryParse(dto.Position, out Position resultPosition);
                var isValidWeapon = Enum.TryParse(dto.Weapon, out Weapon resultWeapon);

                if (IsValid(dto) == false || isValidPosition == false|| isValidWeapon == false)
                {
                    result.AppendLine(ErrorMessage);
                    continue;
                }

                var officer = new Officer
                {
                    FullName = dto.FullName,
                    Salary = dto.Salary,
                    Position = resultPosition,
                    Weapon = resultWeapon,
                    DepartmentId = dto.DepartmentId,
                    OfficerPrisoners = dto.Prisoners
                        .Select(s => new OfficerPrisoner { PrisonerId = s.Id }).ToArray()
                };

                officers.Add(officer);
                result.AppendLine(String.Format(OfficerImportMessage, officer.FullName, officer.OfficerPrisoners.Count));
            }

            context.AddRange(officers);
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