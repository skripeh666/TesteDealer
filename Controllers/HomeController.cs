using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using testeDealer.Models;
using testeDealer.Services;

namespace testeDealer.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ApiService apiService = new ApiService();

        public async Task<ActionResult> Index()
        {
            if (db.Clientes.Count() == 0)
            {
                var clientes = await apiService.GetClientesAsync();
                db.Clientes.AddRange(clientes);
            }
            
            if (db.Produtos.Count() == 0)
            {
                var produtos = await apiService.GetProdutosAsync();
                db.Produtos.AddRange(produtos);
            }
            
            if (db.Vendas.Count() == 0)
            {
                var vendas = await apiService.GetVendasAsync();
                db.Vendas.AddRange(vendas);
            }

            await db.SaveChangesAsync();

            ViewBag.UltimosClientes = db.Clientes.OrderByDescending(x => x.IdCliente).Take(5).ToList();
            ViewBag.UltimosProdutos = db.Produtos.OrderByDescending(x => x.IdProduto).Take(5).ToList();
            ViewBag.UltimasVendas = db.Vendas.OrderByDescending(x => x.IdVenda).Take(5).ToList();

            return View();
        }
    }
}