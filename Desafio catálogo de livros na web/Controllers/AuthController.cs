using Desafio_catálogo_de_livros_na_web.Application.Services;
using Desafio_catálogo_de_livros_na_web.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using BCrypt.Net;
using Desafio_catálogo_de_livros_na_web.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Desafio_catálogo_de_livros_na_web.Domain.DTO;
using Microsoft.Extensions.Caching.Memory;

namespace Desafio_catálogo_de_livros_na_web.Controllers
{
    [ApiController]
    [Route("api/v1/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMemoryCache _memoryCache;
        private readonly EmailService _emailService;

        public AuthController(IUsuarioRepository usuarioRepository, IMemoryCache memoryCache, EmailService emailService)
        {
            _usuarioRepository = usuarioRepository;
            _memoryCache = memoryCache;
            _emailService = emailService;
        }

        [HttpPost]
        public async Task<IActionResult> Auth([FromBody] LoginRequest loginRequest)
        {
            if (loginRequest == null)
            {
                return BadRequest("Dados inválidos");
            }

            var usuario = await _usuarioRepository.BuscarEmail(loginRequest.Email);
            if (usuario == null)
            {
                return BadRequest("Usuário não encontrado");
            }

            bool senhaValida = BCrypt.Net.BCrypt.Verify(loginRequest.Password, usuario.senha_hash);
            if (!senhaValida)
            {
                return BadRequest("Senha incorreta");
            }

            var token = TokenService.GenerateToken(usuario);
            return Ok(new { token });
        }

        [HttpGet("verificar-email")]
        public async Task<IActionResult> VerificarEmail([FromQuery] string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return BadRequest(new { mensagem = "Email não pode ser vazio" });
            }

            var usuario = await _usuarioRepository.BuscarEmail(email);
            return Ok(new { existe = usuario != null });
        }

        [HttpPost("esqueci-senha")]
        public async Task<IActionResult> EsqueciSenha([FromBody] EsqueciSenhaDTO dto)
        {
            var usuario = await _usuarioRepository.BuscarEmail(dto.Email);
            if (usuario == null)
            {
                return NotFound("Usuário não encontrado.");
            }

            var codigo = new Random().Next(100000, 999999).ToString();
            try
            {
                await _emailService.EnviarCodigoRecuperacaoEmail(dto.Email, codigo);

                usuario.codigorecuperacao = codigo;
                usuario.codigorecuperacao_expira = DateTimeOffset.UtcNow.AddMinutes(15);

                await _usuarioRepository.AtualizarUsuario(usuario);

                _memoryCache.Set("EmailRecuperacao", dto.Email, TimeSpan.FromMinutes(15));

                return Ok(new { mensagem = "Código enviado." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = "Erro ao enviar o e-mail de recuperação.", erro = ex.Message });
            }
        }

        [HttpPost("verificar-codigo")]
        public async Task<IActionResult> VerificarCodigo([FromBody] VerificarCodigoDTO dto)
        {
            if (dto == null || string.IsNullOrEmpty(dto.Codigo))
            {
                return BadRequest("Dados inválidos.");
            }

            if (!_memoryCache.TryGetValue("EmailRecuperacao", out string email))
            {
                return BadRequest("Sessão de recuperação expirou ou não encontrada.");
            }

            var usuario = await _usuarioRepository.BuscarEmail(email);

            if (usuario.codigorecuperacao != dto.Codigo || usuario.codigorecuperacao_expira < DateTimeOffset.UtcNow)
            {
                return BadRequest("Código inválido ou expirado.");
            }

            _memoryCache.Set($"CodigoValidado_{email}", true, TimeSpan.FromMinutes(15));
            return Ok(new { mensagem = "Código válido." });
        }

        [HttpPost("redefinir-senha")]
        public async Task<IActionResult> RedefinirSenha([FromBody] RedefinirSenhaDTO dto)
        {
            if (!_memoryCache.TryGetValue("EmailRecuperacao", out string email) || !_memoryCache.TryGetValue($"CodigoValidado_{email}", out bool codigoValidado))
            {
                return BadRequest("Sessão de recuperação expirou");
            }

            if (string.IsNullOrWhiteSpace(dto.NovaSenha))
                return BadRequest("A nova senha é obrigatória.");

            var usuario = await _usuarioRepository.BuscarEmail(email);

            usuario.codigorecuperacao = null;
            usuario.codigorecuperacao_expira = null;
            await _usuarioRepository.AtualizarUsuario(usuario, dto.NovaSenha);

            _memoryCache.Remove("EmailRecuperacao");
            _memoryCache.Remove($"CodigoValidado_{email}");

            return Ok(new { mensagem = "Senha redefinida." });
        }

    }
}