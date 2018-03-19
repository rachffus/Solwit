using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Solwit.Api.Controllers
{
	[Route("api/[controller]")]
	public class LogController : Controller
	{
		private readonly ILogger<LogController> _logger;
		public LogController(ILogger<LogController> logger)
		{
			_logger = logger;
		}

		// GET api/values
		[HttpGet]
		public async Task<IEnumerable<string>> Get()
		{
			_logger.LogInformation("Hello");
			List<string> logs = new List<string>();
			var date = DateTime.Now.Date.ToString("yyyy-MM-dd");
			FileStream fileStream = new FileStream($"c:\\temp\\nlog-own-{date}.log", FileMode.Open);
			using (StreamReader reader = new StreamReader(fileStream))
			{
				string line = "";
				while ((line = await reader.ReadLineAsync()) != null)
				{
					logs.Add(line);
				}
			}
			return logs;
		}
	}
}