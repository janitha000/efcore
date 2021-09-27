using EFCore.Application.Interfaces.SeviceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EFCore.Application.Services
{
    public class BitCoinService : IBitCoinService
    {
        private IHttpClientFactory _clientFactory;

        public enum Currency
        {
            USD,
            GBP,
            EUR
        }

        public BitCoinService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<double> GetExchangeRate(Currency currency)
        {
            var client = _clientFactory.CreateClient("BitCoinClient");
            double rate;
            try
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, string.Empty);

                var response = await client.SendAsync(request);
                string responseJson = await response.Content.ReadAsStringAsync();

                //var response = await client.GetStringAsync(BITCOIN_CURRENTPRICE_URL);
                //var jsonDoc = JsonDocument.Parse(Encoding.ASCII.GetBytes(response));

                //var rateStr = jsonDoc.RootElement.GetProperty("bpi").GetProperty(currency.ToString()).GetProperty("rate");

                rate = Double.Parse(rateStr.GetString());
            }
            catch
            {
                rate = -1;
            }

            return Math.Round(rate, 4);
        }
    }
}
