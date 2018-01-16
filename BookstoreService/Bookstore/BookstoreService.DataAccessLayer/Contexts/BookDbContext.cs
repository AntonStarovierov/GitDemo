using BookstoreService.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookstoreService.DataAccessLayer.Contexts
{
	public class BookDbContext : DbContext
	{
		public BookDbContext(DbContextOptions options) : base(options) { }

		public DbSet<Book> Books { get; set; }
	}
}
