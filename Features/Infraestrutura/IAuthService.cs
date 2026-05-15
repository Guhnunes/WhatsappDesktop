namespace WhatsappDesktop.Features.Infraestrutura
{
    public interface IAuthService
    {
        bool ValidarUsuario(string usuario, string senha);
        bool IsAutenticado { get; }
    }
}
