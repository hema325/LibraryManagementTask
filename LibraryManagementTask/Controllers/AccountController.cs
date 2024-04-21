using AutoMapper;
using LibraryManagementTask.Data.Repositories._UnitOfWork;
using LibraryManagementTask.Dtos.Users;
using LibraryManagementTask.Entities;
using LibraryManagementTask.Errors;
using LibraryManagementTask.Extensions;
using LibraryManagementTask.Services.JWT;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementTask.Controllers
{
    [Route("api/account")]
    public class AccountController: BaseApiController
    {
        private readonly IUnitOfWork _ufw;
        private readonly IMapper _mapper;
        private readonly ITokenGenerator _tokenGenerator;
        private readonly IPasswordHasher<User> _passwordHasher;

        public AccountController(IUnitOfWork ufw, IMapper mapper, ITokenGenerator tokenGenerator, IPasswordHasher<User> passwordHasher)
        {
            _ufw = ufw;
            _mapper = mapper;
            _tokenGenerator = tokenGenerator;
            _passwordHasher = passwordHasher;
        }

        /// <summary>
        ///  Get access token.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(AuthResultDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
        [Produces("application/json")]
        public async Task<IActionResult> LoginAsync(LoginDto dto)
        {
            var user = await _ufw.Users.GetByEmailAsync(dto.Email);

            if (user == null || _passwordHasher.VerifyHashedPassword(user, user.HashedPassword, dto.Password) == PasswordVerificationResult.Failed)
                return Unauthorized(ErrorResponse.Unauthorized("Email or password isn't correct."));

            var result = _mapper.Map<AuthResultDto>(user);
            result.Token = _tokenGenerator.Generate(user);

            return Ok(result);
        }

        /// <summary>
        ///  Get current user details.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
        [Produces("application/json")]
        [Authorize]
        public async Task<IActionResult> GetAsync()
        {
            var user = await _ufw.Users.GetByIdAsync(User.Id().Value);

            return Ok(_mapper.Map<UserDto>(user));
        }
    }
}
