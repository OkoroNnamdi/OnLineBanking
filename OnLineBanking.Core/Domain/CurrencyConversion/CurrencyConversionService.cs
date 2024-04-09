
using OnLineBanking.Core.DTO;
using System.Text.Json;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnLineBanking.Sercice.Implementation
{
    public class CurrencyConversionService : ICurrencyCoversionService
    {
        public readonly IHttpClientFactory _httpClientFactory;
        public CurrencyConversionService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<double> GetCurrencyApiAsync(string currency)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient();
                using (var response = await httpClient.GetAsync("https://open.er-api.com/v6/latest/NGN", HttpCompletionOption.ResponseHeadersRead))
                {
                    var con = currency.ToUpper();
                    response.EnsureSuccessStatusCode();
                    var stream = await response.Content.ReadAsStreamAsync();
                    var rates = await JsonSerializer.DeserializeAsync<CurrencyConversionApiDTO>(stream);
                    if (rates != null)
                        return rates.rates[con];
                }
            }
            catch (Exception)
            {
                throw;

            }
            return 0;
        }
    }
    
}
