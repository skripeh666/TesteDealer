using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using testeDealer.Models;

namespace testeDealer.Services
{
    public class ClienteService
    {
        private static ApplicationDbContext db = new ApplicationDbContext();

        private static int countError = 0;

        public static List<Cliente> Find(string search = null)
        {
            try
            {
                var clientes = from c in db.Clientes select c;
                if (!String.IsNullOrEmpty(search))
                {
                    clientes = clientes.Where(s => s.NmCliente.Contains(search));
                }
                return clientes.ToList();
            }
            catch (Exception ex)
            {
                if (countError < 3)
                {
                    countError++;
                    return Find(search);
                }
                else
                {
                    countError = 0;
                    throw ex;

                }

            }
        }

        public static Cliente FindbyId(int id)
        {
            try
            {
                Cliente cliente = db.Clientes.Find(id);
                return cliente;
            }
            catch (Exception ex)
            {
                if (countError < 3)
                {
                    countError++;
                    return FindbyId(id);
                }
                else
                {
                    countError = 0;
                    throw ex;
                }

            }
        }

        public static bool Save(Cliente cliente)
        {
            try
            {

                db.Clientes.AddOrUpdate(cliente);

                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                if (countError < 3)
                {
                    countError++;
                    return Save(cliente);
                }
                else
                {
                    countError = 0;
                    throw ex;
                }
            }
        }

        public static bool Delete(int id)
        {
            try
            {
                Cliente cliente = FindbyId(id);
                db.Clientes.Remove(cliente);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                if (countError < 3)
                {
                    countError++;
                    return Delete(id);
                }
                else
                {
                    countError = 0;
                    throw ex;
                }
            }
        }

        public static SelectList listarClientes(int idCliente = 0)
        {
            if (idCliente == 0)
                return new SelectList(Find(), "IdCliente", "NmCliente");
            else
                return new SelectList(Find(), "IdCliente", "NmCliente", idCliente);
        }
    }
}