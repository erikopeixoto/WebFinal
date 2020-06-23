using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Modelos;
using System.Data.Entity;

namespace AcessoDados.Repositorio
{
    public class UnidadeRepositorio
    {
        public List<Unidade> ObterUnidades(int pagina = 1, int linhas = 5)
        {
            using (Contexto db = new Contexto())
            {
                return db.Unidades.ToList().Skip((pagina - 1) * linhas).Take(linhas).ToList();
            }
        }

        public List<Unidade> ObterUnidadesT()
        {
            using (Contexto db = new Contexto())
            {
                return db.Unidades.ToList();
            }
        }

        public int ObterQuantidadeUnidade()
        {
            using (Contexto db = new Contexto())
            {
                return db.Unidades.Count();
            }
        }

        public Unidade ObterUnidade(string Psg)
        {
            using (Contexto db = new Contexto())
            {
                return db.Unidades.SingleOrDefault(p => p.SgUnidade == Psg);
            }
        }

        public Unidade IncluirUnidade(Unidade PUnidade)
        {
            try
            {
                using (Contexto db = new Contexto())
                {
                    db.Entry(PUnidade).State = EntityState.Added;
                    db.SaveChanges();
                    return PUnidade;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Unidade AtualizarUnidade(Unidade PUnidade)
        {
            using (Contexto db = new Contexto())
            {
                db.Entry(PUnidade).State = EntityState.Modified;
                db.SaveChanges();
                return PUnidade;
            }
        }

        public void ExcluirUnidade(Unidade PUnidade)
        {
            using (Contexto db = new Contexto())
            {
                db.Entry(PUnidade).State = EntityState.Deleted;
                db.SaveChanges();
            }
        }

    }
}