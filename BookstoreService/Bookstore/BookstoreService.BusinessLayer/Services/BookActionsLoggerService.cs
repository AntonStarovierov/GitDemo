using System.Threading.Tasks;
using BookstoreService.Base.Dtos;
using BookstoreService.Base.ServiceCommunicator;
using Newtonsoft.Json;

namespace BookstoreService.BusinessLayer.Services
{
	public class BookActionsLoggerService : IBookActionsLoggerService
	{
		private readonly IServiceCommunicationFactory _factory;

		public BookActionsLoggerService(IServiceCommunicationFactory factory)
		{
			_factory = factory;
		}

		public async Task LogBookAction(string token, LogParameters parameters, string url)
		{
			using (var communicator = _factory.GetServiceCommunicator(token))
			{
				await communicator.PutAsync(url, JsonConvert.SerializeObject(parameters));
			}
		}
	}

	public interface IBookActionsLoggerService
	{
		Task LogBookAction(string token, LogParameters parameters, string url);
	}
}