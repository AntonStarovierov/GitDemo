using System.Collections.Generic;
using System.Threading.Tasks;
using HistoryService.DataAccessLayer.Entities;
using HistoryService.DataAccessLayer.Repositories;

namespace HistoryService.BusinessLayer.Services
{
	public interface IHistoryService
	{
		Task<IEnumerable<AuthHistory>> GetAuthHistoryAsync();
		Task<IEnumerable<BookHistory>> GetBookstoreHistoryAsync();
		Task AddAuthHistoryAsync(AuthHistory history);
		Task AddBookstoreHistoryAsync(BookHistory history);
	}

	public class HistoryService : IHistoryService
	{
		private readonly IHistoryRepository _repository;

		public HistoryService(IHistoryRepository repository)
		{
			this._repository = repository;
		}

		public async Task<IEnumerable<AuthHistory>> GetAuthHistoryAsync()
		{
			return await _repository.GetAuthHistoryAsync();
		}

		public async Task<IEnumerable<BookHistory>> GetBookstoreHistoryAsync()
		{
			return await _repository.GetBookstoreHistoryAsync();
		}

		public async Task AddAuthHistoryAsync(AuthHistory history)
		{
			await _repository.AddAuthHistoryAsync(history);
		}

		public async Task AddBookstoreHistoryAsync(BookHistory history)
		{
			await _repository.AddBookstoreHistoryAsync(history);
		}
	}
}