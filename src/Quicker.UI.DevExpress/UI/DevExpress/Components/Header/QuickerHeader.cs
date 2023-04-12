using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quicker.UI.DevExpress.Components.Header

{
    public partial class QuickerHeader : UserControl
    {
        public QuickerHeader()
        {
            InitializeComponent();
        }

        private void tmrRelogio_Tick(object sender, EventArgs e)
        {
            lblRelogio.Text = DateTime.Now.ToLongTimeString();
        }
        public string Descricao
        {
            get { return lblDescricao.Text; }
            set { lblDescricao.Text = value; }
        }
    }
}
