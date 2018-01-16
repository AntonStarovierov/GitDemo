using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AuthenticationService.DataAccessLayer.Entities;
using AuthenticationService.DataAccessLayer.Repositories;

namespace AuthenticationService.BusinessLayer.Services
{
	public interface IUserService
	{
		Task<IEnumerable<User>> GetAsync();
		Task AddAsync(User user);
		Task DeleteAsync(User user);
		Task UpdateAsync(string oldlogin, User user);
		Task<User> GetByNameAsync(string identityName);
		Task<ClaimsIdentity> GetIdentity(string login, string password);
	}

	public class UserService : IUserService
	{
		private readonly IUserRepository repository;

		public UserService(IUserRepository repository)
		{
			this.repository = repository;
		}

		public async Task<IEnumerable<User>> GetAsync()
		{
			return await this.repository.GetAsync();
		}

		public async Task AddAsync(User user)
		{
			await this.repository.AddAsync(user);
		}

		public async Task DeleteAsync(User user)
		{
			await this.repository.DeleteAsync(user);
		}

		public async Task UpdateAsync(string oldUserName, User user)
		{
			await this.repository.UpdateAsync(oldUserName, user);
		}

		public async Task<User> GetByNameAsync(string identityName)
		{
			return await this.repository.GetByNameAsync(identityName);
		}

		public async Task<ClaimsIdentity> GetIdentity(string username, string password)
		{
			var users = await this.GetAsync();
			var person = users.FirstOrDefault(x => x.Login == username && x.Password == password);
			if (person == null)
			{
				return null;
			}

			var claims = new List<Claim>
			{
				new Claim(ClaimsIdentity.DefaultNameClaimType, person.Login)
			};

			var claimsIdentity = new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

			return claimsIdentity;
		}
	}
}
