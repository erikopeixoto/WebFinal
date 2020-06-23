using System.Data.Entity;
using Modelos;


namespace AcessoDados
{
    public class Contexto : DbContext
    {
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Unidade> Unidades { get; set; }
        public DbSet<NotaFiscal> NotasFiscais { get; set; }
        public DbSet<ItensNotaFiscal> ItensNotasFiscais { get; set; }

        /*
        public Contexto()
            : base(ConfigurationManager.ConnectionStrings["Contexto"].ConnectionString)
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<Contexto>());
        }
         */
      
    }
}