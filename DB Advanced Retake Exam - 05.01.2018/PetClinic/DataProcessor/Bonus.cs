namespace PetClinic.DataProcessor
{
    using System;
    using System.Linq;
    using PetClinic.Data;

    public class Bonus
    {
        private const string VetNotFound = "Vet with phone number {0} not found!";
        private const string VetUpdated = "{0}'s profession updated from {1} to {2}.";

        public static string UpdateVetProfession(PetClinicContext context, string phoneNumber, string newProfession)
        {
            var vet = context.Vets
                .FirstOrDefault(v => v.PhoneNumber == phoneNumber);

            if (vet == null)
            {
                return String.Format(VetNotFound, phoneNumber);
            }
            var oldProffesion = vet.Profession;
            vet.Profession = newProfession;
            context.SaveChanges();

            return String.Format(VetUpdated, vet.Name, oldProffesion, newProfession);
        }
    }
}