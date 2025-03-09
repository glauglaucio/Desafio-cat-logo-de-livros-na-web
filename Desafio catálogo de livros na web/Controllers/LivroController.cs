using Desafio_catálogo_de_livros_na_web.Model;
using Desafio_catálogo_de_livros_na_web.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Desafio_catálogo_de_livros_na_web.Controllers
{

    [ApiController]
    [Route("api/v1/livro")]

    public class LivroController : ControllerBase
    {
        private readonly ILivroRepository _livroRepository;

        public LivroController(ILivroRepository livroRepository)
        {
            _livroRepository = livroRepository;
        }

        [HttpPost]
        public IActionResult Add(LivroViewModel livroView)
        {
            var livro = new Livro(livroView.id, livroView.usuario_id, livroView.titulo, livroView.isbn, livroView.genero_id, livroView.autor, livroView.editora_id, livroView.sinopse, livroView.imagem);

            _livroRepository.add(livro);
            return Ok();
        }

        [HttpGet]
        public IActionResult Get()
        {
            var livros = _livroRepository.Get();

            return Ok(livros);
        }


    }
}
