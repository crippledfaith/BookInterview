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
        private readonly ILogger<BookController> _logger;

        public BookController(IBookRepository bookRepository, IUserRepository userRepository, ILogger<BookController> logger)
        {
            _bookRepository = bookRepository;
            _userRepository = userRepository;
            _logger = logger;
        }

        /// <summary>
        /// Returns a list of paginated book with a default page size of 5.
        /// </summary>
        [AllowAnonymous]
        [HttpGet]
        public ActionResult GetBook([FromQuery] string? name, int page)
        {
            try
            {
                return Ok(_bookRepository.GetBook(name, page));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Gets a specific book by Id.
        /// </summary>
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult> GetBook(long id)
        {
            try
            {
                return Ok(await _bookRepository.GetBook(id));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Creates a book.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult> AddBook(Shared.Models.Book person)
        {
            try
            {

                return Ok(await _bookRepository.AddBook(person));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Updates a book with a specific Id.
        /// </summary>
        [HttpPut]
        public async Task<ActionResult> UpdateBook(Shared.Models.Book book)
        {
            try
            {
                return Ok(await _bookRepository.UpdateBook(book));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Deletes a book with a specific Id.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBook(long id)
        {
            try
            {
                return Ok(await _bookRepository.DeleteBook(id));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex.Message);
            }
        }
    }
}
