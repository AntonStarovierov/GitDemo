using System.Collections.Generic;
using System.Threading.Tasks;
using HistoryService.DataAccessLayer.Contexts;
using HistoryService.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace HistoryService.DataAccessLayer.Repositories
{
	public class HistoryRepository : IHistoryRepository
	{
		private readonly HistoryDbContext _context;

		public HistoryRepository(HistoryDbContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<AuthHistory>> GetAuthHistoryAsync()
		{
			return await _context.AuthenticationHistory.ToListAsync();
		}

		public async Task<IEnumerable<BookHistory>> GetBookstoreHistoryAsync()
		{
			return await _context.BookstoreHistory.ToListAsync();
		}

		public async Task AddAuthHistoryAsync(AuthHistory history)
		{
			_context.AuthenticationHistory.Add(history);
			await _context.SaveChangesAsync();
		}

		public async Task AddBookstoreHistoryAsync(BookHistory history)
		{
			_context.BookstoreHistory.Add(history);
			await _context.SaveChangesAsync();
		}
	}

	public interface IHistoryRepository
	{
		Task<IEnumerable<AuthHistory>> GetAuthHistoryAsync();
		Task<IEnumerable<BookHistory>> GetBookstoreHistoryAsync();
		Task AddAuthHistoryAsync(AuthHistory history);
		Task AddBookstoreHistoryAsync(BookHistory history);
	}
}