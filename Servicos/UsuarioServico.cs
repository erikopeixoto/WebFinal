using System;
using System.Collections.Generic;
using Modelos;
using AcessoDados.Repositorio;

namespace Servicos
{
    public class UsuarioServico
    {
        UsuarioRepositorio rep = new UsuarioRepositorio();
        public List<Usuario> ObterUsuarios()
        {
            return rep.ObterUsuarios();
        }

        public Usuario ObterUsuario(int Pid)
        {
            return rep.ObterUsuario(Pid);
        }

        public Usuario ObterUsuario(string Pnome)
        {
            return rep.ObterUsuario(Pnome);
        }

        public Usuario RealizarLogin(string Pemail, string Psenha)
        {
            return rep.RealizarLogin(Pemail, Psenha);
        }

        public Usuario IncluirUsuario(Usuario Pusuario)
        {
            try
            {
                return rep.IncluirUsuario(Pusuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Usuario AtualizarUsuario(Usuario Pusuario)
        {
            return rep.AtualizarUsuario(Pusuario);
        }

        public void ExcluirUsuario(Usuario Pusuario)
        {
            rep.ExcluirUsuario(Pusuario);
        }
    }
}