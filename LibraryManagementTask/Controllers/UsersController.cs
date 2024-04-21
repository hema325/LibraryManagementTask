using AutoMapper;
using LibraryManagementTask.Attributes;
using LibraryManagementTask.Data.Repositories._UnitOfWork;
using LibraryManagementTask.Dtos.Users;
using LibraryManagementTask.Entities;
using LibraryManagementTask.Enums;
using LibraryManagementTask.Errors;
using LibraryManagementTask.Filters;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementTask.Controllers
{
    [Route("api/users")]
    [HaveRoles(Roles.Admin)]
    public class UsersController : BaseApiController
    {
        private readonly IUnitOfWork _ufw;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly ILogger<UsersController> _logger;

        public UsersController(IUnitOfWork ufw, IMapper mapper, IPasswordHasher<User> passwordHasher, ILogger<UsersController> logger)
        {
            _ufw = ufw;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _logger = logger;
        }

        /// <summary>
        ///  Add a new user to the system.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [Produces("application/json")]
        public async Task<IActionResult> CreateAsync(CreateUserDto dto)
        {
            var user = _mapper.Map<User>(dto);
            user.HashedPassword = _passwordHasher.HashPassword(user, dto.Password);

            _ufw.Users.Create(user);
            await _ufw.CompleteAsync();

            _logger.LogInformation("New user with id {id} has been added.", user.Id);

            return Ok(user.Id);
        }

        /// <summary>
        ///  Update an existing user's information.
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        public async Task<IActionResult> UpdateAsync(int id, UpdateUserDto dto) 
        {
            var user = await _ufw.Users.GetByIdAsync(id);

            if (user == null)
                return NotFound(ErrorResponse.NotFound());

            _mapper.Map(dto, user);

            _ufw.Users.Update(user);
            await _ufw.CompleteAsync();

            _logger.LogInformation("User with id {id} has been updated.", user.Id);

            return NoContent();
        }

        /// <summary>
        ///  Remove a user from the system.
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        public async Task<IActionResult> DeleteAsync(int id) 
        {
            var user = await _ufw.Users.GetByIdAsync(id);

            if (user == null)
                return NotFound(ErrorResponse.NotFound());

            _ufw.Users.Delete(user);
            await _ufw.CompleteAsync();

            return NoContent();
        }

        /// <summary>
        /// Retrieve details of a specific user by ID.
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        [Cache]
        public async Task<IActionResult> GetByIdAsync(int id) 
        {
            var user = await _ufw.Users.GetByIdAsync(id);

            if (user == null)
                return NotFound(ErrorResponse.NotFound());

            return Ok(_mapper.Map<UserDto>(user));
        }
            
        /// <summary>
        /// Retrieve details of a specific user by ID.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UserDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        [Cache]
        public async Task<IActionResult> GetAllAsync() 
        {
            var users = await _ufw.Users.GetAllAsync();

            return Ok(_mapper.Map<IEnumerable<UserDto>>(users));
        }
    }
}
