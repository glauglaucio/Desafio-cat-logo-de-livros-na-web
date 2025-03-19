using System.ComponentModel.DataAnnotations;

namespace Desafio_catálogo_de_livros_na_web.Domain.DTO
{
    public class LivroDTO
    {   
        public int id { get; set; }

        public string titulo { get; set; }

        public string isbn { get; set; }

        public string autor { get; set; }

        public string sinopse { get; set; }

        public string imagem { get; set; }

    }

}
