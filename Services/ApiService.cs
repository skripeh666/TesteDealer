using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using testeDealer.Models;

namespace testeDealer.Services
{
    public class ApiService
    {
        private static readonly HttpClient client = new HttpClient();

        public async Task<List<Cliente>> GetClientesAsync()
        {
            HttpResponseMessage response = await client.GetAsync("https://camposdealer.dev/Sites/TesteAPI/cliente");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();

            var resp = System.Web.HttpUtility.UrlDecode(json).Replace("\"[", "").Replace("]\"", "").Replace("},", "};").Split(';');

            List<Cliente> lista = new List<Cliente>();
            foreach (var item in resp)
            {
                lista.Add(JsonConvert.DeserializeObject<Cliente>(item.Replace("\\","")));
            }

            return lista;
        }

        public async Task<List<Produto>> GetProdutosAsync()
        {
            HttpResponseMessage response = await client.GetAsync("https://camposdealer.dev/Sites/TesteAPI/produto");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            var resp = System.Web.HttpUtility.UrlDecode(json).Replace("\"[", "").Replace("]\"", "").Replace("},", "};").Split(';');

            List<Produto> lista = new List<Produto>();
            foreach (var item in resp)
            {
                lista.Add(JsonConvert.DeserializeObject<Produto>(item.Replace("\\", "")));
            }

            return lista;

            
        }

        public async Task<List<Venda>> GetVendasAsync()
        {
            HttpResponseMessage response = await client.GetAsync("https://camposdealer.dev/Sites/TesteAPI/venda");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            var resp = System.Web.HttpUtility.UrlDecode(json).Replace("\"[", "").Replace("]\"", "").Replace("},", "};").Split(';');

            List<Venda> lista = new List<Venda>();
            foreach (var item in resp)
            {
                lista.Add(JsonConvert.DeserializeObject<Venda>(item.Replace("\\", "")));
            }

            return lista;

        }
    }
}