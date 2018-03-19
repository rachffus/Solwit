using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Solwit.Core.Models;

namespace Solwit.Core.Services.Impl
{
	public class AuthenticationService : IAuthenticationService
	{
		private readonly UserManager<SolwitUser> _userManager;
		private readonly IConfiguration _configuration;

		public AuthenticationService(UserManager<SolwitUser> userManager, IConfiguration configuration)
		{
			_userManager = userManager;
			_configuration = configuration;
		}

		public async Task<string> Login(string login, string password)
		{
			var user = await _userManager.FindByNameAsync(login);
			if (user == null)
				return null;
			var passwordVerificationResult = _userManager.PasswordHasher
				.VerifyHashedPassword(user, user.PasswordHash, password);
			if (passwordVerificationResult == PasswordVerificationResult.Failed)
				return null;

			var token = GenerateEncodedToken(user);
			return token;
		}

		private string GenerateEncodedToken(SolwitUser user)
		{
			var claims = new List<Claim>
			{
				new Claim(JwtRegisteredClaimNames.Sub, user.Email),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
				new Claim(ClaimTypes.NameIdentifier, user.UserName),

			};

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtKey"]));
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
			var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["JwtExpireDays"]));

			var token = new JwtSecurityToken(
				_configuration["JwtIssuer"],
				_configuration["JwtIssuer"],
				claims,
				expires: expires,
				signingCredentials: creds
			);
			return new JwtSecurityTokenHandler().WriteToken(token);
		}

	}
}
