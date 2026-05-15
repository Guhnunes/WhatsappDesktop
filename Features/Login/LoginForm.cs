using System;
using System.Drawing;
using System.Windows.Forms;
using WhatsappDesktop.Features.Infraestrutura;
using WhatsappDesktop.Features.Login.View;
using WhatsappDesktop.Features.Main.View;

namespace WhatsappDesktop
{
    public partial class LoginForm : BaseForm, ILoginView
    {
        private readonly IAuthService _authService;
        private Panel painelLoading;

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
            this.DialogResult = DialogResult.OK;
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

        private async void btnLogin_Click(object sender, EventArgs e)
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
                    };

                    txt.Leave += (s, e) => {
                        txt.BackColor = corPadrao;
                    };
                }
            }
        }
        private void CriarPainelLoading()
        {
            painelLoading = new Panel
            {
                Size = this.ClientSize,
                BackColor = Color.FromArgb(150, 32, 33, 36), // Semi-transparente
                Location = new Point(0, 0),
                Visible = false
            };

            Label lblStatus = new Label
            {
                Text = "Autenticando...",
                ForeColor = Color.White,
                AutoSize = false,
                Size = new Size(200, 30),
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 12, FontStyle.Bold)
            };

            lblStatus.Location = new Point((painelLoading.Width - lblStatus.Width) / 2, (painelLoading.Height - lblStatus.Height) / 2);
            painelLoading.Controls.Add(lblStatus);
            this.Controls.Add(painelLoading);
            painelLoading.BringToFront();
        }

        // Implementação da Interface
        public void AlternarCarregamento(bool carregando)
        {
            if (painelLoading == null) CriarPainelLoading();

            painelLoading.Visible = carregando;
            btnLogin.Enabled = !carregando; // Desabilita o botão para evitar cliques duplos
            txtUsuario.Enabled = !carregando;
            txtSenha.Enabled = !carregando;
        }
    }
}