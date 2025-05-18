using System.Transactions;
using apbd19_cw11.Services;
using Microsoft.AspNetCore.Mvc;
using apbd19_cw11.DTOs;
using apbd19_cw11.Exceptions;
using apbd19_cw11.Models;

namespace apbd19_cw11.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrescriptionsController : ControllerBase
    {
        private readonly IDbService _dbService;
        public PrescriptionsController(IDbService dbService)
        {
            _dbService = dbService;
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPatientDetails(int id)
        {
            try
            {
                var patient = await _dbService.GetPatientDetails(id);
                return Ok(patient);
            }
            catch (NotFoundException _)
            {
                return NotFound("Patient not found");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> AddPrescription([FromBody] PrescriptionDto prescriptionDto, int id)
        {
            
            if (prescriptionDto.DueDate < prescriptionDto.Date)
            {
                return BadRequest("Prescription is expired!");
            }
            if (prescriptionDto.Medicaments.Count > 10)
            {
                return BadRequest("Can't add more than 10 medicaments on one prescription");
            }
            
            // Sprawdzanie leków
            var medicamentIds = prescriptionDto.Medicaments.Select(m => m.IdMedicament).ToList();
            try
            {
                await _dbService.DoesMedicamentsExist(medicamentIds);
            }
            catch (NotFoundException e)
            {
                return BadRequest(e.Message);
            }

            var patient = await _dbService.FindPatient(prescriptionDto.Patient.IdPatient);
            
            if (patient == null)
            {
                patient = new Patient()
                {
                    FirstName = prescriptionDto.Patient.FirstName,
                    LastName = prescriptionDto.Patient.LastName,
                    BirthDate = prescriptionDto.Patient.BirthDate
                };
                patient.IdPatient = await _dbService.AddNewPatient(patient);
            }

            var doctor = await _dbService.FindDoctor(id);
            if (doctor == null)
            {
                return NotFound("Doctor not found");
            }
            
            var prescription = new Prescription()
            {
                Date = prescriptionDto.Date,
                DueDate = prescriptionDto.DueDate,
                IdPatient = prescriptionDto.Patient.IdPatient,
                IdDoctor = doctor.IdDoctor,
            };
            prescription.IdPrescription = await _dbService.AddNewPrescription(prescription);
            
            var prescriptionMedicaments = prescriptionDto.Medicaments.Select(a => new PrescriptionMedicament()
            {
                IdMedicament = a.IdMedicament,
                IdPrescription = prescription.IdPrescription,
                Dose = a.Dose,
                Details = a.Description
            }).ToList();
            
            await _dbService.AddPrescriptionMedicaments(prescriptionMedicaments);
            return Ok();
        }
    }
}
