using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Desafio_catálogo_de_livros_na_web.Model
{
    [Table("editoras")]
    public class Editora
    {
        [Key]
        public int id { get; private set; }

        [Required]
        [StringLength(255)]
        public string nome { get; private set; }

        // Relacionamento com Livros
        public virtual ICollection<Livro> livros { get; set; } = new List<Livro>();
    }
}
