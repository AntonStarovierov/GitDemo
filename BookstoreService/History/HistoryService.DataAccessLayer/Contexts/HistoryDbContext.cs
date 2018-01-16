using HistoryService.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace HistoryService.DataAccessLayer.Contexts
{
	public class HistoryDbContext : DbContext
	{
		public HistoryDbContext(DbContextOptions options) : base(options)
		{
		}

		public DbSet<AuthHistory> AuthenticationHistory { get; set; }
		public DbSet<BookHistory> BookstoreHistory { get; set; }
	}
}