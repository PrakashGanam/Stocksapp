using Microsoft.Extensions.Configuration;
using Stocksapp.ServiceContracts;
using System.Text.Json;

namespace Stocksapp.Services
{
    public class Finnhubservice : IFinnhubservice
    {
        private readonly IHttpClientFactory _httpclientfactory;

        private readonly IConfiguration _configuration;

        public Finnhubservice(IHttpClientFactory httpclientfactory, IConfiguration configuration)
        {
            _httpclientfactory = httpclientfactory;
            _configuration = configuration;
        }

        public async Task<Dictionary<string, Object>> GetStocksdata(string symbol) {
            using (HttpClient httpclient = _httpclientfactory.CreateClient()) { 
            HttpRequestMessage request = new HttpRequestMessage() {
                RequestUri = new Uri($"https://finnhub.io/api/v1/quote?symbol={symbol}&token={_configuration["FinnhubToken"]}"),
                Method = HttpMethod.Get
            };
           HttpResponseMessage response = await httpclient.SendAsync(request);

                Stream stream = response.Content.ReadAsStream();
                StreamReader reader = new StreamReader(stream);
                string resp = reader.ReadToEnd();

                Dictionary<string, object> respdic = JsonSerializer.Deserialize<Dictionary<string, Object>>(resp);

                if (respdic == null)
                    throw new InvalidOperationException("No response from finhubservice");

                return respdic;
            };
            return null;
        }

        public Dictionary<string, object> GetStockobj()
        {
            throw new NotImplementedException();
        }
    }
}
