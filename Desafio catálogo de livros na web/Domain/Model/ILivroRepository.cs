using Desafio_catálogo_de_livros_na_web.Domain.DTO;

namespace Desafio_catálogo_de_livros_na_web.Domain.Model
{
    public interface ILivroRepository
    {
        void add(Livro livro);

        List<LivroDTO> Get(int usuarioId);

    }
}
