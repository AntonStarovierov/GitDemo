using System.Threading.Tasks;
using BookstoreService.Base.Enums;
using BookstoreService.Base.ServiceCommunicator;
using Newtonsoft.Json;

namespace AuthenticationService.BusinessLayer.Services
{
	public class AuthActionsLoggerService : IAuthActionLoggerService
	{
		private readonly IServiceCommunicationFactory factory;

		public AuthActionsLoggerService(IServiceCommunicationFactory factory)
		{
			this.factory = factory;
		}

		public async Task LogAuthAction(string token, UserAction action, string url)
		{
			using (var communicator = factory.GetServiceCommunicator(token))
			{
				await communicator.PutAsync(url, JsonConvert.SerializeObject(action.ToString()));
			}
		}
	}

	public interface IAuthActionLoggerService
	{
		Task LogAuthAction(string token, UserAction action, string url);
	}
}