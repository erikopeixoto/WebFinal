using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Modelos;
using System.Data.Entity;

namespace AcessoDados.Repositorio
{
    public class UsuarioRepositorio
    {
        public List<Usuario> ObterUsuarios()
        {
            using (Contexto db = new Contexto())
            {
                return db.Usuarios.ToList();
            }
        }

        public Usuario ObterUsuario(int Pid)
        {
            using (Contexto db = new Contexto())
            {
                return db.Usuarios.SingleOrDefault(p => p.ID == Pid);
            }
        }

        public Usuario ObterUsuario(string Pnome)
        {
            using (Contexto db = new Contexto())
            {
                return db.Usuarios.SingleOrDefault(p => p.nmUsuario == Pnome);
            }
        }

        public Usuario RealizarLogin(string Pemail, string Psenha)
        {
            using (Contexto db = new Contexto())
            {
                return db.Usuarios.SingleOrDefault(u => u.dsEmail == Pemail && u.dsSenha == Psenha);
            }
        }

        public Usuario IncluirUsuario(Usuario Pusuario)
        {
            try
            {
                using (Contexto db = new Contexto())
                {
                    db.Entry(Pusuario).State = EntityState.Added;
                    //var erros = db.GetValidationErrors();

                    db.SaveChanges();

                    return Pusuario;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Usuario AtualizarUsuario(Usuario Pusuario)
        {
            using (Contexto db = new Contexto())
            {
                db.Entry(Pusuario).State = EntityState.Modified;

                db.SaveChanges();

                return Pusuario;
            }
        }

        public void ExcluirUsuario(Usuario Pusuario)
        {
            using (Contexto db = new Contexto())
            {
                db.Entry(Pusuario).State = EntityState.Deleted;

                db.SaveChanges();
            }
        }
    }
}