using System.Drawing;
using System.IO;
using System.Windows.Forms;
using WhatsappDesktop.Properties;

namespace WhatsappDesktop.Features.Infraestrutura
{
    public class BaseForm : Form
    {
        public BaseForm()
        {
            try
            {
                byte[] iconBytes = Resources.whatsapp;

                using (MemoryStream ms = new MemoryStream(iconBytes))
                {
                    this.Icon = new Icon(ms);
                }
            }
            catch
            {
                // Se falhar, o Windows usa o ícone padrão, sem quebrar o app
            }
        }
    }
}