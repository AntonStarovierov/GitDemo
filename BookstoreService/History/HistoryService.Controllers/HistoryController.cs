using System.Collections.Generic;
using System.Threading.Tasks;
using BookstoreService.Base.Dtos;
using HistoryService.BusinessLayer.Services;
using HistoryService.DataAccessLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HistoryService.Controllers
{
	[Route("api/[controller]")]
	public class HistoryController : Controller
	{
		private readonly IHistoryService _service;

		public HistoryController(IHistoryService service)
		{
			this._service = service;
		}

		[Authorize]
		[Route("auth")]
		[HttpGet]
		public async Task<IEnumerable<AuthHistory>> GetAuthHistory()
		{
			return await _service.GetAuthHistoryAsync();
		}

		[Authorize]
		[Route("book")]
		[HttpGet]
		public async Task<IEnumerable<BookHistory>> GetBookstoreHistory()
		{
			return await _service.GetBookstoreHistoryAsync();
		}


		[Authorize]
		[Route("auth")]
		[HttpPut]
		public async Task AddAuthHistory([FromBody] string action)
		{
			var history = new AuthHistory {UserLogin = User.Identity.Name, Action = action};
			await _service.AddAuthHistoryAsync(history);
		}

		[Authorize]
		[Route("book")]
		[HttpPut]
		public async Task AddBookstoreHistory([FromBody] LogParameters parameters)
		{
			var history = new BookHistory {UserLogin = User.Identity.Name, Action = parameters.UserAction, BookId = parameters.BookId};
			await _service.AddBookstoreHistoryAsync(history);
		}
	}
}