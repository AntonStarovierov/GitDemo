using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreService.Base.ServiceCommunicator
{
	public class ServiceCommunicator : IServiceCommunicator
	{
		private readonly HttpClient _httpClient;

		public ServiceCommunicator(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public async Task<string> GetAsync(string url)
		{
			var a = 2;
			var response = await _httpClient.GetAsync(url);
			return await response.Content.ReadAsStringAsync();
		}

		public async Task<string> PutAsync(string url, string objectJson)
		{
			var content = new StringContent(objectJson, Encoding.UTF8, "application/json");
			var response = await _httpClient.PutAsync(url, content);
			return await response.Content.ReadAsStringAsync();
		}

		public async Task<string> PostAsync(string url, string objectJson)
		{
			var content = new StringContent(objectJson, Encoding.UTF8, "application/json");
			var response = await _httpClient.PostAsync(url, content);
			return await response.Content.ReadAsStringAsync();
		}

		public async Task<string> DeleteAsync(string url)
		{
			var response = await _httpClient.DeleteAsync(url);
			return await response.Content.ReadAsStringAsync();
		}

		public void Dispose()
		{
			_httpClient?.Dispose();
		}
	}

	public interface IServiceCommunicator : IDisposable
	{
		Task<string> GetAsync(string url);
		Task<string> PutAsync(string url, string objectJson);
		Task<string> PostAsync(string url, string objectJson);
		Task<string> DeleteAsync(string url);
	}
}
