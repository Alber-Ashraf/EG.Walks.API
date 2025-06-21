using EG.Walks.Contracts.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EG.Walks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        public AuthController(UserManager<IdentityUser> userManager)
        {
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
                if (!string.IsNullOrEmpty(registerRequestDto.Role))
                {
                    // Ensure the role exists, if not create it
                    result = await _userManager.AddToRoleAsync(identityUser, registerRequestDto.Role);
                    if (result.Succeeded)
                    {
                        return Ok("User registered successfully.");
                    }
                }
            }
            // If we reach here, something went wrong
            return BadRequest(result.Errors.Select(e => e.Description));
        }
    }
}
