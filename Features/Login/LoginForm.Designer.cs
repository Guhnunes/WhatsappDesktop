using System.Windows.Forms;

namespace WhatsappDesktop
{
    partial class LoginForm
    {
        private System.ComponentModel.IContainer components = null;

        // Controles que usamos no código principal
        private System.Windows.Forms.TextBox txtUsuario;
        private System.Windows.Forms.TextBox txtSenha;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Label lblTitulo;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.txtUsuario = new System.Windows.Forms.TextBox();
            this.txtSenha = new System.Windows.Forms.TextBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.SuspendLayout();

            // Título Centralizado
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.Location = new System.Drawing.Point(0, 30);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(300, 45); // Largura total do form
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter; // Centraliza o texto
            this.lblTitulo.Text = "WhatsApp";

            // TextBox Usuário - Destaque Visual
            this.txtUsuario.BackColor = System.Drawing.Color.FromArgb(50, 52, 54); // Tom cinza levemente mais claro que o fundo
            this.txtUsuario.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUsuario.ForeColor = System.Drawing.Color.White;
            this.txtUsuario.Location = new System.Drawing.Point(50, 110);
            this.txtUsuario.Name = "txtUsuario";
            this.txtUsuario.Size = new System.Drawing.Size(200, 25);
            this.txtUsuario.TabIndex = 1;

            // TextBox Senha - Destaque Visual
            this.txtSenha.BackColor = System.Drawing.Color.FromArgb(50, 52, 54);
            this.txtSenha.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSenha.ForeColor = System.Drawing.Color.White;
            this.txtSenha.Location = new System.Drawing.Point(50, 160);
            this.txtSenha.Name = "txtSenha";
            this.txtSenha.Size = new System.Drawing.Size(200, 25);
            this.txtSenha.TabIndex = 2;
            this.txtSenha.UseSystemPasswordChar = true;

            // Label Usuário
            Label lblUser = new Label();
            lblUser.Text = "USUÁRIO";
            lblUser.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            lblUser.ForeColor = System.Drawing.Color.FromArgb(150, 150, 150);
            lblUser.BackColor = System.Drawing.Color.Transparent; // Garante que não tenha fundo cinza
            lblUser.Location = new System.Drawing.Point(50, 88);
            lblUser.AutoSize = true;
            this.Controls.Add(lblUser);

            // Label Senha
            Label lblPass = new Label();
            lblPass.Text = "SENHA";
            lblPass.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            lblPass.ForeColor = System.Drawing.Color.FromArgb(150, 150, 150);
            lblPass.BackColor = System.Drawing.Color.Transparent; // Garante que não tenha fundo cinza
            lblPass.Location = new System.Drawing.Point(50, 145); // Ajustado para não bater no TextBox
            lblPass.AutoSize = true;
            this.Controls.Add(lblPass);

            // Ajuste rápido nos TextBox para dar espaço
            this.txtUsuario.Location = new System.Drawing.Point(50, 110);
            this.txtSenha.Location = new System.Drawing.Point(50, 168); 

            // Botão Centralizado
            this.btnLogin.BackColor = System.Drawing.Color.FromArgb(37, 211, 102);
            this.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogin.FlatAppearance.BorderSize = 0;
            this.btnLogin.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnLogin.Location = new System.Drawing.Point(50, 220);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(200, 40);
            this.btnLogin.Text = "Entrar";
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            this.btnLogin.TabStop = false; // Remove o foco automático que cria a borda
            this.btnLogin.FlatAppearance.BorderSize = 0;
            this.btnLogin.Cursor = System.Windows.Forms.Cursors.Hand; // Cursor de "mãozinha" ao passar o mouse

            // Configuração do Form
            this.ClientSize = new System.Drawing.Size(300, 350);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.txtUsuario);
            this.Controls.Add(this.txtSenha);
            this.Controls.Add(this.btnLogin);
            this.Name = "LoginForm";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}