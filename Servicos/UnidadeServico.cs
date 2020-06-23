using System;
using System.Collections.Generic;
using Modelos;
using AcessoDados.Repositorio;

namespace Servicos
{
    public class UnidadeServico
    {
        UnidadeRepositorio rep = new UnidadeRepositorio();
        public List<Unidade> ObterUnidades(int pagina = 1, int linhas = 5)
        {
            return rep.ObterUnidades(pagina, linhas);
        }

        public List<Unidade> ObterUnidadesT()
        {
            return rep.ObterUnidadesT();
        }

        public int ObterQuantidadeUnidade()
        {
            return rep.ObterQuantidadeUnidade();
        }

        public Unidade ObterUnidade(string Psg)
        {
            return rep.ObterUnidade(Psg);
        }

        public Unidade IncluirUnidade(Unidade PUnidade)
        {
            try
            {
                return rep.IncluirUnidade(PUnidade);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Unidade AtualizarUnidade(Unidade PUnidade)
        {
            return rep.AtualizarUnidade(PUnidade);
        }

        public void ExcluirUnidade(Unidade PUnidade)
        {
            rep.ExcluirUnidade(PUnidade);
        }

    }
}