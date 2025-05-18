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

    
    public async Task AddPrescription(PrescriptionDto prescriptionDto)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            if (prescriptionDto.DueDate < prescriptionDto.Date)
            {
                throw new PrescriptionExpiredException();
            }
    
            if (prescriptionDto.Medicaments.Count > 10)
            {
                throw new TooMuchMedicamentsException($"{prescriptionDto.Medicaments.Count}");
            }
    
            // Sprawdzanie leków 
            var medicamentIds = prescriptionDto.Medicaments.Select(m => m.IdMedicament);
            var existingMedicaments = await _context.Medicaments
                .Where(m => medicamentIds.Contains(m.IdMedicament))
                .ToListAsync();
            
            if (existingMedicaments.Count != medicamentIds.Count())
            {
                throw new NotFoundException("Some medicaments not found");
            }
    
            // Sprawdzanie pacjenta w bazie danych
            // var patient = await _context.Patients.FindAsync(prescriptionDto.Patient.IdPatient);
            var patient = new Patient()
            {
                IdPatient = prescriptionDto.Patient.IdPatient,
                FirstName = prescriptionDto.Patient.FirstName,
                LastName = prescriptionDto.Patient.LastName,
                BirthDate = prescriptionDto.Patient.BirthDate
            };
            
            // // Dodawanie pacjenta
            // if (patient == null)
            // {
            //     patient = new Patient()
            //     {
            //         IdPatient = prescriptionDto.Patient.IdPatient,
            //         FirstName = prescriptionDto.Patient.FirstName,
            //         LastName = prescriptionDto.Patient.LastName,
            //         BirthDate = prescriptionDto.Patient.BirthDate
            //     };
            // }
    
            // var prescription = new Prescription()
            // {
            //     Date = prescriptionDto.Date,
            //     DueDate = prescriptionDto.DueDate,
            //     IdPatient = patient.IdPatient
            // };
            //
            // var prescriptionMedicaments = prescriptionDto.Medicaments.Select(a => new PrescriptionMedicament()
            // {
            //     IdMedicament = a.IdMedicament,
            //     IdPrescription = prescription.IdPrescription,
            //     Dose = a.Dose,
            //     Details = a.Description
            // }).ToList();
    
            await _context.Patients.AddAsync(patient);
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                // await _context.Prescriptions.AddAsync(prescription);
                // await _context.PrescriptionMedicaments.AddRangeAsync(prescriptionMedicaments);
        
                scope.Complete();
            }
            
            await _context.SaveChangesAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
    

    // public async Task AddPrescription(PrescriptionDto prescriptionDto)
    // {
    //     var patient = await _context.Patients.FindAsync(prescriptionDto.Patient.IdPatient);
    //
    //     if (patient == null)
    //     {
    //         // Add new patient
    //         patient = new Patient
    //         {
    //             IdPatient = prescriptionDto.Patient.IdPatient,
    //             FirstName = prescriptionDto.Patient.FirstName,
    //             LastName = prescriptionDto.Patient.LastName,
    //             BirthDate = prescriptionDto.Patient.BirthDate
    //         };
    //         _context.Patients.Add(patient);
    //     }
    //
    //     // NOTE: You may hardcode or get the doctor ID from the request/user
    //     int doctorId = 1;
    //
    //     var prescription = new Prescription
    //     {
    //         Date = prescriptionDto.Date,
    //         DueDate = prescriptionDto.DueDate,
    //         IdDoctor = doctorId,
    //         IdPatient = prescriptionDto.Patient.IdPatient
    //     };
    //
    //     _context.Prescriptions.Add(prescription);
    //     await _context.SaveChangesAsync();
    // }
    
}