using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Modelos;
using System.Data.Entity;

namespace AcessoDados.Repositorio
{
    public class NotaFiscalRepositorio
    {
        public List<NotaFiscal> ObterNotasFiscais(int pagina = 1, int linhas = 5)
        {
            using (Contexto db = new Contexto())
            {
                return db.NotasFiscais.OrderBy(p => p.NuNota).Skip((pagina - 1) * linhas).Take(linhas).ToList();
            }
        }

        public NotaFiscal InstanciarNotaFiscal()
        {

            NotaFiscal notaFiscal = new NotaFiscal();
            return notaFiscal;
        }

        public int ObterQuantidadeNotaFiscal()
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

        public NotaFiscal ObterNotaFiscal(int Pid)
        {
            using (Contexto db = new Contexto())
            {
                return db.NotasFiscais.SingleOrDefault(p => p.ID == Pid);
            }
        }

        public NotaFiscal ObterNotaFiscal(long PNuNota,
                                          int  PSeNota,
                                          int  PMdNota)
        {
            using (Contexto db = new Contexto())
            {
                try
                {
                    return db.NotasFiscais.Include("Cliente").SingleOrDefault(p => p.NuNota == PNuNota &&
                                                                p.SeNota == PSeNota &&
                                                                p.MdNota == PMdNota);
                }
                catch (Exception e)
                {
                    
                    throw e;
                }
            }
        }

        public long ObterNumeroNF(int PSeNota,
                                  int PMdNota)
        {
            using (Contexto db = new Contexto())
            {
                try
                {
                    return db.NotasFiscais.Where(p => p.SeNota == PSeNota &&
                                                      p.MdNota == PMdNota).Max(p => p.NuNota) + 1;
                }
                catch (Exception e)
                {

                    return Convert.ToInt64(1);
                }
            }
        }

        public Mensagem IncluirNotaFiscal(NotaFiscal PNotaFiscal)
        {
            Mensagem msg = new Mensagem();

            msg.Codigo = 0;
            msg.Descricao = "Nota Fiscal incluída com sucesso";

            using (Contexto db = new Contexto())
            {
                //db.NotasFiscais.Add(PNotaFiscal);
                try
                {
                    db.Entry(PNotaFiscal).State = EntityState.Added;
                    db.SaveChanges();
                    msg.Codigo = PNotaFiscal.ID;
                }
                catch (Exception e)
                {
                    msg.Codigo = 0;
                    msg.Descricao = "Erro ao incluir a Nota Fiscal: " + e.Message.ToString();
                }

                return msg;
            }
        }

        public Mensagem AtualizarNotaFiscal(NotaFiscal PNotaFiscal)
        {
            Mensagem msg = new Mensagem();

            msg.Codigo = PNotaFiscal.ID;
            msg.Descricao = "Nota Fiscal gravada com sucesso";

            using (Contexto db = new Contexto())
            {
                //db.NotasFiscais.Add(PNotaFiscal);
                try
                {
                    db.Entry(PNotaFiscal).State = EntityState.Modified;
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    msg.Codigo = 0;
                    msg.Descricao = "Erro ao gravar a Nota Fiscal: " + e.Message.ToString();
                }

                return msg;
            }
        }

        public Mensagem ExcluirNotaFiscal(NotaFiscal PNotaFiscal)
        {
            Mensagem msg = new Mensagem();
            ItensNotaFiscalRepositorio repItens = new ItensNotaFiscalRepositorio();

            msg.Codigo = 0;
            msg.Descricao = "Nota Fiscal excluída com sucesso";

            using (Contexto db = new Contexto())
            {
                
                try
                {
                    repItens.ExcluirTodosItensNF(PNotaFiscal.ID);

                    db.Entry(PNotaFiscal).State = EntityState.Deleted;
                    db.SaveChanges();

                }
                catch (Exception e)
                {
                    msg.Codigo = 999;
                    msg.Descricao = "Erro excluir Nota Fiscal: " + e.Message.ToString();
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