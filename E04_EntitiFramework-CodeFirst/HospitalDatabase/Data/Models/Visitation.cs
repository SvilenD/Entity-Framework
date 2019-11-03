namespace P01_HospitalDatabase.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Visitation
    {
        [Key]
        public int VisitationId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [Column(TypeName = "NVARCHAR(250)")]
        public string Comments { get; set; }

        //Task2
        [Required]
        public int DoctorId { get; set; }

        //Task2
        public Doctor Doctor { get; set; }

        [Required]
        public int PatientId { get; set; }

        public Patient Patient { get; set; }
    }
}