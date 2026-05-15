using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;
using System.Text.Json;

namespace WhatsappDesktop.Features.Infraestrutura
{
    public class Usuario
    {
        public string Nome { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
    }

    public class AuthService : IAuthService
    {
        private readonly string _googleApiUrl = "https://script.google.com/macros/s/AKfycbwUlJGJTQ-chwmQWqo383dhprBPypPXaSvuQ_6C0iTskOU6l2w0O-leJ7vzGREfhPgaCw/exec";

        public bool IsAutenticado { get; private set; } = false;

        // CORREÇÃO: Adicionado 'public' e 'private set' para permitir a gravação interna
        public string NomeUsuarioLogado { get; private set; }

        public async Task<bool> ValidarUsuario(string usuario, string senha)
        {
            var userResult = await ValidarLoginWeb(usuario, senha);

            if (userResult != null)
            {
                IsAutenticado = true;
                NomeUsuarioLogado = userResult.Nome; // Agora o C# permite gravar aqui
                return true;
            }

            IsAutenticado = false;
            NomeUsuarioLogado = null;
            return false;
        }

        private async Task<Usuario> ValidarLoginWeb(string login, string senha)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // Adicionamos um cabeçalho para evitar cache
                    client.DefaultRequestHeaders.CacheControl = new System.Net.Http.Headers.CacheControlHeaderValue { NoCache = true };

                    var response = await client.GetStringAsync(_googleApiUrl);
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                        NumberHandling = System.Text.Json.Serialization.JsonNumberHandling.AllowReadingFromString
                    };

                    var usuarios = JsonSerializer.Deserialize<List<Usuario>>(response, options);

                    if (usuarios == null) return null;

                    // Debug rápido: veja no Console se a lista está vindo com todos os usuários
                    System.Diagnostics.Debug.WriteLine($"Usuários carregados: {usuarios.Count}");

                    return usuarios.FirstOrDefault(u =>
                        u.Login != null && u.Login.Trim().Equals(login.Trim(), StringComparison.OrdinalIgnoreCase) &&
                        u.Senha != null && u.Senha.ToString().Trim() == senha.Trim());
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Erro Login: " + ex.Message);
                    return null;
                }
            }
        }
    }
}