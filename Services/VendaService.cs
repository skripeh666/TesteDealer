using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using testeDealer.Models;

namespace testeDealer.Services
{
    public class VendaService
    {
        private static ApplicationDbContext db = new ApplicationDbContext();

        private static int countError = 0;

        public static List<Venda> Find(string search)
        {
            try
            {
                var vendas = from c in db.Vendas select c;
                if (!String.IsNullOrEmpty(search))
                {
                    vendas = vendas.Where(s => s.Cliente.NmCliente.Contains(search) || s.Produto.DscProduto.Contains(search));
                }
                return vendas.ToList();
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

        public static Venda FindbyId(int id)
        {
            try
            {
                Venda venda = db.Vendas.Find(id);
                return venda;
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

        public static bool Save(Venda venda)
        {
            try
            {

                db.Vendas.AddOrUpdate(venda);

                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                if (countError < 3)
                {
                    countError++;
                    return Save(venda);
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
                Venda venda = FindbyId(id);
                db.Vendas.Remove(venda);
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
    }
}