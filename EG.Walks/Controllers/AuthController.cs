using EG.Walks.Contracts.Requests;
using EG.Walks.Infrastructure.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using EG.Walks.Contracts.Responses;

namespace EG.Walks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;
        public AuthController(UserManager<IdentityUser> userManager, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }
        
        // Create a new user and assign a role if provided
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            // Create a new user
            var identityUser = new IdentityUser
            {
                UserName = registerRequestDto.Username,
                Email = registerRequestDto.Username
            };

            // Attempt to create the user with the provided password
            var result = await _userManager.CreateAsync(identityUser, registerRequestDto.Password);

            if (result.Succeeded)
            {
                // If a role is provided, add the user to that role
                if (registerRequestDto.Roles != null)
                {
                    // Ensure the role exists, if not create it
                    result = await _userManager.AddToRolesAsync(identityUser, registerRequestDto.Roles);
                    if (result.Succeeded)
                    {
                        return Ok("User registered successfully.");
                    }
                }
            }
            // If we reach here, something went wrong
            return BadRequest(result.Errors.Select(e => e.Description));
        }

        // Login a user and return a token
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var user = await _userManager.FindByEmailAsync(loginRequestDto.userName);

            if (user != null)
            {
                var checkPasswordResult = await _userManager.CheckPasswordAsync(user, loginRequestDto.password);
                if (checkPasswordResult)
                {
                    var roles = await _userManager.GetRolesAsync(user);

                    if (roles != null)
                    {
                        var jwtToken = _unitOfWork.Token.CreateJWTToken(user, roles.ToList());

                        var response = new LoginResponseDto
                            {
                                JWTToken = jwtToken,
                            };

                        // Return the token in the response
                        return Ok(response);
                    }
                    return Unauthorized("User has no roles assigned.");
                }
                else
                {
                    return Unauthorized("Invalid password.");
                }
            }
            else
            {
                return NotFound("User not found.");
            }
        }
    }
}
