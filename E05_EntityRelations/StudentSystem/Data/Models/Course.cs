namespace P01_StudentSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Course
    {
        public int CourseId { get; set; }

        [Required]
        [Column(TypeName = "NVARCHAR(80)")]
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public decimal Price { get; set; }

        public ICollection<Resource> Resources { get; set; }
            = new HashSet<Resource>();

        public ICollection<StudentCourse> StudentsEnrolled { get; set; }
            = new HashSet<StudentCourse>();

        public ICollection<Homework> HomeworkSubmissions { get; set; }
            = new HashSet<Homework>();
    }
}