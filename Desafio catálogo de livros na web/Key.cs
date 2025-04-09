using Microsoft.Extensions.Configuration;

namespace Desafio_catálogo_de_livros_na_web
{
    public static class Key
    {
        public static string Secret { get; private set; }

        public static void Configure(IConfiguration configuration)
        {
            Secret = configuration["KeySettings:Secret"];
        }
    }
}