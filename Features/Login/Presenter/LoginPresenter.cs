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

        private async void ProcessarLogin(object sender, EventArgs e)
        {
            string usuario = _view.Usuario;
            string senha = _view.Senha;

            if (string.IsNullOrWhiteSpace(usuario) || string.IsNullOrWhiteSpace(senha))
            {
                _view.MostrarMensagem("Preencha todos os campos!");
                return;
            }

            // A lógica fica aqui no Presenter
            try
            {
                // LIGA O LOADING
                _view.AlternarCarregamento(true);

                bool logado = await _authService.ValidarUsuario(_view.Usuario, _view.Senha);

                if (logado)
                {
                    _view.FecharView();
                }
                else
                {
                    // DESLIGA SE DER ERRO PARA O USUÁRIO TENTAR DE NOVO
                    _view.AlternarCarregamento(false);
                    _view.MostrarMensagem("Usuário ou senha inválidos.");
                }
            }
            catch (Exception)
            {
                _view.AlternarCarregamento(false);
                _view.MostrarMensagem("Erro de conexão com o servidor.");
            }
        }
        public void Inicializar()
        {
            // Qualquer lógica de inicialização que o Presenter precise no futuro
        }
    }
}