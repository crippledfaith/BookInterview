using Book.Shared.Data;
using Book.Shared.Models;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;

namespace Book.Server.Models
{
    public class BookRepository : IBookRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly BookValidator _validator;

        public BookRepository(AppDbContext appDbContext, BookValidator validator)
        {
            _appDbContext = appDbContext;
            _validator = validator;
        }

        public async Task<Shared.Models.Book> AddBook(Shared.Models.Book book)
        {
            ValidationResult valid = _validator.Validate(book);
            if (valid.IsValid)
            {
                var result = await _appDbContext.Books.AddAsync(book);
                await _appDbContext.SaveChangesAsync();
                return result.Entity;
            }
            else
            {
                throw new Exception(valid.ToString());
            }
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
            ValidationResult valid = _validator.Validate(book);
            if (valid.IsValid)
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
            else
            {
                throw new Exception(valid.ToString());
            }
        }
    }
}