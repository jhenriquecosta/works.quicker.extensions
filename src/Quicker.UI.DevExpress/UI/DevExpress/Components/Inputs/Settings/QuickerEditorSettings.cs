using System.ComponentModel;
using System.Drawing.Design;

namespace Quicker.UI.DevExpress.Components.Inputs.Settings
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class QuickerEditorSettings
    {
        protected QuickerDataBindings xtDataBidings = new QuickerDataBindings();
        public QuickerEditorSettings()
        {

        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Editor("Quicker.Editor.Settings", typeof(UITypeEditor))]
        public virtual QuickerDataBindings DataBindings
        {
            get
            {
                return xtDataBidings;
            }
        }
    }
}
