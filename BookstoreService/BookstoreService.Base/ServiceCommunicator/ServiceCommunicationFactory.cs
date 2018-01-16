using System.Net.Http;

namespace BookstoreService.Base.ServiceCommunicator
{
	public class ServiceCommunicationFactory : IServiceCommunicationFactory
	{
		public IServiceCommunicator GetServiceCommunicator(string token)
		{
			var client = CreateHttpClient(token);
			var communicator = new ServiceCommunicator(client);
			return communicator;
		}

		private static HttpClient CreateHttpClient(string token = "")
		{
			var client = new HttpClient();
			if (!string.IsNullOrWhiteSpace(token))
			{
				client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
			}

			return client;
		}
	}

	public interface IServiceCommunicationFactory
	{
		IServiceCommunicator GetServiceCommunicator(string token);
	}
}