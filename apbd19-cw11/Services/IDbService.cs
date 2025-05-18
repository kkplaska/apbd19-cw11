using apbd19_cw11.DTOs;
using apbd19_cw11.Models;

namespace apbd19_cw11.Services;

public interface IDbService
{
    Task<PatientWithDescriptionsDto> GetPatientDetails(int patientId);
    
    Task DoesMedicamentsExist(List<int> medicamentIds);
    Task<Patient?> FindPatient(int IdPatient);
    Task<Doctor?> FindDoctor(int IdDoctor);
    Task<int> AddNewPatient(Patient patient);
    Task<int> AddNewPrescription(Prescription prescription);
    Task AddPrescriptionMedicaments(List<PrescriptionMedicament> medicaments);
}