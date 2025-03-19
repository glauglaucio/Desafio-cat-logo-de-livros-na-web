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
        }   

    }
}
