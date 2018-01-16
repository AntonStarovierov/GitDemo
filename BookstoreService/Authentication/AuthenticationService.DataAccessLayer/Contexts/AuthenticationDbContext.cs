using AuthenticationService.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationService.DataAccessLayer.Contexts
{
	public class AuthenticationDbContext : DbContext
	{
		public AuthenticationDbContext(DbContextOptions options) : base(options) { }

		public DbSet<User> Users { get; set; }
	}
}
