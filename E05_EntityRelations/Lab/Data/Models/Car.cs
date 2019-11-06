namespace Lab.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using static Validations.Car;

    public class Car
    {
        public int Id { get; set; }

        [Required]
        public DateTime ProductionDate { get; set; }

        [Required]
        [Column(TypeName = VinRequirement)]
        public string VIN { get; set; }

        public decimal Price { get; set; }

        public string Color { get; set; }

        public string Extras { get; set; }

        public int ModelId { get; set; }

        public Model Model { get; set; }

        public ICollection<CarPurchase> Owners { get; set; }
            = new HashSet<CarPurchase>();
    }
}