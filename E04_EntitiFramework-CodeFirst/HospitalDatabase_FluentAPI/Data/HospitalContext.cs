namespace P01_HospitalDatabase.Data
{
    using Microsoft.EntityFrameworkCore;
    using P01_HospitalDatabase.Data.Models;

    public class HospitalContext : DbContext
    {
        public DbSet<Diagnose> Diagnoses { get; set; }

        public DbSet<Doctor> Doctors { get; set; }

        public DbSet<Patient> Patients { get; set; }

        public DbSet<PatientMedicament> PatientsMedicaments { get; set; }

        public DbSet<Visitation> Visitations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ConnectionSettings.ConnectionString);
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            PatientEntityConfig(modelBuilder);

            VisitationEntityConfig(modelBuilder);

            DiagnoseEntityConfig(modelBuilder);

            MedicamentEntityConfig(modelBuilder);

            PatientMedicamentEntityConfig(modelBuilder);

            DoctorEntityConfig(modelBuilder);
        }

        private void DoctorEntityConfig(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Doctor>()
                .HasKey(d => d.DoctorId);

            modelBuilder
                .Entity<Doctor>()
                .HasMany(d => d.Visitations)
                .WithOne(v => v.Doctor)
                .HasForeignKey(v => v.DoctorId);

            modelBuilder
                .Entity<Doctor>()
                .Property(d => d.Name)
                .HasMaxLength(ValidationConstants.NameMaxLength)
                .IsUnicode()
                .IsRequired();

            modelBuilder
                .Entity<Doctor>()
                .Property(d => d.Specialty)
                .HasMaxLength(ValidationConstants.TextMaxLength)
                .IsUnicode();
        }

        private void PatientMedicamentEntityConfig(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<PatientMedicament>()
                .HasKey(pm => new
                {
                    pm.PatientId,
                    pm.MedicamentId
                });
        }

        private void MedicamentEntityConfig(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Medicament>()
                .HasKey(m => m.MedicamentId);

            modelBuilder
                .Entity<Medicament>()
                .HasMany(pm => pm.Prescriptions)
                .WithOne(m => m.Medicament)
                .HasForeignKey(m => m.MedicamentId);

            modelBuilder.Entity<Medicament>()
                .Property(m => m.Name)
                .HasMaxLength(ValidationConstants.NameMaxLength)
                .IsUnicode()
                .IsRequired();
        }

        private void DiagnoseEntityConfig(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Diagnose>()
                .HasKey(d => d.DiagnoseId);

            modelBuilder
                .Entity<Diagnose>()
                .Property(d => d.Name)
                .HasMaxLength(ValidationConstants.NameMaxLength)
                .IsUnicode()
                .IsRequired();

            modelBuilder
                .Entity<Diagnose>()
                .Property(d => d.Comments)
                .HasMaxLength(ValidationConstants.TextMaxLength)
                .IsUnicode()
                .IsRequired();
        }

        private void VisitationEntityConfig(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Visitation>()
                .HasKey(v => v.VisitationId);

            modelBuilder
                .Entity<Visitation>()
                .Property(v => v.Date)
                .IsRequired();

            modelBuilder
                .Entity<Visitation>()
                .Property(v => v.Comments)
                .HasMaxLength(ValidationConstants.TextMaxLength)
                .IsUnicode()
                .IsRequired();
        }

        private void PatientEntityConfig(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Patient>()
                .HasKey(p => p.PatientId);

            modelBuilder
                .Entity<Patient>()
                .HasMany(p => p.Diagnoses)
                .WithOne(d => d.Patient)
                .HasForeignKey(d => d.PatientId);

            modelBuilder
                .Entity<Patient>()
                .HasMany(p => p.Visitations)
                .WithOne(v => v.Patient)
                .HasForeignKey(p => p.PatientId);

            modelBuilder
                .Entity<Patient>()
                .HasMany(p => p.Presciptions)
                .WithOne(pm => pm.Patient)
                .HasForeignKey(p => p.PatientId);

            modelBuilder
                .Entity<Patient>()
                .Property(p => p.FirstName)
                .HasMaxLength(ValidationConstants.NameMaxLength)
                .IsUnicode()
                .IsRequired();

            modelBuilder
                .Entity<Patient>()
                .Property(p => p.LastName)
                .HasMaxLength(ValidationConstants.NameMaxLength)
                .IsUnicode()
                .IsRequired();

            modelBuilder
                .Entity<Patient>()
                .Property(p => p.Address)
                .HasMaxLength(ValidationConstants.TextMaxLength)
                .IsUnicode()
                .IsRequired();

            modelBuilder
                .Entity<Patient>()
                .Property(p => p.Email)
                .HasMaxLength(ValidationConstants.EmailMaxLength);
        }
    }
}