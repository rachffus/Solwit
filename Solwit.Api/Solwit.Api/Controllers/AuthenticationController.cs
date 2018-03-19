using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Solwit.Api.Models.ViewModels;
using Solwit.Core.Services;

namespace Solwit.Api.Controllers
{
	[Route("api/[controller]")]
	public class AuthenticationController : Controller
	{
		private readonly IAuthenticationService _authenticationService;
		private readonly ILogger<AuthenticationController> _logger;

		public AuthenticationController(IAuthenticationService authenticationService, ILogger<AuthenticationController> logger)
		{
			_authenticationService = authenticationService;
			_logger = logger;
		}

		// POST api/authentication
		[HttpPost]
		public async Task<IActionResult> Post([FromBody]LoginViewModel model)
		{
			var result = await _authenticationService.Login(model.Username, model.Password);
			var resultLog = string.IsNullOrEmpty(result) ? "failed" : "success";
			_logger.LogInformation($"Login user. UserId: {model.Username} with: {resultLog}");
			return Ok(result);
		}
	}
}
