using Desafio_catálogo_de_livros_na_web.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Desafio_catálogo_de_livros_na_web.Controllers
{

    [ApiController]
    [Route("api/v1/auth")]
    public class AuthController : Controller
    {
        [HttpPost]
        public IActionResult Auth(string username, string password)
        {
            if (username == "admin" && password == "admin")
            {
                var token = TokenService.GenerateToken(new Domain.Model.Usuario());
                return Ok(token);
            }
            return BadRequest("usuario ou senha incorretos");
        }
    }
}
