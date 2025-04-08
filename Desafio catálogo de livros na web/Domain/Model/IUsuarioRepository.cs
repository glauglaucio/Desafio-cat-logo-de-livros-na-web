using Desafio_catálogo_de_livros_na_web.Domain.Model;
using System.Threading.Tasks;

namespace Desafio_catálogo_de_livros_na_web.Infrastructure.Repositories
{
    public interface IUsuarioRepository
    {
        Task<Usuario> BuscarEmail(string email);
        Task AtualizarUsuario(Usuario usuario, string senhaEmTextoPuro = null);

        Task CriarUsuario(Usuario usuario);
    }
}