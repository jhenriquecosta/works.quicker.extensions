using System;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.Utils.Drawing;
using Quicker.UI.DevExpress.Components.Inputs.ViewInfo;

namespace Quicker.UI.DevExpress.Components.Base.Contracts
{
    public interface IQuickerEditor
    {
        object EditValue { get; set; }
        bool SupportsDraw { get; }
        bool AllowBorder { get; }
        event EventHandler EditValueChanged;
        Size CalcSize(Graphics g);
        void Draw(GraphicsCache cache, QuickerEditorEditViewInfo viewInfo);
        void SetupAsDrawControl();
        void SetupAsEditControl();
        string GetDisplayText(object editValue);
        bool IsNeededKey(KeyEventArgs e);
        bool AllowClick(Point point);
    }
}