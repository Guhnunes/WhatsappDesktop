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
        // 1. Script Global usando EXATAMENTE a sua lógica que funcionou
        private const string JS_WA_HELPER = @"
        window.waHelper = {
            escreverTexto: function(texto) {
                const caixa = document.querySelector('div[contenteditable=""true""][role=""textbox""]');
                if (caixa) {
                    caixa.focus();
                    document.execCommand('insertText', false, texto);
                    caixa.dispatchEvent(new Event('input', { bubbles: true }));
                }
            },
            
            ativarAutoAssinatura: function(nomeAtendente) {
                // Remove o evento antigo se a página recarregar para não duplicar
                if (window._waHandler) {
                    window.removeEventListener('keydown', window._waHandler, true);
                }

                const prefixo = '*' + nomeAtendente + '*:\n\n';
                window._processandoWA = false;

                window._waHandler = function(event) {
                    const box = document.querySelector('div[data-testid=""conversation-compose-box-input""]');
                    
                    if (box && (event.target === box || box.contains(event.target))) {
                        if (event.key === 'Enter' && !event.shiftKey) {
                            
                            if (window._processandoWA) return;

                            const textoOriginal = box.innerText.trim();

                            if (textoOriginal.length > 0 && !textoOriginal.startsWith('*' + nomeAtendente + '*:')) {
                                
                                window._processandoWA = true;
                                event.preventDefault();
                                event.stopImmediatePropagation();

                                const textoFinal = prefixo + textoOriginal;

                                box.focus();
                                document.execCommand('selectAll', false, null);

                                setTimeout(() => {
                                    const dataTransfer = new DataTransfer();
                                    dataTransfer.setData('text/plain', textoFinal);
                                    const pasteEvent = new ClipboardEvent('paste', {
                                        clipboardData: dataTransfer,
                                        bubbles: true,
                                        cancelable: true
                                    });
                                    box.dispatchEvent(pasteEvent);

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
                                        
                                        setTimeout(() => { window._processandoWA = false; }, 300);
                                        
                                    }, 150); 
                                }, 50); 
                            }
                        }
                    }
                };

                window.addEventListener('keydown', window._waHandler, true);
            }
        };
        ";

        private readonly IAuthService _authService;

        public MainForm(IAuthService authService)
        {
            InitializeComponent();
            _authService = authService;
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
            await webView.EnsureCoreWebView2Async(environment);

            // Injeta a inteligência uma única vez
            await webView.CoreWebView2.AddScriptToExecuteOnDocumentCreatedAsync(JS_WA_HELPER);

            webView.NavigationCompleted += WebView_NavigationCompleted;
            webView.Source = new Uri(url);
        }

        private async void WebView_NavigationCompleted(object sender, CoreWebView2NavigationCompletedEventArgs e)
        {
            if (e.IsSuccess && webView.Source.ToString().Contains("web.whatsapp.com"))
            {
                // Limpa possíveis aspas do nome e chama a ativação
                string nomeParaOScript = (_authService.NomeUsuarioLogado ?? "Usuário").Replace("'", "");

                // Chamada limpa
                await webView.ExecuteScriptAsync($"window.waHelper.ativarAutoAssinatura('{nomeParaOScript}');");
            }
        }

        private async void btnEnviarAssinatura_Click(object sender, EventArgs e)
        {
            string atendente = _authService.NomeUsuarioLogado ?? "Usuário";
            string msg = $"\n\n*Atendimento por*: _{atendente}_";
            await webView.ExecuteScriptAsync($"window.waHelper.escreverTexto('{msg}');");
        }

        public void FecharView() => this.Close();
    }
}