using Dapper.FluentMap;

namespace WhatsappDesktop.Features.Infraestrutura
{
    public static class ConfiguraMapeamento
    {
        public static void Registrar()
        {
            FluentMapper.Initialize(config =>
            {
                //config.AddMap(new ClienteModelMap());
                // Adicionar novos maps aqui conforme criar (ProdutoModelMap, etc.)
            });
        }
    }
}
