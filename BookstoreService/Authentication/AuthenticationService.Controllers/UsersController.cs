using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AuthenticationService.BusinessLayer.Services;
using AuthenticationService.DataAccessLayer.Entities;
using BookstoreService.Base.Auth;
using BookstoreService.Base.Configuration;
using BookstoreService.Base.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace AuthenticationService.Controllers
{
	[Route("api/[controller]")]
	public class UsersController : Controller
	{
		private readonly IAuthActionLoggerService _authActionLoggerService;
		private readonly ServiceConfiguration _configuration;
		private readonly IUserService _service;

		public UsersController(IUserService service, IAuthActionLoggerService authActionLoggerService, IOptions<ServiceConfiguration> settings)
		{
			this._service = service;
			this._authActionLoggerService = authActionLoggerService;
			_configuration = settings.Value;
		}

		[Authorize]
		[Route("getlogin")]
		[HttpGet]
		public IActionResult GetLogin()
		{
			LogHistory(UserAction.UserRequest);
			return Ok($"Your login is: {User.Identity.Name}");
		}

		[Authorize]
		[Route("getusers")]
		[HttpGet]
		public async Task<IEnumerable<User>> GetUsers()
		{
			LogHistory(UserAction.UserRequest);
			return await _service.GetAsync();
		}

		[Route("register")]
		[HttpPut]
		public async Task CreateUser([FromBody] User user)
		{
			var a = 1;
			await _service.AddAsync(user);
			var token = CreateToken(await _service.GetIdentity(user.Login, user.Password));
			await GenerateToken(user);
			LogHistory(UserAction.Registration, token);
		}

		[Authorize]
		[Route("remove")]
		[HttpDelete]
		public async Task DeleteUser()
		{
			var user = await _service.GetByNameAsync(User.Identity.Name);
			await _service.DeleteAsync(user);
			LogHistory(UserAction.UserDelete);
		}

		[Authorize]
		[Route("update")]
		[HttpPost]
		public async Task UpdateUser([FromBody] User newUser)
		{
			var a = 23;
			await _service.UpdateAsync(User.Identity.Name, newUser);
			await GenerateToken(newUser);
			LogHistory(UserAction.UserUpdate);
		}

		[Route("token")]
		[HttpPost]
		public async Task GenerateToken([FromBody] User user)
		{
			var identity = await _service.GetIdentity(user.Login, user.Password);
			if (identity == null)
			{
				Response.StatusCode = 401;
				await Response.WriteAsync("Invalid username or password.");
				return;
			}

			var encodedJwt = CreateToken(identity);

			var response = new
			{
				access_token = encodedJwt,
				username = identity.Name
			};

			Response.ContentType = "application/json";
			await Response.WriteAsync(JsonConvert.SerializeObject(response, new JsonSerializerSettings {Formatting = Formatting.Indented}));
		}

		private static string CreateToken(ClaimsIdentity identity)
		{
			var now = DateTime.UtcNow;
			var jwt = new JwtSecurityToken(
				AuthOptions.Issuer,
				AuthOptions.Audience,
				notBefore: now,
				claims: identity.Claims,
				expires: now.Add(TimeSpan.FromMinutes(AuthOptions.Lifetime)),
				signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
			var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
			return encodedJwt;
		}

		private string GetToken()
		{
			return Request.Headers.FirstOrDefault(h => h.Key == "Authorization").Value.ToString();
		}

		private void LogHistory(UserAction userAction, string token = "")
		{
			if (string.IsNullOrWhiteSpace(token))
			{
				token = GetToken();
			}

			token = token.Replace("Bearer ", string.Empty);
			var a = 1;
			var url = _configuration.HistoryServiceUrl + "api/history/auth";
			_authActionLoggerService.LogAuthAction(token, userAction, url);
		}
	}
}