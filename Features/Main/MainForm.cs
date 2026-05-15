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
        // 1. Declarar o serviço para que a classe toda possa usar
        private readonly IAuthService _authService;

        // 2. Mudar o construtor para receber o IAuthService (O Autofac vai amar isso)
        public MainForm(IAuthService authService)
        {
            InitializeComponent();
            _authService = authService; // Aqui guardamos o serviço injetado
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

            webView.NavigationCompleted += WebView_NavigationCompleted;
            webView.Source = new Uri(url);
        }

        private async void WebView_NavigationCompleted(object sender, CoreWebView2NavigationCompletedEventArgs e)
        {
            if (e.IsSuccess)
            {
                // 3. Pegamos o nome que veio da planilha através do serviço
                string nomeParaOScript = _authService.NomeUsuarioLogado ?? "Usuário";

                // 4. Usamos o "$" antes da string para que o C# troque {nomeParaOScript} pelo valor real
                // E dobramos as chaves {{ }} do JavaScript para o C# não se confundir
                string script = $@"
                (function() {{
                    const prefixo = '*{nomeParaOScript}*:\n\n';
                    let processando = false;

                    window.addEventListener('keydown', function(event) {{
                        const box = document.querySelector('div[data-testid=""conversation-compose-box-input""]');
                        
                        if (box && (event.target === box || box.contains(event.target))) {{
                            if (event.key === 'Enter' && !event.shiftKey) {{
                                
                                if (processando) return;

                                const textoOriginal = box.innerText.trim();

                                // Verificamos se já não começa com o nome para não duplicar
                                if (textoOriginal.length > 0 && !textoOriginal.startsWith('*{nomeParaOScript}*:')) {{
                                    
                                    processando = true;
                                    event.preventDefault();
                                    event.stopImmediatePropagation();

                                    const textoFinal = prefixo + textoOriginal;

                                    box.focus();
                                    document.execCommand('selectAll', false, null);

                                    setTimeout(() => {{
                                        const dataTransfer = new DataTransfer();
                                        dataTransfer.setData('text/plain', textoFinal);
                                        const pasteEvent = new ClipboardEvent('paste', {{
                                            clipboardData: dataTransfer,
                                            bubbles: true,
                                            cancelable: true
                                        }});
                                        box.dispatchEvent(pasteEvent);

                                        setTimeout(() => {{
                                            const enterEvent = new KeyboardEvent('keydown', {{
                                                bubbles: true, 
                                                cancelable: true, 
                                                key: 'Enter', 
                                                code: 'Enter', 
                                                keyCode: 13,
                                                which: 13
                                            }});
                                            box.dispatchEvent(enterEvent);
                                            
                                            setTimeout(() => {{ processando = false; }}, 1000);
                                            
                                        }}, 150); 
                                    }}, 50); 
                                }}
                            }}
                        }}
                    }}, true);
                }})();";

                await webView.CoreWebView2.ExecuteScriptAsync(script);
            }
        }

        public void FecharView() => this.Close();
    }
}