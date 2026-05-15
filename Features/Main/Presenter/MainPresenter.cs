using WhatsappDesktop.Features.Main.View;

namespace WhatsappDesktop.Features.Main.Presenter
{
    public class MainPresenter : IMainPresenter
    {
        private readonly IMainView _view;
        private const string WhatsAppUrl = "https://web.whatsapp.com/";

        public MainPresenter(IMainView view)
        {
            _view = view;
        }

        public void Inicializar()
        {
            _view.InicializarNavegador(WhatsAppUrl);
        }
    }
}