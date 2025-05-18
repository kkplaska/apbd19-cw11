using apbd19_cw11.Services;
using Microsoft.AspNetCore.Mvc;
using apbd19_cw11.DTOs;
using apbd19_cw11.Exceptions;

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
            catch (NotFoundException e)
            {
                return NotFound("Patient not found");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddPrescription(PrescriptionDto prescription)
        {
            try
            {
                await _dbService.AddPrescription(prescription);
                return Created();
            }
            catch (PrescriptionExpiredException e)
            {
                return BadRequest(e);
            }
            catch (TooMuchMedicamentsException e)
            {
                return BadRequest(e);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
