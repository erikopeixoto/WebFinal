using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Modelos;
using AcessoDados.Repositorio;

namespace Servicos
{
    public class ProdutoServico
    {
        ProdutoRepositorio rep = new ProdutoRepositorio();
        public List<Produto> ObterProdutos(int pagina = 1, int linhas = 5)
        {
            return rep.ObterProdutos(pagina, linhas);
        }

        public Produto InstanciarProduto()
        {
            Produto produto = new Produto();

            return produto;
        }

        public int ObterQuantidadeProdutos()
        {
            return rep.ObterQuantidadeProdutos();
        }

        public Produto ObterProduto(int Pid)
        {
            return rep.ObterProduto(Pid);
        }

        public List<Produto> ObterPNome(string pNmProduto)
        {
            return rep.ObterPNome(pNmProduto);
        }

        public string ObterNomeProduto(long pId)
        {
            return rep.ObterNomeProduto(pId);
        }

        public Produto IncluirProduto(Produto Pproduto)
        {
            return rep.IncluirProduto(Pproduto);
        }

        public Produto AtualizarProduto(Produto Pproduto)
        {
            return rep.AtualizarProduto(Pproduto);
        }

        public void ExcluirProduto(Produto Pproduto)
        {
            rep.ExcluirProduto(Pproduto);
        }

        public IEnumerable<Produto> ObterProdCategoria(int Pid)
        {
            return rep.ObterProdCategoria(Pid);
        }
    }
}