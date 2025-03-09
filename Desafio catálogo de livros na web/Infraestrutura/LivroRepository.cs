using Desafio_catálogo_de_livros_na_web.Model;

namespace Desafio_catálogo_de_livros_na_web.Infraestrutura
{
    public class LivroRepository : ILivroRepository
    {
        private readonly ConnectionContext _context = new ConnectionContext();

        public void add(Livro livro)
        {
            _context.Livros.Add(livro);
            _context.SaveChanges();
        }

        public List<Livro> Get()
        {
            return _context.Livros.ToList();
        }
    }
}
