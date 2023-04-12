using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quicker.Samples.Controls
{
    public partial class QTaskBarFormCrud : XtraUserControl
    {
        public delegate void QEventHandler(object sender, EventArgs e);
        
         public event QEventHandler SaveEventClick;    
         public event QEventHandler DeleteEventClick;    
         public event QEventHandler ExitEventClick;    
        
            
        public QTaskBarFormCrud()
        {
            InitializeComponent();
            this.btnDelete.Click += BtnDelete_Click;
            this.btnExit.Click += BtnExit_Click;
            this.btnSave.Click += BtnSave_Click;
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            DeleteEventClick?.Invoke(this, e);
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
          ExitEventClick?.Invoke(this, e);
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
          SaveEventClick?.Invoke(this, e);
        }
    }
}
