using System.Collections.Generic;
using System.Threading.Tasks;
using BookstoreService.DataAccessLayer.Entities;
using BookstoreService.DataAccessLayer.Repositories;

namespace BookstoreService.BusinessLayer.Services
{
	public interface IBookService
	{
		Task<IEnumerable<Book>> GetAsync();
		Task<Book> GetById(int id);
		Task<int> AddAsync(Book book);
		Task DeleteAsync(int id);
		Task UpdateAsync(int id, Book newBook);
	}

	public class BookService : IBookService
	{
		private readonly IBookRepository _repository;

		public BookService(IBookRepository repository)
		{
			this._repository = repository;
		}

		public async Task<IEnumerable<Book>> GetAsync()
		{
			return await _repository.GetAsync();
		}

		public async Task<Book> GetById(int id)
		{
			return await _repository.GetById(id);
		}

		public async Task<int> AddAsync(Book book)
		{
			return await _repository.AddAsync(book);
		}

		public async Task DeleteAsync(int id)
		{
			await _repository.DeleteAsync(id);
		}

		public async Task UpdateAsync(int id, Book newBook)
		{
			await _repository.UpdateAsync(id, newBook);
		}
	}
}