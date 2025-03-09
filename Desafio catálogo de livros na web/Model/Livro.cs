using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Desafio_catálogo_de_livros_na_web.Model
{


    [Table("livros")]
    public class Livro
    {
        [Key]
        public int id { get; private set; }

        [Required]
        public int usuario_id { get; private set; }

        [Required]
        [StringLength(255)]
        public string titulo { get; private set; }

        [Required]
        [StringLength(13)]
        public string isbn { get; private set; }

        [Required]
        public int genero_id { get; private set; }

        [Required]
        [StringLength(255)]
        public string autor { get; private set; }

        [Required]
        public int editora_id { get; private set; }

        [StringLength(5000)]
        public string sinopse { get; private set; }

        public byte[] imagem { get; private set; }

        // Relações
        [ForeignKey("usuario_id")]
        public virtual Usuario usuario { get; private set; }

        [ForeignKey("genero_id")]
        public virtual Genero genero { get; private set; }

        [ForeignKey("editora_id")]
        public virtual Editora editora { get; private set; }

        //Construtor

        public Livro(int id, int usuario_id, string titulo, string isbn, int genero_id, string autor, int editora_id, string sinopse, byte[] imagem)
        {
            this.id = id;
            this.usuario_id = usuario_id;
            this.titulo = titulo;
            this.isbn = isbn;
            this.genero_id = genero_id;
            this.autor = autor;
            this.editora_id = editora_id;
            this.sinopse = sinopse;
            this.imagem = imagem;
        }

        public Livro()
        {
        }
    }
    }
