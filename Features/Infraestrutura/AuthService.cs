namespace WhatsappDesktop.Features.Infraestrutura
{
    public class AuthService : IAuthService
    {
        public bool IsAutenticado { get; private set; } = false;
        public bool ValidarUsuario(string usuario, string senha)
        {
            // MVP: Login fixo para teste
            if (usuario == "admin" && senha == "123")
            {
                IsAutenticado = true;
                return true;
            }
            IsAutenticado = false;
            return false;
        }
    }
}
