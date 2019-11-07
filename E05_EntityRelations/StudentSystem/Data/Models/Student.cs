namespace P01_StudentSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Student
    {
        public int StudentId { get; set; }

        [Required]
        [Column(TypeName= "NVARCHAR(100)")]
        public string Name { get; set; }

        [Column(TypeName = "CHAR(10)")]
        public string PhoneNumber { get; set; }

        public DateTime RegisteredOn { get; set; }

        public DateTime? Birthday { get; set; }

        public ICollection<StudentCourse> CourseEnrollments { get; set; }
            = new HashSet<StudentCourse>();

        public ICollection<Homework> HomeworkSubmissions { get; set; }
            = new HashSet<Homework>();
    }
}