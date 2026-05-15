using System.Windows.Forms;

namespace WhatsappDesktop.Features.Infraestrutura
{
    public static class ControleDeMensagens
    {
        public static void Informar(string mensagem) =>
            MessageBox.Show(mensagem, "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

        public static void Avisar(string mensagem) =>
            MessageBox.Show(mensagem, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        public static void Avisar(Form formularioPai, string mensagem) =>
            MessageBox.Show(formularioPai, mensagem, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        public static bool Perguntar(string mensagem) =>
            MessageBox.Show(mensagem, "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
        public static void Erro(string mensagem, string titulo)
        {
            MessageBox.Show(mensagem, titulo, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}