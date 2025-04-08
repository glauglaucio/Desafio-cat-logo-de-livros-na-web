using AutoMapper;
using Desafio_catálogo_de_livros_na_web.Domain.DTO;
using Desafio_catálogo_de_livros_na_web.Domain.Model;
using Desafio_catálogo_de_livros_na_web.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Desafio_catálogo_de_livros_na_web.Controllers
{
    [ApiController]
    [Route("api/v1/cadastro")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;

        public UsuarioController(IUsuarioRepository usuarioRepository, IMapper mapper)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CadastrarUsuario([FromBody] UsuarioCadastroDTO dto)
        {
            if (dto == null || string.IsNullOrWhiteSpace(dto.Nome) || string.IsNullOrWhiteSpace(dto.Email) ||
                string.IsNullOrWhiteSpace(dto.Senha) || string.IsNullOrWhiteSpace(dto.DataNascimento))
            {
                return BadRequest("Todos os campos são obrigatórios.");
            }

            if (!DateTime.TryParse(dto.DataNascimento, out var dataNascimento))
            {
                return BadRequest("Data de nascimento inválida.");
            }

            var emailValido = new EmailAddressAttribute().IsValid(dto.Email);
            if (!emailValido)
            {
                return BadRequest("O e-mail informado não é válido.");
            }

            var usuarioExistente = await _usuarioRepository.BuscarEmail(dto.Email);
            if (usuarioExistente != null)
            {
                return BadRequest("Já existe um usuário com esse e-mail.");
            }

            var novoUsuario = _mapper.Map<Usuario>(dto);
            await _usuarioRepository.CriarUsuario(novoUsuario);

            return Ok(new { mensagem = "Usuário cadastrado com sucesso!" });
        }
    }
}