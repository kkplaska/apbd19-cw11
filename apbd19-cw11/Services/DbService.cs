using System.Transactions;
using apbd19_cw11.Data;
using apbd19_cw11.DTOs;
using apbd19_cw11.Exceptions;
using apbd19_cw11.Models;
using Microsoft.EntityFrameworkCore;

namespace apbd19_cw11.Services;

public class DbService : IDbService
{
    private readonly DatabaseContext _context;
    public DbService(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<PatientWithDescriptionsDto> GetPatientDetails(int patientId)
    {
        var patient = await _context.Patients.Where(e => e.IdPatient == patientId).Select(e => new PatientWithDescriptionsDto
        {
            IdPatient = e.IdPatient,
            FirstName = e.FirstName,
            LastName = e.LastName,
            Birthdate = e.BirthDate,
            Prescriptions = e.Prescriptions.Select(a => new PresWithMedicsAndDocsDto
            {
                IdPrescription = a.IdPrescription,
                Date = a.Date,
                DueDate = a.DueDate,
                Medicaments = a.PrescriptionMedicament.Where(b => b.IdPrescription == a.IdPrescription)
                    .Select(c => new MedicamentDto
                        {
                            IdMedicament = c.IdMedicament,
                            Name = c.Medicament.Name,
                            Dose = c.Dose,
                            Description = c.Medicament.Description
                        }
                    ).ToList(),
                Doctor = new DoctorDto()
                {
                    FirstName = a.Doctor.FirstName,
                    IdDoctor = a.Doctor.IdDoctor,
                }
            }).OrderBy(a => a.DueDate).ToList()
        }).FirstOrDefaultAsync();
    
        if (patient == null) throw new NotFoundException();
        return patient;
    }

    public async Task DoesMedicamentsExist(List<int> medicamentIds)
    {
        var existingMedicaments = await _context.Medicaments
            .Where(m => medicamentIds.Contains(m.IdMedicament))
            .ToListAsync();
        if (existingMedicaments.Count != medicamentIds.Count)
        {
            throw new NotFoundException("Some medicaments not found");
        }
    }

    public async Task<Patient?> FindPatient(int IdPatient)
    {
        return await _context.Patients.Where(e => e.IdPatient == IdPatient).FirstOrDefaultAsync();
    }
    public async Task<Doctor?> FindDoctor(int IdDoctor)
    {
        return await _context.Doctors.Where(e => e.IdDoctor == IdDoctor).FirstOrDefaultAsync();
    }

    public async Task<int> AddNewPatient(Patient patient)
    {
        var entityEntry = await _context.Patients.AddAsync(patient);
        await _context.SaveChangesAsync();
        return entityEntry.Entity.IdPatient;
    }

    public async Task<int> AddNewPrescription(Prescription prescription)
    {
        var entityEntry = await _context.Prescriptions.AddAsync(prescription);
        await _context.SaveChangesAsync();
        return entityEntry.Entity.IdPrescription;
    }

    public async Task AddPrescriptionMedicaments(List<PrescriptionMedicament> medicaments)
    {
        await _context.PrescriptionMedicaments.AddRangeAsync(medicaments);
        await _context.SaveChangesAsync();
    }
}