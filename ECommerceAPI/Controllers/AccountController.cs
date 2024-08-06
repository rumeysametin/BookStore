using BookStore.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using System;
using Microsoft.AspNetCore.Authorization;

namespace ECommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model) //body
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.Password != model.ConfirmPassword)
            {
                return BadRequest("Passwords do not match.");
            }

            var user = new ApplicationUser
            {
                Email = model.Email,
                Name = model.Name,
                Surname = model.Surname,
                UserName = model.UserName,
            };

            var result = await _userManager.CreateAsync(user, model.Password);
        

            if (result.Succeeded)
                {
                var token = GenerateJwtToken(user);
                return Ok(new { Token = "Bearer " +token });
            }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return BadRequest(ModelState);
            }

            [HttpPost("Login")]
            public async Task<IActionResult> Login([FromBody] LoginModel model)
            {
       
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password,false,false);

            if (result.Succeeded)
                    {
                var user = await _userManager.FindByEmailAsync(model.Email);
                var token = GenerateJwtToken(user);
                    return Ok(new { Token = "Bearer " + token });
                }

                return Unauthorized("Invalid login attempt.");
            }


        private string GenerateJwtToken(ApplicationUser user)
            {
                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                   issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                   claims: claims,
                   expires: DateTime.Now.AddMinutes(30),
                   signingCredentials: creds);

                 return new JwtSecurityTokenHandler().WriteToken(token);
             }
    
     }
}
