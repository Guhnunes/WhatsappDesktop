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
            //System.Diagnostics.Debug.WriteLine("2. Evento chegou no Presenter");
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
                _view.FecharView();
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