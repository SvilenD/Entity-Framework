namespace PetStore.Data.Models.Validations
{
    using System;
    using System.ComponentModel.DataAnnotations;

    [AttributeUsage(AttributeTargets.Property)]
    public class CustomDateAttribute : RangeAttribute
    {
        public CustomDateAttribute()
          : base(typeof(DateTime),
                  DateTime.Now.ToShortDateString(),
                  DateTime.Now.AddYears(3).ToShortDateString())
        { 
        }
    }
}