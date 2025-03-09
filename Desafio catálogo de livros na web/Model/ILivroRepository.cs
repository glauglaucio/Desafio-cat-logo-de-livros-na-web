namespace Desafio_catálogo_de_livros_na_web.Model
{
    public interface ILivroRepository
    {
        void add(Livro livro);

        List<Livro> Get();

    }
}
