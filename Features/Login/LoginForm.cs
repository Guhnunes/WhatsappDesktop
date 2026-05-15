using System;
using System.Drawing;
using System.Windows.Forms;
using WhatsappDesktop.Features.Infraestrutura;
using WhatsappDesktop.Features.Login.View;

namespace WhatsappDesktop
{
    public partial class LoginForm : BaseForm, ILoginView
    {
        private readonly IAuthService _authService;

        public string Usuario => txtUsuario.Text;
        public string Senha => txtSenha.Text;
        public event EventHandler OnLoginClick;
        public LoginForm()
        {
            InitializeComponent();
        }

        public LoginForm(IAuthService authService) : this()
        {
            _authService = authService;

            this.StartPosition = FormStartPosition.CenterScreen;
        }
        public void MostrarMensagem(string mensagem, bool isErro = true)
        {
            if (isErro)
            {
                ControleDeMensagens.Erro(mensagem, "Erro");
            }
        }
        public void FecharView()
        {
            this.Close();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            ConfigurarEstilo();
            this.AcceptButton = btnLogin;
        }

        private void ConfigurarEstilo()
        {
            this.BackColor = Color.FromArgb(32, 33, 36);
            this.lblTitulo.ForeColor = Color.FromArgb(37, 211, 102);
            this.lblTitulo.TextAlign = ContentAlignment.MiddleCenter;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            // Chama o destaque dos campos
            AdicionarEventosDestaque();

            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.Text = "Login - WhatsApp Desktop";
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            OnLoginClick?.Invoke(this, EventArgs.Empty);
        }
        private void AdicionarEventosDestaque()
        {
            // Cor quando ganha o foco (Verde WhatsApp suave)
            Color corFoco = Color.FromArgb(37, 211, 102);
            // Cor padrão (Cinza escuro dos campos)
            Color corPadrao = Color.FromArgb(50, 52, 54);

            foreach (Control c in this.Controls)
            {
                if (c is TextBox txt)
                {
                    txt.BackColor = corPadrao;
                    txt.BorderStyle = BorderStyle.FixedSingle;

                    txt.Enter += (s, e) => {
                        txt.BackColor = Color.FromArgb(60, 63, 65); // Fundo levemente mais claro
                                                                    // Opcional: Mudar a cor da borda exigiria desenhar manualmente, 
                                                                    // então vamos focar na cor de fundo para o MVP.
                    };

                    txt.Leave += (s, e) => {
                        txt.BackColor = corPadrao;
                    };
                }
            }
        }
    }
}