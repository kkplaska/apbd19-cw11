using apbd19_cw11.Data;
using apbd19_cw11.DTOs;
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
        var patient = await _context.Patients.Select(e => new PatientWithDescriptionsDto
        {
            IdPatient = e.IdPatient,
            FirstName = e.FirstName,
            LastName = e.LastName,
            Birthdate = e.BirthDate

        }).First();

        
        return patient;
    }
    
    public async Task<List<BookWithAuthorsDto>> GetBooks()
    {
        var books = await _context.Books.Select(e =>
        new BookWithAuthorsDto {
            Name = e.Name,
            Price = e.Price,
            Authors = e.BookAuthors.Select(a =>
            new AuthorDto {
                FirstName = a.Doctor.FirstName,
                LastName = a.Doctor.LastName
            }).ToList()
        }).ToListAsync();
        return books;
    }
}