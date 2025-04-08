using System;

namespace Desafio_catálogo_de_livros_na_web.Domain.DTO
{
    public class UsuarioCadastroDTO
    {
        public string Nome { get; set; }
        public string DataNascimento { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
    }
}