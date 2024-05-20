using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using testeDealer.Models;
using testeDealer.Services;

namespace testeDealer.Controllers
{
    public class ClienteController : Controller
    {



        // GET: Clientes
        public ActionResult Index(string search)
        {
            try
            {
                var clientes = ClienteService.Find(search);
                return View(clientes.ToList());
            }
            catch (Exception ex)
            {
                // Tratar exceção e logar o erro
                ModelState.AddModelError("", "Não foi possível carregar os clientes: " + ex.Message);
                return View();
            }
        }

        // GET: Clientes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Clientes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Cliente cliente)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    if (ClienteService.Save(cliente))
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Não foi possível salvar o cliente, tente mais tarde");
                    }
                }
                return View(cliente);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Não foi possível salvar o cliente: " + ex.Message);

                return View(cliente);
            }
        }

        // GET: Clientes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                ModelState.AddModelError("", "Ouve um erro ao buscar informações do cliente");
                return View();
            }

            Cliente cliente = ClienteService.FindbyId(id ?? 0);

            if (cliente == null)
            {
                ModelState.AddModelError("", "Não encontramos informações desse cliente");
                return View();
            }
            else
                return View(cliente);
        }

        // POST: Clientes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Cliente cliente)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (ClienteService.Save(cliente))
                    {
                        return RedirectToAction("Index");
                    }
                }
                return View(cliente);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Não foi possível editar o cliente: " + ex.Message);
                return View(cliente);
            }
        }

        // GET: Clientes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                ModelState.AddModelError("", "Ouve um erro ao buscar informações do cliente");
                return View();
            }

            Cliente cliente = ClienteService.FindbyId(id ?? 0);

            if (cliente == null)
            {
                ModelState.AddModelError("", "Não encontramos informações desse cliente");
                return View();
            }
            else
                return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                if(ClienteService.Delete(id))
                    return RedirectToAction("Index");
                else
                {
                    ModelState.AddModelError("", "Não foi possível excluir o cliente, tente mais tarde");
                    return View();
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Não encontramos informações desse cliente");
                return View();
            }
        }
    }


}