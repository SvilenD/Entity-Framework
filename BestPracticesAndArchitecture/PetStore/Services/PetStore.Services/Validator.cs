namespace PetStore.Services
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    internal static class Validator
    {
        internal static bool IsValid(object entity)
        {
            var validationContext = new ValidationContext(entity);
            var validationResult = new List<ValidationResult>();

            var result = System.ComponentModel.DataAnnotations.Validator.TryValidateObject(entity, validationContext, validationResult, true);

            return result;
        }
    }
}