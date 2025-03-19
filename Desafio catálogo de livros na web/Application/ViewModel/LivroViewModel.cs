namespace Desafio_catálogo_de_livros_na_web.Application.ViewModel
{
    public class LivroViewModel
    {
        public required int id { get; set; }
        public required int usuario_id { get; set; }
        public required string titulo { get; set; }
        public required string isbn { get; set; }
        public required int genero_id { get; set; }
        public required string autor { get; set; }
        public required int editora_id { get; set; }
        public required string sinopse { get; set; }
        public required IFormFile imagem { get; set; }
    }
}
