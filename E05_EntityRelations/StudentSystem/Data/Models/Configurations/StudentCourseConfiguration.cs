namespace P01_StudentSystem.Data.Models.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class StudentCourseConfiguration : IEntityTypeConfiguration<StudentCourse>
    {
        public void Configure(EntityTypeBuilder<StudentCourse> studentCourse)
        {
            studentCourse.HasKey(sc => new { sc.CourseId, sc.StudentId});
        }
    }
}