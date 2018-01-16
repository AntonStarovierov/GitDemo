using System.Collections.Generic;
using System.Threading.Tasks;
using AuthenticationService.DataAccessLayer.Contexts;
using AuthenticationService.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationService.DataAccessLayer.Repositories
{
	public class UserRepository : IUserRepository
	{
		private readonly AuthenticationDbContext context;

		public UserRepository(AuthenticationDbContext context)
		{
			this.context = context;
		}

		public async Task<IEnumerable<User>> GetAsync()
		{
			return await this.context.Users.ToListAsync();
		}

		public async Task AddAsync(User user)
		{
			this.context.Add(user);
			await this.context.SaveChangesAsync();
		}

		public async Task DeleteAsync(User user)
		{
			var attachedUser = await this.context.FindAsync<User>(user.Id);
			this.context.Remove(attachedUser);
			await this.context.SaveChangesAsync();
		}

		public async Task UpdateAsync(string oldUser, User newUser)
		{
			var existing = await this.GetByNameAsync(oldUser);
			existing.Login = newUser.Login;
			existing.Password = newUser.Password;
			existing.Email = newUser.Email;

			await this.context.SaveChangesAsync();
		}

		public async Task<User> GetByNameAsync(string identityName)
		{
			return await this.context.Users.FirstOrDefaultAsync(i => i.Login == identityName);
		}
	}

	public interface IUserRepository
	{
		Task<IEnumerable<User>> GetAsync();
		Task AddAsync(User user);
		Task DeleteAsync(User user);
		Task UpdateAsync(string oldUser, User newUser);
		Task<User> GetByNameAsync(string identityName);
	}
}
