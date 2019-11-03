namespace P01_HospitalDatabase.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    //Task2
    public class Doctor
    {
        [Key]
        public int DoctorId { get; set; }

        [Required]
        [Column(TypeName = "NVARCHAR(100)")]
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "NVARCHAR(100)")]
        public string Specialty { get; set; }

        public ICollection<Visitation> Visitations { get; set; }
            = new HashSet<Visitation>();
    }
}