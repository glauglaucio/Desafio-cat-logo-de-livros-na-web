using Desafio_catálogo_de_livros_na_web.Domain.DTO;
using Desafio_catálogo_de_livros_na_web.Domain.Model;

namespace Desafio_catálogo_de_livros_na_web.Infraestrutura.Repositories
{
    public class LivroRepository : ILivroRepository
    {
        private readonly ConnectionContext _context;
        public LivroRepository(ConnectionContext context)
        {
            _context = context;
        }

        public void add(Livro livro)
        {
            _context.Livros.Add(livro);
            _context.SaveChanges();
        }

        //public List<LivroDTO> Get()
        //{
        //    return _context.Livros
        //        .Select(b => new LivroDTO()
        //        {
        //            id = b.id,
        //            titulo = b.titulo,
        //            isbn = b.isbn,
        //            autor = b.autor,
        //            sinopse = b.sinopse,
        //            imagem = b.imagem

        //        })
        //        .ToList();
        //}


        public List<LivroDTO> Get(int usuarioId)
        {
            return _context.Livros
                .Where(l => l.usuario_id == usuarioId) 
                .Select(b => new LivroDTO()
                {
                    id = b.id,
                    titulo = b.titulo,
                    isbn = b.isbn,
                    autor = b.autor,
                    sinopse = b.sinopse,
                    imagem = b.imagem
                })
                .ToList();
        }


    }
}
