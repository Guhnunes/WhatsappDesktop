namespace WhatsappDesktop.Features.Infraestrutura
{
    public interface IAuthService
    {
        bool ValidarUsuario(string usuario, string senha);
    }

    public class AuthService : IAuthService
    {
        public bool ValidarUsuario(string usuario, string senha)
        {
            // MVP: Login fixo para teste
            return usuario == "admin" && senha == "123";
        }
    }
}
