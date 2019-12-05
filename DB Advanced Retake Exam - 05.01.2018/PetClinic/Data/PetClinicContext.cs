namespace PetClinic.Data
{
    using Microsoft.EntityFrameworkCore;
    using PetClinic.Models;

    public class PetClinicContext : DbContext
    {
        public PetClinicContext() { }

        public PetClinicContext(DbContextOptions options)
            : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.ConnectionString);
            }
        }
        public DbSet<Animal> Animals { get; set; }
        public DbSet<AnimalAid> AnimalAids { get; set; }
        public DbSet<Passport> Passports { get; set; }
        public DbSet<Procedure> Procedures { get; set; }
        public DbSet<ProcedureAnimalAid> ProceduresAnimalAids { get; set; }
        public DbSet<Vet> Vets { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Vet>(vet =>
            {
                vet.HasIndex(b => b.PhoneNumber)
                .IsUnique();
            });

            builder.Entity<AnimalAid>()
                .HasIndex(b => b.Name)
                .IsUnique();

            builder.Entity<Animal>()
                .HasOne(a => a.Passport)
                .WithOne(p => p.Animal)
                .HasForeignKey<Animal>(a => a.PassportSerialNumber);

            builder.Entity<Procedure>(proc =>
            {
                proc.HasOne(p => p.Vet)
                    .WithMany(v => v.Procedures)
                    .HasForeignKey(p => p.VetId);
                proc.HasOne(p => p.Animal)
                    .WithMany(a => a.Procedures)
                    .HasForeignKey(p => p.AnimalId);
            });

            builder.Entity<ProcedureAnimalAid>(paa =>
            {
                paa.HasKey(p => new { p.ProcedureId, p.AnimalAidId });

                paa.HasOne(p => p.AnimalAid)
                    .WithMany(a => a.AnimalAidProcedures)
                    .HasForeignKey(p => p.AnimalAidId);
                paa.HasOne(p => p.Procedure)
                    .WithMany(pr => pr.ProcedureAnimalAids)
                    .HasForeignKey(p => p.ProcedureId);
            });
        }
    }
}