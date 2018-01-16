using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookstoreService.Base.Configuration;
using BookstoreService.Base.Dtos;
using BookstoreService.Base.Enums;
using BookstoreService.BusinessLayer.Services;
using BookstoreService.DataAccessLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace BookstoreService.Controllers
{
	[Route("api/[controller]")]
	public class BookController : Controller
	{
		private readonly IBookActionsLoggerService _bookActionLogger;
		private readonly ServiceConfiguration _configuration;
		private readonly IBookService _service;

		public BookController(IBookService service, IBookActionsLoggerService bookActionLogger, IOptions<ServiceConfiguration> settings)
		{
			_service = service;
			_bookActionLogger = bookActionLogger;
			_configuration = settings.Value;
		}

		[Authorize]
		[Route("{id:int}")]
		[HttpGet]
		public async Task<Book> GetBook(int id)
		{
			LogHistory(new LogParameters {UserAction = UserAction.BookRequest.ToString(), BookId = id});
			return await _service.GetById(id);
		}

		[Authorize]
		[HttpGet]
		public async Task<IEnumerable<Book>> GetBooks()
		{
			LogHistory(new LogParameters {UserAction = UserAction.BookRequest.ToString(), BookId = 0});
			return await _service.GetAsync();
		}

		[Authorize]
		[HttpPut]
		public async Task CreateBook([FromBody] Book book)
		{
			var id = await _service.AddAsync(book);
			LogHistory(new LogParameters {UserAction = UserAction.BookCreate.ToString(), BookId = id});
		}

		[Authorize]
		[Route("{id:int}")]
		[HttpDelete]
		public async Task DeleteBook(int id)
		{
			await _service.DeleteAsync(id);
			LogHistory(new LogParameters {UserAction = UserAction.BookDelete.ToString(), BookId = id});
		}

		[Authorize]
		[Route("{id:int}")]
		[HttpPost]
		public async Task UpdateBook(int id, [FromBody] Book newBook)
		{
			await _service.UpdateAsync(id, newBook);
			LogHistory(new LogParameters {UserAction = UserAction.BookUpdate.ToString(), BookId = id});
		}


		private string GetToken()
		{
			return Request.Headers.FirstOrDefault(h => h.Key == "Authorization").Value.ToString();
		}

		private void LogHistory(LogParameters parameters)
		{
			var token = GetToken();

			token = token.Replace("Bearer ", string.Empty);

			var url = _configuration.HistoryServiceUrl + "api/history/book";
			_bookActionLogger.LogBookAction(token, parameters, url);
		}
	}
}