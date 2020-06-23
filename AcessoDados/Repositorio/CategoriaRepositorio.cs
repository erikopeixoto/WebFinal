using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Modelos;
using System.Data.Entity;

namespace AcessoDados.Repositorio
{
    public class CategoriaRepositorio
    {
        public List<Categoria> ObterCategorias(int pagina = 1, int linhas = 5)
        {
            using (Contexto db = new Contexto())
            {
                return db.Categorias.OrderBy(p => p.NmCategoria).Skip((pagina - 1) * linhas).Take(linhas).ToList();
            }
        }

        public List<Categoria> ObterCategoriasT()
        {
            using (Contexto db = new Contexto())
            {
                return db.Categorias.ToList();
            }
        }

        public int ObterQuantidadeCategoria()
        {
            using (Contexto db = new Contexto())
            {
                return db.Categorias.Count();
            }
        }

        public Categoria ObterCategoria(int Pid)
        {
            using (Contexto db = new Contexto())
            {
                return db.Categorias.SingleOrDefault(p => p.ID == Pid);
            }
        }

        public Categoria ObterCategoria(string Pnome)
        {
            using (Contexto db = new Contexto())
            {
                return db.Categorias.SingleOrDefault(p => p.NmCategoria == Pnome);
            }
        }

        public Categoria IncluirCategoria(Categoria Pcategoria)
        {
            try
            {
                using (Contexto db = new Contexto())
                {
                    db.Entry(Pcategoria).State = EntityState.Added;
                    //var erros = db.GetValidationErrors();

                    db.SaveChanges();

                    return Pcategoria;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Categoria AtualizarCategoria(Categoria Pcategoria)
        {
            using (Contexto db = new Contexto())
            {
                db.Entry(Pcategoria).State = EntityState.Modified;

                db.SaveChanges();

                return Pcategoria;
            }
        }

        public void ExcluirCategoria(Categoria Pcategoria)
        {
            using (Contexto db = new Contexto())
            {
                db.Entry(Pcategoria).State = EntityState.Deleted;

                db.SaveChanges();
            }
        }

    }
}