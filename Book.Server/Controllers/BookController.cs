using Book.Server.Authorization;
using Book.Server.Models;
using Microsoft.AspNetCore.Mvc;

namespace Book.Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        private readonly IUserRepository _userRepository;

        public BookController(IBookRepository bookRepository, IUserRepository userRepository)
        {
            _bookRepository = bookRepository;
            _userRepository = userRepository;
        }

        /// <summary>
        /// Returns a list of paginated book with a default page size of 5.
        /// </summary>
        [AllowAnonymous]
        [HttpGet]
        public ActionResult GetBook([FromQuery] string? name, int page)
        {
            return Ok(_bookRepository.GetBook(name, page));
        }

        /// <summary>
        /// Gets a specific book by Id.
        /// </summary>
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult> GetBook(long id)
        {
            return Ok(await _bookRepository.GetBook(id));
        }

        /// <summary>
        /// Creates a book.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult> AddBook(Shared.Models.Book person)
        {
            return Ok(await _bookRepository.AddBook(person));
        }

        /// <summary>
        /// Updates a book with a specific Id.
        /// </summary>
        [HttpPut]
        public async Task<ActionResult> UpdateBook(Shared.Models.Book book)
        {
            return Ok(await _bookRepository.UpdateBook(book));
        }

        /// <summary>
        /// Deletes a book with a specific Id.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBook(long id)
        {
            return Ok(await _bookRepository.DeleteBook(id));
        }
    }
}
