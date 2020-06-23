using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Modelos;
using System.Data.Entity;

namespace AcessoDados.Repositorio
{
    public class ClienteRepositorio
    {
        public List<Cliente> ObterClientes(int pagina = 1, int linhas = 5)
        {
            using (Contexto db = new Contexto())
            {
                try
                {
                    /*
                    var clientes = db.Clientes.Select(p => new 
                    {
                        ID = p.ID,
                        NmCliente = p.NmCliente,
                        NuCep = p.NuCep,
                        NuCnpjCpf = p.NuCnpjCpf,
                        NuTelefone = p.NuTelefone,
                        TpCliente = p.TpCliente
                    }).OrderBy(p => p.NmCliente).Skip((pagina - 1) * linhas).Take(linhas).ToList();
                     */

                    return db.Clientes.OrderBy(p => p.NmCliente).Skip((pagina - 1) * linhas).Take(linhas).ToList();
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        public Cliente InstanciarCliente()
        {

            Cliente cliente = new Cliente();
            return cliente;
        }

        public int ObterQuantidadeClientes()
        {
            using (Contexto db = new Contexto())
            {
                try
                {
                    return db.Clientes.Count();
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        public Cliente ObterCliente(int Pid)
        {
            using (Contexto db = new Contexto())
            {
                //var consulta = from p in db.Clientes
                //          where (p.ID == Pid)
                //          select new { p.ID, p.NmCliente, p.NuCep, p.NuCnpjCpf, p.NuTelefone, p.TpCliente };
                //return cliente;

                return db.Clientes.SingleOrDefault(p => p.ID == Pid);
            }
        }

        public List<Cliente> ObterPNome(string pNmCliente, long pNuCnpjCpf)
        {
            using (Contexto db = new Contexto())
            {
                try
                {
                    if (pNuCnpjCpf == 0)
                        return db.Clientes.Where(p => p.NmCliente.Contains(pNmCliente)).ToList();
                    else
                        return db.Clientes.Where(p => p.NuCnpjCpf.Equals(pNuCnpjCpf)).ToList();
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        public string ObterNomeCliente(long pId)
        {
            string retorno = "";
            using (Contexto db = new Contexto())
            {
                try
                {
                    Cliente cliente = new Cliente();
                    cliente = db.Clientes.SingleOrDefault(p => p.ID == pId);
                    retorno = cliente.CPFCNPJFormatado + " " + cliente.NmCliente;
                    return retorno;
                }
                catch (Exception e)
                {
                    return "Cliente não encontrado";
                }
            }
        }

        public Cliente IncluirCliente(Cliente PCliente)
        {
            using (Contexto db = new Contexto())
            {
                db.Entry(PCliente).State = EntityState.Added;
                db.SaveChanges();
                return PCliente;
            }
        }

        public Cliente AtualizarCliente(Cliente PCliente)
        {
            using (Contexto db = new Contexto())
            {
                db.Entry(PCliente).State = EntityState.Modified;
                db.SaveChanges();

                return PCliente;
            }
        }

        public void ExcluirCliente(Cliente PCliente)
        {
            using (Contexto db = new Contexto())
            {
                db.Entry(PCliente).State = EntityState.Deleted;
                db.SaveChanges();
            }
        }
    }
}