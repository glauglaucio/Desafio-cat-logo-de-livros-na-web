using AutoMapper;
using Desafio_catálogo_de_livros_na_web.Application.ViewModel;
using Desafio_catálogo_de_livros_na_web.Domain.DTO;
using Desafio_catálogo_de_livros_na_web.Domain.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Desafio_catálogo_de_livros_na_web.Controllers
{

    [ApiController]
    [Route("api/v1/livro")]

    public class LivroController : ControllerBase
    {
        private readonly ILivroRepository _livroRepository;
        private readonly IMapper _mapper;

        public LivroController(ILivroRepository livroRepository, IMapper mapper)
        {
            _livroRepository = livroRepository ?? throw new ArgumentNullException(nameof(livroRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }


        [Authorize]
        [HttpPost]
        public IActionResult Add([FromForm] LivroViewModel livroView)
        {

            var filePath = Path.Combine("Storage", livroView.imagem.FileName);

            using Stream fileStream = new FileStream(filePath, FileMode.Create);
            livroView.imagem.CopyTo(fileStream);


            var livro = new Livro(livroView.id, livroView.usuario_id, livroView.titulo, livroView.isbn, livroView.genero_id, livroView.autor, livroView.editora_id, livroView.sinopse, filePath);

            _livroRepository.add(livro);
            return Ok();
        }

        [Authorize]
        [HttpGet]
        public IActionResult Get(int id)
        {
            var livros = _livroRepository.Get(id);

            return Ok(livros);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult SearchByUserId(int id)
        {
            var livros = _livroRepository.Get(id);

            if (livros == null || livros.Count == 0)
            {
                return NotFound("Nenhum livro encontrado para este usuário.");
            }

            var livrosDTO = _mapper.Map<List<LivroDTO>>(livros);

            return Ok(livrosDTO);
        }
    }
}
