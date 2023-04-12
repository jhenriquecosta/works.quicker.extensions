using DevExpress.Utils.Drawing;
using DevExpress.XtraEditors;
using Quicker.UI.DevExpress.Components.Base.Contracts;
using Quicker.UI.DevExpress.Components.Inputs.ViewInfo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quicker.UI.DevExpress.Components.Inputs.Controls
{
    [ToolboxItem(false)]
    public class QuickerComboEditImage : ImageComboBoxEdit, IQuickerEditor, IQuickerCombo, ICloneable
    {
        private bool _selectAllOnEnter = false;
        public QuickerComboEditImage()
        {
            Enter += (sender, e) => { _selectAllOnEnter = true; };

            MouseUp += (sender, e) =>
            {
                if (_selectAllOnEnter)
                {
                    var edit = sender as QuickerComboEditImage;
                    if (edit != null)
                    {
                        BeginInvoke(new Action(() => edit.SelectAll()));
                    }
                    _selectAllOnEnter = false;
                }
            };
        }
        public bool SupportsDraw
        {
            get { return false; }
        }

        public bool AllowBorder
        {
            get { return false; }
        }

        public bool AllowClick(Point point)
        {
            return true;
        }

        public Size CalcSize(Graphics g)
        {
            return Size;
        }

        public object Clone()
        {
            throw new NotImplementedException();
        }

        public void Draw(GraphicsCache cache, QuickerEditorEditViewInfo viewInfo)
        {
            throw new NotImplementedException();
        }

        public string GetDisplayText(object editValue)
        {
            return editValue == null ? string.Empty : editValue.ToString();
        }

        public void SetupAsDrawControl()
        {
            throw new NotImplementedException();
        }

        public void SetupAsEditControl()
        {
            throw new NotImplementedException();
        }
    }
}
