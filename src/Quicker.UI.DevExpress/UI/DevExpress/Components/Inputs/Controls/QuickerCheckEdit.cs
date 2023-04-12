using DevExpress.Utils.Drawing;
using DevExpress.XtraEditors;
using Quicker.UI.DevExpress.Components.Base.Contracts;
using Quicker.UI.DevExpress.Components.Inputs.ViewInfo;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Quicker.UI.DevExpress.Components.Inputs.Controls
{

    [ToolboxItem(false)]
    public class QuickerCheckEdit : CheckEdit , IQuickerEditor, ICloneable
    {
        private bool _selectAllOnEnter = false;
        public QuickerCheckEdit()
        {
            

     
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
