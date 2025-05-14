using apbd19_cw11.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using apbd19_cw11.Data;

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
        
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var books = await _dbService.GetBooks();
            return Ok(books);
        }
    }
}
