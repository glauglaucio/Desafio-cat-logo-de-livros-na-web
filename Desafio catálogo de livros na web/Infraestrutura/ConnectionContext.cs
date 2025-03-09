using Microsoft.EntityFrameworkCore;

namespace Desafio_catálogo_de_livros_na_web.Infraestrutura
{
    public class ConnectionContext : DbContext
    {

        public DbSet<Model.Livro> Livros { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=catalogo_livros;Uid=postgres;Pwd=1423");
        }
    }
}
