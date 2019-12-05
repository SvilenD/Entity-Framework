namespace PetClinic.DataProcessor
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.IO;
    using System.Text;
    using System.Globalization;
    using System.Xml.Serialization;
    using Newtonsoft.Json;

    using PetClinic.Data;
    using PetClinic.Models;
    using PetClinic.Models.ImportDtos;

    public class Deserializer
    {
        private const string ErrorMsg = "Error: Invalid data.";
        public static string ImportAnimalAids(PetClinicContext context, string jsonString)
        {
            var animalAidDtos = JsonConvert.DeserializeObject<AnimalAidImportDto[]>(jsonString);

            var animalAidsList = new List<AnimalAid>();
            var animalAidsNames = new HashSet<string>();
            var result = new StringBuilder();
            foreach (var dto in animalAidDtos)
            {
                if (IsValid(dto) == false || animalAidsNames.Contains(dto.Name))
                {
                    result.AppendLine(ErrorMsg);
                    continue;
                }
                animalAidsNames.Add(dto.Name);
                var animalAid = AutoMapper.Mapper.Map<AnimalAid>(dto);
                animalAidsList.Add(animalAid);
                result.AppendLine($"Record {animalAid.Name} successfully imported.");
            }

            context.AnimalAids.AddRange(animalAidsList);
            context.SaveChanges();

            return result.ToString().Trim();
        }

        public static string ImportAnimals(PetClinicContext context, string jsonString)
        {
            var animalDtos = JsonConvert.DeserializeObject<AnimalImportDto[]>(jsonString);

            var animalsList = new List<Animal>();
            var passportNumbers = new HashSet<string>();
            var result = new StringBuilder();
            foreach (var dto in animalDtos)
            {
                if (IsValid(dto) == false || IsValid(dto.Passport) == false || passportNumbers.Contains(dto.Passport.SerialNumber))
                {
                    result.AppendLine(ErrorMsg);
                    continue;
                }
                passportNumbers.Add(dto.Passport.SerialNumber);
                var animal = AutoMapper.Mapper.Map<Animal>(dto);
                animalsList.Add(animal);
                result.AppendLine($"Record {animal.Name} Passport №: {animal.Passport.SerialNumber} successfully imported.");
            }

            context.Animals.AddRange(animalsList);
            context.SaveChanges();

            return result.ToString().Trim();
        }

        public static string ImportVets(PetClinicContext context, string xmlString)
        {
            var xmlSerializer = new XmlSerializer(typeof(VetImportDto[]), new XmlRootAttribute("Vets"));
            var vetDtos = (VetImportDto[])xmlSerializer.Deserialize(new StringReader(xmlString));

            var vetsList = new List<Vet>();
            var result = new StringBuilder();
            var vetsPhoneNums = new HashSet<string>();
            foreach (var dto in vetDtos)
            {
                if (IsValid(dto) == false || vetsPhoneNums.Contains(dto.PhoneNumber))
                {
                    result.AppendLine(ErrorMsg);
                    continue;
                }
                vetsPhoneNums.Add(dto.PhoneNumber);
                var vet = AutoMapper.Mapper.Map<Vet>(dto);
                vetsList.Add(vet);
                result.AppendLine($"Record {vet.Name} successfully imported.");
            }

            context.Vets.AddRange(vetsList);
            context.SaveChanges();

            return result.ToString().Trim();
        }

        public static string ImportProcedures(PetClinicContext context, string xmlString)
        {
            var xmlSerializer = new XmlSerializer(typeof(ProcedureImportDto[]), new XmlRootAttribute("Procedures"));
            var procedureDtos = (ProcedureImportDto[])xmlSerializer.Deserialize(new StringReader(xmlString));

            var animals = context.Animals.ToHashSet();
            var animalAids = context.AnimalAids.ToHashSet();
            var vets = context.Vets.ToHashSet();
            var proceduresList = new List<Procedure>();
            var result = new StringBuilder();

            foreach (var dto in procedureDtos)
            {
                if (IsValid(dto) == false)
                {
                    result.AppendLine(ErrorMsg);
                    continue;
                }

                try
                {
                    var procedure = new Procedure
                    {
                        Animal = animals.First(a => a.PassportSerialNumber == dto.AnimalSerialNumber),
                        Vet = vets.First(v => v.Name == dto.VetName),
                        DateTime = DateTime.ParseExact(dto.DateTime, "dd-MM-yyyy", CultureInfo.InvariantCulture)
                    };

                    foreach (var item in dto.AnimalAidNames)
                    {
                        var animalAidId = animalAids.First(a => a.Name == item.Name).Id;
                        if (procedure.ProcedureAnimalAids.Any(paa => paa.AnimalAidId == animalAidId))
                        {
                            throw new Exception(); 
                        }
                        procedure.ProcedureAnimalAids.Add(new ProcedureAnimalAid { AnimalAidId = animalAidId });
                    }

                    proceduresList.Add(procedure);
                    result.AppendLine($"Record successfully imported.");
                }
                catch (Exception)
                {
                    result.AppendLine(ErrorMsg);
                    continue;
                }
            }

            context.Procedures.AddRange(proceduresList);
            context.SaveChanges();

            return result.ToString().Trim();
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