using apbd19_cw11.DTOs;

namespace apbd19_cw11.Services;

public interface IDbService
{
    Task<List<BookWithAuthorsDto>> GetBooks();
}