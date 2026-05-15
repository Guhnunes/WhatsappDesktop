using Microsoft.Web.WebView2.WinForms;
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
            // Aguarda a inicialização do CoreWebView2
            await webView.EnsureCoreWebView2Async(null);
            webView.Source = new Uri(url);
        }

        public void FecharView() => this.Close();
    }
}