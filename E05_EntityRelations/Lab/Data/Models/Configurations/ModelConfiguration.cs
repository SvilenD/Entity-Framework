namespace Lab.Data.Models.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ModelConfiguration : IEntityTypeConfiguration<Model>
    {
        public void Configure(EntityTypeBuilder<Model> model)
        {
            model.HasOne(m => m.Make)
                .WithMany(m => m.Models)
                .HasForeignKey(m => m.MakeId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}