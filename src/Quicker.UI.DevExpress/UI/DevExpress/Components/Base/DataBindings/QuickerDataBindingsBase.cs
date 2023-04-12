using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;

namespace Quicker.UI.DevExpress.Components.Base.DataBindings
{

    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class QuickerDataBindingsBase : IQuickerDataBindingsBase
    {
        [DefaultValue(""), Category("Data"), Localizable(true), RefreshProperties(RefreshProperties.All),]
        public string Entity { get; set; }

        [DefaultValue(""), Category("Data"), Localizable(true), RefreshProperties(RefreshProperties.All),]
        public string Property { get; set; }
    }
}
