using System.Threading.Tasks;
using BookstoreService.Base.Dtos;
using BookstoreService.Base.ServiceCommunicator;
using Newtonsoft.Json;

namespace BookstoreService.BusinessLayer.Services
{
	public class BookActionsLoggerService : IBookActionsLoggerService
	{
		private readonly IServiceCommunicationFactory factory;

		public BookActionsLoggerService(IServiceCommunicationFactory factory)
		{
			this.factory = factory;
		}

		public async Task LogBookAction(string token, LogParameters parameters, string url)
		{
			using (var communicator = factory.GetServiceCommunicator(token))
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