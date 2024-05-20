using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using testeDealer.Models;
using testeDealer.Services;

namespace testeDealer.Controllers
{
    public class VendaController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Vendas
        public ActionResult Index(string search = null)
        {
            try
            {
                var vendas = VendaService.Find(search);
                return View(vendas.ToList());
            }
            catch (Exception ex)
            {
                // Tratar exceção e logar o erro
                ModelState.AddModelError("", "Não foi possível carregar os vendas: " + ex.Message);
                return View();
            }

        }

        // GET: Vendas/Create
        public ActionResult Create()
        {
            ViewBag.ClientesList = ClienteService.listarClientes();
            ViewBag.ProdutosList = ProdutoService.listarProdutos();
            return View();
        }

        // POST: Vendas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Venda venda)
        {
            try
            {
                ViewBag.ClientesList = ClienteService.listarClientes();
                ViewBag.ProdutosList = ProdutoService.listarProdutos();

                var valor = Session["ValorUnitario"];

                if (valor != null)
                {
                    venda.VlrUnitarioVenda = float.Parse(valor.ToString().Replace(".", ","));
                }

                if (VendaService.Save(venda))
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Não foi possível salvar o venda, tente mais tarde");
                }

                return View(venda);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Não foi possível salvar o venda: " + ex.Message);

                return View(venda);
            }

        }

        // GET: Vendas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                ModelState.AddModelError("", "Ouve um erro ao buscar informações do venda");
                return RedirectToAction("Index");
            }
            Venda venda = VendaService.FindbyId(id ?? 0);

            if (venda == null)
            {
                ViewBag.ClientesList = ClienteService.listarClientes();
                ViewBag.ProdutosList = ProdutoService.listarProdutos();

                ModelState.AddModelError("", "Não encontramos informações desse venda");
                return View();
            }
            else
            {
                ViewBag.ClientesList = ClienteService.listarClientes(venda.IdCliente);
                ViewBag.ProdutosList = ProdutoService.listarProdutos(venda.IdProduto);
                Session["IdVenda"] = venda.IdVenda;
                return View(venda);
            }

        }

        // POST: Vendas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Venda venda)
        {

            try
            {
                var valor = Session["ValorUnitario"];

                if (valor != null)
                {
                    venda.VlrUnitarioVenda = float.Parse(valor.ToString().Replace(".", ","));
                }

                var IdVenda = Session["IdVenda"];
                if (IdVenda != null)
                {
                    venda.IdVenda = Convert.ToInt32(IdVenda.ToString());
                }

                if (VendaService.Save(venda))
                {
                    return RedirectToAction("Index");
                }

                ViewBag.ClientesList = ClienteService.listarClientes(venda.IdCliente);
                ViewBag.ProdutosList = ProdutoService.listarProdutos(venda.IdProduto);

                return View(venda);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Não foi possível editar o venda: " + ex.Message);
                return View(venda);
            }

        }

        // GET: Vendas/Delete/5
        public ActionResult Delete(int? id)
        {

            if (id == null)
            {
                ModelState.AddModelError("", "Ouve um erro ao buscar informações do venda");
                return View();
            }

            Venda venda = VendaService.FindbyId(id ?? 0);

            if (venda == null)
            {
                ModelState.AddModelError("", "Não encontramos informações desse venda");
                return View();
            }
            else
                return View(venda);

        }

        // POST: Vendas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                if (VendaService.Delete(id))
                    return RedirectToAction("Index");
                else
                {
                    ModelState.AddModelError("", "Não foi possível excluir o venda, tente mais tarde");
                    return View();
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Não encontramos informações desse venda");
                return View();
            }

        }

        [HttpPost]
        public ActionResult SetValorUnitario(string valorUnitario)
        {
            Session["ValorUnitario"] = valorUnitario;

            return Json(new { success = true });
        }
    }
}