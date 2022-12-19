using Book.Server.Authorization;
using Book.Server.Models;
using Book.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Book.Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly AppSettings _appSettings;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserRepository userRepository, IOptions<AppSettings> appSettings, ILogger<UserController> logger)
        {
            _userRepository = userRepository;
            _appSettings = appSettings.Value;
            _logger = logger;
        }

        /// <summary>
        /// Authenticates a user and returns a JWT token and user details
        /// </summary>
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public ActionResult Authenticate(AuthenticateRequest request)
        {
            try
            {
                return Ok(_userRepository.Authenticate(request));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }


        /// <summary>
        /// Returns a list of paginated users with a default page size of 5.
        /// </summary>
        [HttpGet]
        public ActionResult GetUsers([FromQuery] string? name, int page)
        {
            try
            {
                return Ok(_userRepository.GetUsers(name, page));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Gets a specific user by Id.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult> GetUser(int id)
        {
            try
            {
                return Ok(await _userRepository.GetUser(id));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Creates a user and hashes password.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult> AddUser(User user)
        {
            try
            {
                return Ok(await _userRepository.AddUser(user));

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Updates a user with a specific Id, hashing password if updated.
        /// </summary>
        [HttpPut]
        public async Task<ActionResult> UpdateUser(User user)
        {
            try
            {
                return Ok(await _userRepository.UpdateUser(user));

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Deletes a user with a specific Id.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            try
            {
                return Ok(await _userRepository.DeleteUser(id));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex.Message);
            }
        }
    }
}
