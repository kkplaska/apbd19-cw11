using apbd19_cw11.Models;
using Microsoft.EntityFrameworkCore;

namespace apbd19_cw11.Data;

public class DatabaseContext : DbContext
{
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Medicament> Medicaments { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Prescription> BookAuthors { get; set; }
    public DbSet<PrescriptionMedicament> PrescriptionMedicaments { get; set; }
    
    protected DatabaseContext()
    {
    }

    public DatabaseContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Doctor>(a =>
        {
            a.ToTable("Author");
            
            a.HasKey(e => e.IdDoctor);
            a.Property(e => e.FirstName).HasMaxLength(100);
            a.Property(e => e.LastName).HasMaxLength(200);
        });

        modelBuilder.Entity<Doctor>().HasData(new List<Doctor>()
        {
            new Doctor() { IdDoctor = 1, FirstName = "Jane", LastName = "Doe"},
            new Doctor() { IdDoctor = 2, FirstName = "John", LastName = "Doe"},
        });
        
        modelBuilder.Entity<Patient>().HasData(new List<Patient>()
        {
        });
        
        modelBuilder.Entity<Prescription>().HasData(new List<Prescription>()
        {
        });
    }
}