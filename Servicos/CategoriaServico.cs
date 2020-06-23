using System;
using System.Collections.Generic;
using Modelos;
using AcessoDados.Repositorio;

namespace Servicos
{
    public class CategoriaServico
    {
        CategoriaRepositorio rep = new CategoriaRepositorio();
        public List<Categoria> ObterCategorias(int pagina = 1, int linhas = 5)
        {
            return rep.ObterCategorias(pagina, linhas);
        }
        public List<Categoria> ObterCategoriasT()
        {
            return rep.ObterCategoriasT();
        }

        public int ObterQuantidadeCategoria()
        {
            return rep.ObterQuantidadeCategoria();
        }

        public Categoria ObterCategoria(int Pid)
        {
            return rep.ObterCategoria(Pid);
        }

        public Categoria ObterCategoria(string Pnome)
        {
            return rep.ObterCategoria(Pnome);
        }

        public Categoria IncluirCategoria(Categoria Pcategoria)
        {
            Categoria cat = new Categoria();
            try
            {
                cat = rep.IncluirCategoria(Pcategoria);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return cat;
        }

        public Categoria AtualizarCategoria(Categoria Pcategoria)
        {
            Categoria cat = new Categoria();
            try
            {
                cat = rep.AtualizarCategoria(Pcategoria);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return cat;
        }

        public void ExcluirCategoria(Categoria Pcategoria)
        {
            rep.ExcluirCategoria(Pcategoria);
        }
    }
}
