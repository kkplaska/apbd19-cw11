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
       modelBuilder.Entity<Doctor>().HasData(new List<Doctor>()
        {
            new Doctor() { IdDoctor = 1, FirstName = "Anna", LastName = "Nowak", Email = "anna.nowak@clinic.pl" },
            new Doctor() { IdDoctor = 2, FirstName = "Piotr", LastName = "Kowalski", Email = "piotr.kowalski@clinic.pl" },
            new Doctor() { IdDoctor = 3, FirstName = "Ewa", LastName = "Wiśniewska", Email = "ewa.wisniewska@clinic.pl" }
        });
        
        modelBuilder.Entity<Patient>().HasData(new List<Patient>()
        {
            new Patient(){ IdPatient = 1, FirstName = "Jan", LastName = "Kaczmarek", BirthDate = DateOnly.Parse("1985-03-12")},
            new Patient(){ IdPatient = 2, FirstName = "Maria", LastName = "Zielińska", BirthDate = DateOnly.Parse("1992-08-25")},
            new Patient(){ IdPatient = 3, FirstName = "Tomasz", LastName = "Wójcik", BirthDate = DateOnly.Parse("1978-11-07")}
        });

        modelBuilder.Entity<Medicament>().HasData(new List<Medicament>()
        {
            new Medicament(){IdMedicament = 1, Name = "Paracetamol", Description = "Pain relief", Type = "Analgesic"},
            new Medicament(){IdMedicament = 2, Name = "Amoxicillin", Description = "Antibiotic for infections", Type = "Antibiotic"},
            new Medicament(){IdMedicament = 3, Name = "Metformin", Description = "Diabetes treatment", Type = "Antidiabetic"}
        });

        modelBuilder.Entity<Prescription>().HasData(new List<Prescription>()
        {
            new Prescription(){IdPrescription = 1, Date = DateOnly.Parse("2025-05-10"),	DueDate = DateOnly.Parse("2025-05-20"),	IdPatient = 1, IdDoctor	= 1},   
            new Prescription(){IdPrescription = 2, Date = DateOnly.Parse("2025-05-12"),	DueDate = DateOnly.Parse("2025-05-22"),	IdPatient = 1, IdDoctor	= 2},   
            new Prescription(){IdPrescription = 3, Date = DateOnly.Parse("2025-05-14"),	DueDate = DateOnly.Parse("2025-05-24"),	IdPatient = 3, IdDoctor	= 3}   
        });

        modelBuilder.Entity<PrescriptionMedicament>().HasData(new List<PrescriptionMedicament>()
        {
          new PrescriptionMedicament(){IdMedicament = 1, IdPrescription = 1, Dose = 500, Details = "Take twice daily" },   
          new PrescriptionMedicament(){IdMedicament = 2, IdPrescription = 2, Dose = 250, Details = "After meals" },   
          new PrescriptionMedicament(){IdMedicament = 3, IdPrescription = 3, Dose = 850, Details = "Once in the morning" }   
        });
    }
}