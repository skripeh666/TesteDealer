using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using testeDealer.Models;
using testeDealer.Services;

namespace testeDealer.Controllers
{
    public class ProdutoController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Produtos
        public ActionResult Index(string search)
        {
            try
            {
                var produtos = ProdutoService.Find(search);
                return View(produtos.ToList());
            }
            catch (Exception ex)
            {
                // Tratar exceção e logar o erro
                ModelState.AddModelError("", "Não foi possível carregar os produtos: " + ex.Message);
                return View();
            }
            
        }

        // GET: Produtos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Produtos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Produto produto)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    if (ProdutoService.Save(produto))
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Não foi possível salvar o produto, tente mais tarde");
                    }
                }
                return View(produto);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Não foi possível salvar o produto: " + ex.Message);

                return View(produto);
            }
        }

        // GET: Produtos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                ModelState.AddModelError("", "Ouve um erro ao buscar informações do produto");
                return View();
            }

            Produto produto = ProdutoService.FindbyId(id ?? 0);

            if (produto == null)
            {
                ModelState.AddModelError("", "Não encontramos informações desse produto");
                return View();
            }
            else
                return View(produto);

          
        }

        // POST: Produtos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Produto produto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (ProdutoService.Save(produto))
                    {
                        return RedirectToAction("Index");
                    }
                }
                return View(produto);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Não foi possível editar o produto: " + ex.Message);
                return View(produto);
            }

           
        }

        // GET: Produtos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                ModelState.AddModelError("", "Ouve um erro ao buscar informações do produto");
                return View();
            }

            Produto produto = ProdutoService.FindbyId(id ?? 0);

            if (produto == null)
            {
                ModelState.AddModelError("", "Não encontramos informações desse produto");
                return View();
            }
            else
                return View(produto);
            
        }

        // POST: Produtos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                if (ProdutoService.Delete(id))
                    return RedirectToAction("Index");
                else
                {
                    ModelState.AddModelError("", "Não foi possível excluir o produto, tente mais tarde");
                    return View();
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Não encontramos informações desse produto");
                return View();
            }

           
        }


    }
}