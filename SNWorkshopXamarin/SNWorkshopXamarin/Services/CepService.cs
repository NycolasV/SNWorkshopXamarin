using Newtonsoft.Json.Linq;
using SNWorkshopXamarin.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SNWorkshopXamarin.Services
{
    public class CepService
    {
        HttpClient HttpClient = new HttpClient();

        private async Task<T> HttpClientGetAsync<T>(string url)
        {
            var result = await HttpClient.GetAsync(url);

            if (result.IsSuccessStatusCode)
            {
                var responseContent = await result.Content.ReadAsStringAsync();
                var jToken = JToken.Parse(responseContent);

                return jToken.ToObject<T>();
            }

            return default;
        }

        public async Task<CepResponse> GetCepResponseAsync(string cep)
        {
            try
            {
                return await HttpClientGetAsync<CepResponse>($"https://viacep.com.br/ws/{cep}/json/");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("ERROR: " + ex.Message);
                return null;
            }
        }

    }
}
