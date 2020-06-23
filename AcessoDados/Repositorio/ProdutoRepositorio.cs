using System;
using System.Collections.Generic;
using System.Linq;
using Modelos;
using System.Data.Entity;

namespace AcessoDados.Repositorio
{
    public class ProdutoRepositorio
    {
        public List<Produto> ObterProdutos(int pagina = 1, int linhas = 5)
        {
            using (Contexto db = new Contexto())
            {
                //db.Produtos.Include("Usuario").OrderBy(p => p.nmProduto).Skip((pagina - 1) * linhas).Take(linhas).ToList() ---  LAZY
                
                /*
                var query = db.Produtos; //.Where(d => d.Nome == "Desenvolvimento");
                var dev = query.Single();
                db.Entry(dev).Collection("Usuario").Load();
                Explicit  */
                return db.Produtos.Include("Usuario").Include("Categoria").OrderBy(p => p.nmProduto).Skip((pagina - 1) * linhas).Take(linhas).ToList();
            }
        }

        public Produto InstanciarProduto()
        {
            Produto produto = new Produto();

            return produto;
        }

        public int ObterQuantidadeProdutos()
        {
            using (Contexto db = new Contexto())
            {
                try
                {
                    return db.Produtos.Count();
                }
                catch (Exception e)
                {
                    throw e;
                }
            }

        }

        public Produto ObterProduto(int Pid)
        {
            using (Contexto db = new Contexto())
            {
                return db.Produtos.Include("Usuario").Include("Unidade").Include("Categoria").SingleOrDefault(p => p.ID == Pid);
            }
        }

        public List<Produto> ObterPNome(string pNmProduto)
        {
            using (Contexto db = new Contexto())
            {
                try
                {
                    return db.Produtos.Include("Unidade").Where(p => p.nmProduto.Contains(pNmProduto)).ToList();
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        public string ObterNomeProduto(long pId)
        {
            using (Contexto db = new Contexto())
            {
                try
                {
                    return db.Produtos.SingleOrDefault(p => p.ID == pId).nmProduto;
                }
                catch (Exception e)
                {
                    return "";
                }
            }
        }

        public Produto IncluirProduto(Produto Pproduto)
        {
            using (Contexto db = new Contexto())
            {
                db.Entry(Pproduto).State = EntityState.Added;

                db.SaveChanges();

                return Pproduto;
            }
        }

        public Produto AtualizarProduto(Produto Pproduto)
        {
            using (Contexto db = new Contexto())
            {
                db.Entry(Pproduto).State = EntityState.Modified;

                db.SaveChanges();

                return Pproduto;
            }
        }

        public void ExcluirProduto(Produto Pproduto)
        {
            using (Contexto db = new Contexto())
            {
                db.Entry(Pproduto).State = EntityState.Deleted;

                db.SaveChanges();
            }
        }

        public IEnumerable<Produto> ObterProdCategoria(int Pid)
        {
            using (Contexto db = new Contexto())
            {
                return db.Produtos.ToList().Where(p => p.CategoriaId == Pid);
            }
        }
    }
}