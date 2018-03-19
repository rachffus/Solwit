using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Solwit.Core.Models;

namespace Solwit.Core.Database
{
	public class SolwitDbContext : IdentityDbContext<SolwitUser>
	{
		public SolwitDbContext(DbContextOptions<SolwitDbContext> options) : base((DbContextOptions) options)
		{

		}
	}
}
