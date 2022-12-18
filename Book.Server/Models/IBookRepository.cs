using Book.Shared.Data;

namespace Book.Server.Models
{
    public interface IBookRepository
    {
        PagedResult<Shared.Models.Book> GetBook(string? name, int page);
        Task<Shared.Models.Book?> GetBook(long personId);
        Task<Shared.Models.Book> AddBook(Shared.Models.Book person);
        Task<Shared.Models.Book?> UpdateBook(Shared.Models.Book person);
        Task<Shared.Models.Book?> DeleteBook(long personId);
    }
}