using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Modelos;
using System.Data.Entity;

namespace AcessoDados.Repositorio
{
    public class ItensNotaFiscalRepositorio
    {
        public List<ItensNotaFiscal> ObterItensNF(int pagina = 1, int linhas = 5)
        {
            using (Contexto db = new Contexto())
            {
                return db.ItensNotasFiscais.Include("Produto").OrderBy(p => p.ProdutoID).Skip((pagina - 1) * linhas).Take(linhas).ToList();
            }
        }

        public ItensNotaFiscal InstanciarItensNF()
        {

            ItensNotaFiscal ItensNF = new ItensNotaFiscal();
            return ItensNF;
        }

        public int ObterQuantidadeItensNF()
        {
            using (Contexto db = new Contexto())
            {
                try
                {
                    return db.NotasFiscais.Count();
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        public ItensNotaFiscal ObterItemNF(long Pid)
        {
            using (Contexto db = new Contexto())
            {
                return db.ItensNotasFiscais.Include("Produto").Include("Unidade").SingleOrDefault(p => p.ID == Pid);
            }
        }

        public IEnumerable<ItensNotaFiscal> ObterItensNFpNF(long PNFID)
        {
            using (Contexto db = new Contexto())
            {
                try
                {
                    return db.ItensNotasFiscais.Include("Produto").Where(p => p.NotaFiscalID == PNFID).ToList();
                }
                catch (Exception e)
                {
                    
                    throw e;
                }
            }
        }

        public Mensagem IncluirItensNF(ItensNotaFiscal PItensNF)
        {
            Mensagem msg = new Mensagem();
            msg.Codigo = 0;
            msg.Descricao = "Item NF gravado com sucesso";
            using (Contexto db = new Contexto())
            {
                try
                {
                    db.Entry(PItensNF).State = EntityState.Added;
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    msg.Codigo = 999;
                    msg.Descricao = "Erro gravar item: " + e.Message.ToString();
                }
            }
            return msg;
        }

        public Mensagem AtualizarItensNF(ItensNotaFiscal PItensNF)
        {
            Mensagem msg = new Mensagem();
            msg.Codigo = 0;
            msg.Descricao = "Item NF alterado com sucesso";
            using (Contexto db = new Contexto())
            {
                try
                {
                    db.Entry(PItensNF).State = EntityState.Modified;
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    msg.Codigo = 999;
                    msg.Descricao = "Erro alterar item: " + e.Message.ToString();
                }
            }
            return msg;
        }

        public Mensagem ExcluirItensNF(ItensNotaFiscal PItensNF)
        {
            Mensagem msg = new Mensagem();
            msg.Codigo = 0;
            msg.Descricao = "Item NF excluído com sucesso";

            using (Contexto db = new Contexto())
            {
                try
                {
                    db.Entry(PItensNF).State = EntityState.Deleted;
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    msg.Codigo = 999;
                    msg.Descricao = "Erro excluir item: " + e.Message.ToString();
                }
            }
            return msg;
        }

        public Mensagem ExcluirTodosItensNF(long pIDNF)
        {
            Mensagem msg = new Mensagem();
            msg.Codigo = 0;
            msg.Descricao = "Item NF excluído com sucesso";

            using (Contexto db = new Contexto())
            {
                try
                {
                    var SqlDel = db.Database.Connection.CreateCommand();
                    SqlDel.CommandText = "delete from itensnotafiscal where NotaFiscalID = " + pIDNF;
                    SqlDel.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    msg.Codigo = 999;
                    msg.Descricao = "Erro excluir item: " + e.Message.ToString();
                }
            }
            return msg;
        }

        public IEnumerable<ItensNotaFiscal> ObterItensNF(long pNFID)
        {
            using (Contexto db = new Contexto())
            {
                return db.ItensNotasFiscais.Include("Produto").ToList().Where(p => p.NotaFiscalID == pNFID);
            }
        }
    }
}