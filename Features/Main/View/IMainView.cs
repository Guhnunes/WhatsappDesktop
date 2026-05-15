using System;

namespace WhatsappDesktop.Features.Main.View
{
    public interface IMainView
    {
        // Método para iniciar o navegador com uma URL
        void InicializarNavegador(string url);
        void FecharView();
    }
}