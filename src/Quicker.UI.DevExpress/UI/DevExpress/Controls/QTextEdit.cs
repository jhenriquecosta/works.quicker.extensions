using DevExpress.XtraEditors;
using Quicker.UI.DevExpress.Components.Inputs.Settings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quicker.UI.DevExpress.Controls
{

    [ToolboxItem(true)]
    public class QTextEdit : TextEdit
    {
        readonly QuickerDataBindings quickerDataBindings = new QuickerDataBindings();

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Editor("Quicker.Editor.Settings", typeof(System.Drawing.Design.UITypeEditor))]
        [DefaultValue(""), Category("Data"), Localizable(true), RefreshProperties(RefreshProperties.All),]
        public virtual QuickerDataBindings Bindings
        {
            get
            {
                return quickerDataBindings;
            }
        }
    }

}
