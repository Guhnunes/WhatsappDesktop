using System;

namespace WhatsappDesktop.Features.Login.View
{
    public interface ILoginView
    {
        string Usuario { get; }
        string Senha { get; }

        void MostrarMensagem(string mensagem, bool isErro = true);
        void FecharView();
        void AlternarCarregamento(bool carregando);

        event EventHandler OnLoginClick;
    }
}