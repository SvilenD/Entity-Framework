﻿namespace P01_HospitalDatabase.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Medicament
    {
        [Key]
        public int MedicamentId { get; set; }

        [Required]
        [Column(TypeName = "NVARCHAR(50)")]
        public string Name { get; set; }

        public ICollection<PatientMedicament> Prescriptions { get; set; }
    }
}