using System;
using System.Collections.Generic;
using Modelos;
using AcessoDados.Repositorio;

namespace Servicos
{
    public class NotaFiscalServico
    {
        NotaFiscalRepositorio rep = new NotaFiscalRepositorio();
        public List<NotaFiscal> ObterNotasFiscais(int pagina = 1, int linhas = 5)
        {
            return rep.ObterNotasFiscais(pagina, linhas);
        }

        public NotaFiscal InstanciarNotaFiscal()
        {

            NotaFiscal notaFiscal = new NotaFiscal();
            return notaFiscal;
        }

        public int ObterQuantidadeNotaFiscal()
        {
            try
            {
                return rep.ObterQuantidadeNotaFiscal();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public NotaFiscal ObterNotaFiscal(int Pid)
        {
            return rep.ObterNotaFiscal(Pid);
        }

        public NotaFiscal ObterNotaFiscal(long PNuNota,
                                          int  PSeNota,
                                          int  PMdNota)
        {
            try
            {
                return rep.ObterNotaFiscal(PNuNota, PSeNota, PMdNota);
            }
            catch (Exception e)
            {
                    
                throw e;
            }
        }

        public long ObterNumeroNF(int PSeNota,
                                  int PMdNota)
        {
                try
                {
                    return rep.ObterNumeroNF(PSeNota, PMdNota);
                }
                catch (Exception e)
                {

                    return Convert.ToInt64(1);
                }
        }

        public Mensagem IncluirNotaFiscal(NotaFiscal PNotaFiscal)
        {
            Mensagem msg = new Mensagem();

            msg.Codigo = 0;
            msg.Descricao = "Nota Fiscal incluída com sucesso";

            try
            {
                rep.IncluirNotaFiscal(PNotaFiscal);
                msg.Codigo = PNotaFiscal.ID;
            }
            catch (Exception e)
            {
                msg.Codigo = 0;
                msg.Descricao = "Erro ao incluir a Nota Fiscal: " + e.Message.ToString();
            }

            return msg;
        }

        public Mensagem AtualizarNotaFiscal(NotaFiscal PNotaFiscal)
        {
            Mensagem msg = new Mensagem();

            msg.Codigo = PNotaFiscal.ID;
            msg.Descricao = "Nota Fiscal gravada com sucesso";

            try
            {
                rep.AtualizarNotaFiscal(PNotaFiscal);
            }
            catch (Exception e)
            {
                msg.Codigo = 0;
                msg.Descricao = "Erro ao gravar a Nota Fiscal: " + e.Message.ToString();
            }

            return msg;
        }

        public Mensagem ExcluirNotaFiscal(NotaFiscal PNotaFiscal)
        {
            Mensagem msg = new Mensagem();
            ItensNotaFiscalRepositorio repItens = new ItensNotaFiscalRepositorio();

            msg.Codigo = 0;
            msg.Descricao = "Nota Fiscal excluída com sucesso";

            try
            {
                repItens.ExcluirTodosItensNF(PNotaFiscal.ID);
                rep.ExcluirNotaFiscal(PNotaFiscal);
            }
            catch (Exception e)
            {
                msg.Codigo = 999;
                msg.Descricao = "Erro excluir Nota Fiscal: " + e.Message.ToString();
            }
            return msg;
        }

        public IEnumerable<ItensNotaFiscal> ObterItensNF(long pNFID)
        {
            return rep.ObterItensNF(pNFID);
        }
    }
}