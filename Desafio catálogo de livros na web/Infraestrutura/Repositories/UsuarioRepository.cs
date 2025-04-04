using Desafio_catálogo_de_livros_na_web.Domain.Model;
using Desafio_catálogo_de_livros_na_web.Infraestrutura;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Desafio_catálogo_de_livros_na_web.Infrastructure.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ConnectionContext _context;

        public UsuarioRepository(ConnectionContext context)
        {
            _context = context;
        }

        public async Task<Usuario> BuscarEmail(string email)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.email == email);
        }

        public async Task AtualizarUsuario(Usuario usuario, string NovaSenha = null)
        {
            var usuarioExistente = await _context.Usuarios.FindAsync(usuario.id);
            if (usuarioExistente != null)
            {
                if (!string.IsNullOrEmpty(NovaSenha))
                {
                    usuarioExistente.AtualizarSenha(NovaSenha);
                }

                usuarioExistente.codigorecuperacao = usuario.codigorecuperacao;
                usuarioExistente.codigorecuperacao_expira = usuario.codigorecuperacao_expira;

                await _context.SaveChangesAsync();
            }
        }
    }
}