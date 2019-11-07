namespace P01_StudentSystem.Data.Models.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> course)
        {
            course
                .Property(c => c.Description)
                .IsUnicode();

            course
                .HasMany(c => c.Resources)
                .WithOne(r => r.Course)
                .HasForeignKey(r => r.CourseId)
                .OnDelete(DeleteBehavior.Restrict);

            course.HasMany(c => c.StudentsEnrolled)
                .WithOne(sc => sc.Course)
                .HasForeignKey(sc => sc.CourseId)
                .OnDelete(DeleteBehavior.Restrict);

            course.HasMany(c => c.HomeworkSubmissions)
                .WithOne(h => h.Course)
                .HasForeignKey(h => h.CourseId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

