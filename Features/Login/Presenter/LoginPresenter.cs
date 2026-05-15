using System;
using WhatsappDesktop.Features.Infraestrutura;
using WhatsappDesktop.Features.Login.View;

namespace WhatsappDesktop.Features.Login.Presenter
{
    public class LoginPresenter : ILoginPresenter
    {
        private readonly ILoginView _view;
        private readonly IAuthService _authService;

        public LoginPresenter(ILoginView view, IAuthService authService)
        {
            _view = view;
            _authService = authService;

            // O Presenter "ouve" o evento de clique da View
            _view.OnLoginClick += ProcessarLogin;
        }

        private void ProcessarLogin(object sender, EventArgs e)
        {
            // Pegamos os dados através da Interface
            string usuario = _view.Usuario;
            string senha = _view.Senha;

            if (string.IsNullOrWhiteSpace(usuario) || string.IsNullOrWhiteSpace(senha))
            {
                ControleDeMensagens.Erro("Preencha todos os campos!", "Erro");
                return;
            }

            if (_authService.ValidarUsuario(usuario, senha))
            {
                ControleDeMensagens.Informar("Bem-vindo!");
                // Aqui você chamaria a abertura do próximo Form
                // _view.FecharView();
            }
            else
            {
                ControleDeMensagens.Erro("Usuário ou senha inválidos.", "Erro");
            }
        }
        public void Inicializar()
        {
            // Qualquer lógica de inicialização que o Presenter precise no futuro
        }
    }
}