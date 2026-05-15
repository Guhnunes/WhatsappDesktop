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
            this.WindowState = FormWindowState.Maximized;
            this.BackColor = Color.FromArgb(32, 33, 36);
        }

        public async void InicializarNavegador(string url)
        {
            string userDataFolder = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "WhatsappDesktop",
                "UserData"
            );

            if (!Directory.Exists(userDataFolder))
                Directory.CreateDirectory(userDataFolder);

            var environment = await CoreWebView2Environment.CreateAsync(null, userDataFolder);

            // IMPORTANTE: Assinar o evento ANTES de chamar o EnsureCoreWebView2Async
            // ou logo após, mas antes de definir o Source.
            await webView.EnsureCoreWebView2Async(environment);

            // ESTA LINHA ESTAVA FALTANDO: Liga o evento ao método
            webView.NavigationCompleted += WebView_NavigationCompleted;

            webView.Source = new Uri(url);
        }

        private async void WebView_NavigationCompleted(object sender, CoreWebView2NavigationCompletedEventArgs e)
        {
            if (e.IsSuccess)
            {
                string script = @"
        (function() {
            const prefixo = '*Gustavo*:\n\n';
            let processando = false;

            // Usamos 'capture: true' para chegar antes de qualquer script do WhatsApp
            window.addEventListener('keydown', function(event) {
                const box = document.querySelector('div[data-testid=""conversation-compose-box-input""]');
                
                if (box && (event.target === box || box.contains(event.target))) {
                    if (event.key === 'Enter' && !event.shiftKey) {
                        
                        // Se já estamos no meio da nossa automação, não faz nada
                        if (processando) return;

                        const textoOriginal = box.innerText.trim();

                        // Só age se houver texto e o nome não estiver lá
                        if (textoOriginal.length > 0 && !textoOriginal.startsWith('*Gustavo*:')) {
                            
                            processando = true;

                            // 1. CANCELA TUDO: O WhatsApp não vai nem saber que você apertou Enter
                            event.preventDefault();
                            event.stopImmediatePropagation();

                            // 2. Monta o texto final
                            const textoFinal = prefixo + textoOriginal;

                            // 3. Substitui o conteúdo via Inserção Atômica
                            document.execCommand('selectAll', false, null);
                            document.execCommand('insertText', false, textoFinal);

                            // 4. DISPARA O ENVIO (O Enter 'de verdade' para o WhatsApp)
                            // Damos um tempo um pouco maior (250ms) para o React atualizar o DOM
                            setTimeout(() => {
                                const enterEvent = new KeyboardEvent('keydown', {
                                    bubbles: true, 
                                    cancelable: true, 
                                    key: 'Enter', 
                                    code: 'Enter', 
                                    keyCode: 13,
                                    which: 13
                                });
                                box.dispatchEvent(enterEvent);
                                
                                // Libera para a próxima mensagem após o envio ser concluído
                                setTimeout(() => { processando = false; }, 1000);
                            }, 250);
                        }
                    }
                }
            }, true); // O segredo está neste 'true' (Fase de Captura)
        })();";

                await webView.CoreWebView2.ExecuteScriptAsync(script);
            }
        }

        public void FecharView() => this.Close();
    }
}