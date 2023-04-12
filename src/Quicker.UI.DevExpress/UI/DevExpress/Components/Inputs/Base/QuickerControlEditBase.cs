using DevExpress.Utils.Drawing;
using Quicker.UI.DevExpress.Components.Base.Contracts;
using Quicker.UI.DevExpress.Components.Inputs.ViewInfo;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quicker.UI.DevExpress.Components.Inputs.Base
{
    public abstract class QuickerControlEditBase : IQuickerEditor
    {
        #region IAnyControlEdit Members

        public object EditValue { get; set; }

        public event EventHandler EditValueChanged
        {
            add { editValueChanged += value; }
            remove { editValueChanged -= value; }
        }

        public bool IsNeededKey(KeyEventArgs e)
        {
            return false;
        }

        public void SetupAsDrawControl()
        {
        }

        public void SetupAsEditControl()
        {
        }

        public Size CalcSize(Graphics g)
        {
            return Size.Empty;
        }

        public bool AllowClick(Point p)
        {
            return false;
        }

        public bool SupportsDraw
        {
            get { return true; }
        }

        public bool AllowBorder
        {
            get { return true; }
        }

        public string GetDisplayText(object editValue)
        {
            if (editValue == null || ReferenceEquals(editValue, DBNull.Value)) return "";
            return editValue.ToString();
        }

        public void Draw(GraphicsCache cache, QuickerEditorEditViewInfo viewInfo)
        {
            viewInfo.PaintAppearance.FillRectangle(cache, viewInfo.ContentRect);
            viewInfo.PaintAppearance.DrawString(cache, viewInfo.DisplayText, viewInfo.ContentRect);
        }

        private event EventHandler editValueChanged;

        private void OnEditValueChanged()
        {
            if (editValueChanged != null) editValueChanged(this, EventArgs.Empty);
        }

        #endregion
    }


}
