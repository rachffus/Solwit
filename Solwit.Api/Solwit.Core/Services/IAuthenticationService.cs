using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Solwit.Core.Services
{
	public interface IAuthenticationService
	{
		Task<string> Login(string login, string password);
	}
}
