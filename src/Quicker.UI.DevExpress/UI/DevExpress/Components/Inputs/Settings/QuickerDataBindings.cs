using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quicker.UI.DevExpress.Components.Inputs.Settings
{

    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class QuickerDataBindings
    {
        [DefaultValue(""), Category("Data"), Localizable(true), RefreshProperties(RefreshProperties.All),]
        public string Entity { get; set; }

        [DefaultValue(""), Category("Data"), Localizable(true), RefreshProperties(RefreshProperties.All),]
        public string Property { get; set; }
    }
}
