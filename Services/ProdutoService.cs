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
    public class ProdutoService
    {
        private static ApplicationDbContext db = new ApplicationDbContext();

        private static int countError = 0;

        public static List<Produto> Find(string search = null)
        {
            try
            {
                var produtos = from c in db.Produtos select c;
                if (!String.IsNullOrEmpty(search))
                {
                    produtos = produtos.Where(s => s.DscProduto.Contains(search));
                }
                return produtos.ToList();
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

        public static Produto FindbyId(int id)
        {
            try
            {
                Produto produto = db.Produtos.Find(id);
                return produto;
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

        public static bool Save(Produto produto)
        {
            try
            {
                db.Produtos.AddOrUpdate(produto);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                if (countError < 3)
                {
                    countError++;
                    return Save(produto);
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
                Produto produto = FindbyId(id);
                db.Produtos.Remove(produto);
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

        public static SelectList listarProdutos(int idProduto = 0)
        {
            if(idProduto == 0)
                return new SelectList(Find(), "IdProduto", "DscProduto"); 
            else
                return new SelectList(Find(), "IdProduto", "DscProduto",idProduto); 
        }
    }
}