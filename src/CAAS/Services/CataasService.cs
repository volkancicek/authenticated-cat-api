using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace CAAS.Services
{
    public interface ICataasService
    {
        Task<Byte[]> GetRandomCatImage();
    }
    public class CataasService : ICataasService
    {
        private readonly IHttpClientFactory _clientFactory;

        public CataasService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<Byte[]> GetRandomCatImage()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "https://cataas.com/cat");
            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var bytes = await response.Content.ReadAsByteArrayAsync();
                return bytes;
            }
            else
            {
                throw new Exception($"The request to the cataas API (https://cataas.com/cat) was not successful! Status: {response.StatusCode}");
            }
        }
    }
}
