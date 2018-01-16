using System.Linq;
using BookstoreService.DataAccessLayer.Entities;

namespace BookstoreService.DataAccessLayer.Contexts.DbInit
{
	public class BookDbInitializer
	{
		public static void Initialize(BookDbContext context)
		{
			context.Database.EnsureCreated();

			if (context.Books.Any())
			{
				return;
			}

			var books = new[]
			{
				new Book { Title = "Book1", Author = "OM" },
				new Book { Title = "Book2", Author = "OM" },
				new Book { Title = "Book3", Author = "OM" },
				new Book { Title = "Book4", Author = "OM" },
				new Book { Title = "Book5", Author = "OM" },
				new Book { Title = "Book6", Author = "OM" },
				new Book { Title = "Book7", Author = "OM" },
				new Book { Title = "Book8", Author = "OM" }
			};

			foreach (var book in books)
			{
				context.Books.Add(book);
			}
			context.SaveChanges();
		}
	}
}
