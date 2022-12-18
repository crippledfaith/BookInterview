using Book.Shared.Data;
using Microsoft.EntityFrameworkCore;

namespace Book.Server.Models
{
    public class BookRepository : IBookRepository
    {
        private readonly AppDbContext _appDbContext;

        public BookRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Shared.Models.Book> AddBook(Shared.Models.Book person)
        {
            var result = await _appDbContext.Books.AddAsync(person);
            await _appDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Shared.Models.Book?> DeleteBook(long bookId)
        {
            var result = await _appDbContext.Books.FirstOrDefaultAsync(p => p.BookId == bookId);
            if (result != null)
            {
                _appDbContext.Books.Remove(result);
                await _appDbContext.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException("Book not found");
            }
            return result;
        }

        public async Task<Shared.Models.Book?> GetBook(long bookId)
        {
            var result = await _appDbContext.Books
                .FirstOrDefaultAsync(p => p.BookId == bookId);
            if (result != null)
            {
                return result;
            }
            else
            {
                throw new KeyNotFoundException("Book not found");
            }
        }

        public PagedResult<Shared.Models.Book> GetBook(string? title, int page)
        {
            int pageSize = 10;

            if (title != null)
            {
                return _appDbContext.Books
                    .Where(p => p.Title.ToLower().Contains(title.ToLower()))
                    .OrderBy(p => p.Title)
                    .GetPaged(page, pageSize);
            }
            else
            {
                return _appDbContext.Books
                    .OrderBy(p => p.Title)
                    .GetPaged(page, pageSize);
            }
        }

        public async Task<Shared.Models.Book?> UpdateBook(Shared.Models.Book book)
        {
            var result = await _appDbContext.Books.FirstOrDefaultAsync(p => p.BookId == book.BookId);
            if (result != null)
            {
                _appDbContext.Entry(result).CurrentValues.SetValues(book);
                await _appDbContext.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException("Book not found");
            }
            return result;
        }
    }
}