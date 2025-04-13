using Domain.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Persistance.Global;
using Shared.Dtos.Auth.Dtos;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Service.Abstraction
{
    public class AuthService : IAuthService
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JwtSettings _jwt;
        public AuthService(UserManager<ApplicationUser> userManager, IOptions<JwtSettings> jwt,RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager ;
            _jwt = jwt.Value;
            _roleManager = roleManager ;
        }

        public async Task<AuthDto> RegisterAsync(RegistrationDto model)
        {
            if (await _userManager.FindByEmailAsync(model.Email) is not null)
                return new AuthDto { Message = "Email is already registered!" };

            if (await _userManager.FindByNameAsync(model.Username) is not null)
                return new AuthDto { Message = "Username is already registered!" };

            var user = new ApplicationUser
            {
                UserName = model.Username,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,

            };
            // Adding User to the AspNetUsers Table with Hashed Password
            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                var errors = string.Empty;

                foreach (var error in result.Errors)
                {
                    errors += $"{error.Description},";
                }

                return new AuthDto { Message = errors };
            }
            // Adding Role User to the AspNetRoles Table 
            await _userManager.AddToRoleAsync(user, "User");
            
            return new AuthDto
            {
                Email = user.Email,
                IsAuthenticated = true,
                Roles = new List<string> { "User" },
                Username = user.UserName
            };
        }
        public async Task<AuthDto> LoginAsync(LoginDto model)
        {

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user is null || !await _userManager.CheckPasswordAsync(user, model.Password))
            {
                return new AuthDto() { Message = "Email or Password is incorrect!" };
            }

            var jwtSecurityToken = await CreateJwtToken(user);
            var rolesList = await _userManager.GetRolesAsync(user);

            return new AuthDto
            {
                IsAuthenticated = true,
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                Email = user.Email,
                Username = user.UserName,
                ExpiresOn = jwtSecurityToken.ValidTo,
                Roles = rolesList.ToList()
            };
        }
        private async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            foreach (var role in roles)
                roleClaims.Add(new Claim("roles", role));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(_jwt.DurationInDays),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }
        public async Task<string> AddRoleAsync(AddRoleDto model)
        {
            var user = await _userManager.FindByIdAsync(model.Id);

            if (user is null || !await _roleManager.RoleExistsAsync(model.Role))
                return "Invalid user ID or Role";

            if (await _userManager.IsInRoleAsync(user, model.Role))
                return "User already assigned to this role";

            var result = await _userManager.AddToRoleAsync(user, model.Role);

            return result.Succeeded ? string.Empty : "Sonething went wrong";
        }

        
    }
}
