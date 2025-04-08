using AutoMapper;
using Desafio_catálogo_de_livros_na_web.Application.ViewModel;
using Desafio_catálogo_de_livros_na_web.Domain.DTO;
using Desafio_catálogo_de_livros_na_web.Domain.Model;

namespace Desafio_catálogo_de_livros_na_web.Application.Mapping
{
    public class DomainToDTOMapping : Profile
    {
        public DomainToDTOMapping()
        {
            CreateMap<Livro, LivroDTO>();

            CreateMap<UsuarioCadastroDTO, Usuario>()
                .ConstructUsing(dto => new Usuario(
                    dto.Nome,
                    DateTime.SpecifyKind(DateTime.Parse(dto.DataNascimento), DateTimeKind.Utc),
                    dto.Email,
                    BCrypt.Net.BCrypt.HashPassword(dto.Senha)
                ));
        }
    }
}