namespace P01_HospitalDatabase.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Diagnose
    {
        [Key]
        public int DiagnoseId { get; set; }

        [Required]
        [Column(TypeName = "NVARCHAR(50)")]
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "NVARCHAR(250)")]
        public string Comments { get; set; }

        [Required]
        public int PatientId { get; set; }

        public Patient Patient { get; set; }
    }
}