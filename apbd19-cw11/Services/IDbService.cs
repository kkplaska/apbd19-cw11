using apbd19_cw11.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace apbd19_cw11.Services;

public interface IDbService
{
    Task<PatientWithDescriptionsDto> GetPatientDetails(int patientId);
    Task AddPrescription(PrescriptionDto prescription);
}