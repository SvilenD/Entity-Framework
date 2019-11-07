namespace P01_StudentSystem.Data.Models.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> student)
        {
            student
                .HasMany(s => s.HomeworkSubmissions)
                .WithOne(h => h.Student)
                .HasForeignKey(h => h.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            student
                .HasMany(s => s.CourseEnrollments)
                .WithOne(sc => sc.Student)
                .HasForeignKey(sc => sc.StudentId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
