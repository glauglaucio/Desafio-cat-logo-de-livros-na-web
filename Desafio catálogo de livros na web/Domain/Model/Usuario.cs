﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Desafio_catálogo_de_livros_na_web.Domain.Model
{
    [Table("usuarios")]
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; private set; }

        [Required]
        [StringLength(255)]
        public string nome { get; private set; }

        [Required]
        public DateTime data_nascimento { get; private set; }

        [Required]
        [StringLength(250)]
        [EmailAddress]
        public string email { get; private set; }

        [Required]
        public string senha_hash { get; private set; }

        public string? codigorecuperacao { get; set; }
        public DateTimeOffset? codigorecuperacao_expira { get; set; }

        // Relacionamento com Livros
        public virtual ICollection<Livro> livros { get; set; } = new List<Livro>();

        //Construtor
        public Usuario(string nome, DateTime data_nascimento, string email, string senha_hash)
        {
            this.nome = nome;
            this.data_nascimento = data_nascimento;
            this.email = email;
            this.senha_hash = senha_hash;
        }

        public void AtualizarSenha(string novaSenha)
        {
            senha_hash = BCrypt.Net.BCrypt.HashPassword(novaSenha);
        }

        public Usuario() { }
    }
}
