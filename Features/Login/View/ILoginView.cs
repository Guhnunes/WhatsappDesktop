using System;

namespace WhatsappDesktop.Features.Login.View
{
    public interface ILoginView
    {
        // Propriedades para ler o que o usuário digitou
        string Usuario { get; }
        string Senha { get; }

        // Métodos para a View reagir a comandos do Presenter (lógica)
        void MostrarMensagem(string mensagem, bool isErro = true);
        void FecharView();

        // Evento que a View dispara quando o botão entrar é clicado
        event EventHandler OnLoginClick;
    }
}