using System.Data;

namespace WhatsappDesktop.Features.Infraestrutura
{
    public interface IFabricaDeConexao
    {
        IDbConnection RetornarNovaConexao();
    }
}
