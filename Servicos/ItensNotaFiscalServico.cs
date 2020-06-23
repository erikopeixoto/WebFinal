using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Modelos;
using AcessoDados.Repositorio;

namespace Servicos
{
    public class ItensNotaFiscalServico
    {
        ItensNotaFiscalRepositorio rep = new ItensNotaFiscalRepositorio();
        public List<ItensNotaFiscal> ObterItensNF(int pagina = 1, int linhas = 5)
        {

            return rep.ObterItensNF(pagina, linhas);
        }

        public ItensNotaFiscal InstanciarItensNF()
        {

            ItensNotaFiscal ItensNF = new ItensNotaFiscal();
            return ItensNF;
        }

        public int ObterQuantidadeItensNF()
        {
            try
            {
            return rep.ObterQuantidadeItensNF();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public ItensNotaFiscal ObterItemNF(long Pid)
        {
            return rep.ObterItemNF(Pid);
        }

        public IEnumerable<ItensNotaFiscal> ObterItensNFpNF(long PNFID)
        {
                try
                {
                    return rep.ObterItensNFpNF(PNFID);
                }
                catch (Exception e)
                {
                    
                    throw e;
                }
        }

        public Mensagem IncluirItensNF(ItensNotaFiscal PItensNF)
        {
            Mensagem msg = new Mensagem();
            msg.Codigo = 0;
            msg.Descricao = "Item NF gravado com sucesso";
            try
            {
            rep.IncluirItensNF(PItensNF);
            }
            catch (Exception e)
            {
                msg.Codigo = 999;
                msg.Descricao = "Erro gravar item: " + e.Message.ToString();
            }
            return msg;
        }

        public Mensagem AtualizarItensNF(ItensNotaFiscal PItensNF)
        {
            Mensagem msg = new Mensagem();
            msg.Codigo = 0;
            msg.Descricao = "Item NF alterado com sucesso";
            try
            {
                rep.AtualizarItensNF(PItensNF);
            }
            catch (Exception e)
            {
                msg.Codigo = 999;
                msg.Descricao = "Erro alterar item: " + e.Message.ToString();
            }
            return msg;
        }

        public Mensagem ExcluirItensNF(ItensNotaFiscal PItensNF)
        {
            Mensagem msg = new Mensagem();
            msg.Codigo = 0;
            msg.Descricao = "Item NF excluído com sucesso";

            try
            {
                rep.ExcluirItensNF(PItensNF);
            }
            catch (Exception e)
            {
                msg.Codigo = 999;
                msg.Descricao = "Erro excluir item: " + e.Message.ToString();
            }
            return msg;
        }

        public Mensagem ExcluirTodosItensNF(long pIDNF)
        {
            Mensagem msg = new Mensagem();
            msg.Codigo = 0;
            msg.Descricao = "Item NF excluído com sucesso";

            try
            {
                rep.ExcluirTodosItensNF(pIDNF);
            }
            catch (Exception e)
            {
                msg.Codigo = 999;
                msg.Descricao = "Erro excluir item: " + e.Message.ToString();
            }
            return msg;
        }

        public IEnumerable<ItensNotaFiscal> ObterItensNF(long pNFID)
        {
            return rep.ObterItensNF(pNFID);
        }
    }
}