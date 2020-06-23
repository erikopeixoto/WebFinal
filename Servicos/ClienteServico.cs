using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Modelos;
using AcessoDados.Repositorio;

namespace Servicos
{
    public class ClienteServico
    {
        ClienteRepositorio rep = new ClienteRepositorio();
        public List<Cliente> ObterClientes(int pagina = 1, int linhas = 5)
        {
            return rep.ObterClientes(pagina, linhas);
        }

        public Cliente InstanciarCliente()
        {

            Cliente cliente = new Cliente();
            return cliente;
        }

        public int ObterQuantidadeClientes()
        {
            return rep.ObterQuantidadeClientes();
        }

        public Cliente ObterCliente(int Pid)
        {
            return rep.ObterCliente(Pid);
        }

        public List<Cliente> ObterPNome(string pNmCliente, long pNuCnpjCpf)
        {
            return rep.ObterPNome(pNmCliente, pNuCnpjCpf);
        }

        public string ObterNomeCliente(long pId)
        {
            return rep.ObterNomeCliente(pId);
        }

        public Cliente IncluirCliente(Cliente PCliente)
        {
            return rep.IncluirCliente(PCliente);
        }

        public Cliente AtualizarCliente(Cliente PCliente)
        {
            return rep.AtualizarCliente(PCliente);
        }

        public void ExcluirCliente(Cliente PCliente)
        {
            rep.ExcluirCliente(PCliente);
        }
    }
}