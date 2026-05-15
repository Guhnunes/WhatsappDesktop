using System.Threading.Tasks;

namespace WhatsappDesktop.Features.Infraestrutura
{
    public interface IAuthService
    {
        Task<bool> ValidarUsuario(string usuario, string senha);

        bool IsAutenticado { get; }
        string NomeUsuarioLogado { get; }
    }
}