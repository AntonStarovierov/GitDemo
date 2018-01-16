using System.Linq;
using AuthenticationService.DataAccessLayer.Entities;

namespace AuthenticationService.DataAccessLayer.Contexts.DbInit
{
	public class AuthenticationDbInitializer
	{
		public static void Initialize(AuthenticationDbContext context)
		{
			context.Database.EnsureCreated();

			if (context.Users.Any())
			{
				return;
			}

			var users = new[]
			{
				new User{Login = "Admin", Password = "Admin", Email = "AdminAdmin@gmail.com"}
			};

			foreach (var usr in users)
			{
				context.Users.Add(usr);
			}
			context.SaveChanges();
		}
	}
}
