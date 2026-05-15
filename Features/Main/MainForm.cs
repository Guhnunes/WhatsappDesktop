using Microsoft.Web.WebView2.Core;
using System.IO;
using System;

using System.Drawing;
using System.Windows.Forms;
using WhatsappDesktop.Features.Infraestrutura;

namespace WhatsappDesktop.Features.Main.View
{
    public partial class MainForm : BaseForm, IMainView
    {
        public MainForm()
        {
            InitializeComponent();
            ConfigurarEstilo();
        }

        private void ConfigurarEstilo()
        {
            this.Text = "WhatsApp Desktop - Sessão Ativa";
            this.WindowState = FormWindowState.Maximized; // Abre em tela cheia
            this.BackColor = Color.FromArgb(32, 33, 36);
        }

        public async void InicializarNavegador(string url)
        {
            // 1. Define o caminho da pasta de dados (ex: AppData/Local/WhatsappDesktop/UserData)
            string userDataFolder = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "WhatsappDesktop",
                "UserData"
            );

            // 2. Garante que a pasta existe
            if (!Directory.Exists(userDataFolder))
                Directory.CreateDirectory(userDataFolder);

            // 3. Cria o ambiente do WebView2 com a pasta de perfil personalizada
            var environment = await CoreWebView2Environment.CreateAsync(null, userDataFolder);

            // 4. Inicializa o controle com esse ambiente
            await webView.EnsureCoreWebView2Async(environment);

            // 5. Agora sim, navega para o WhatsApp
            webView.Source = new Uri(url);
        }

        public void FecharView() => this.Close();
    }
}