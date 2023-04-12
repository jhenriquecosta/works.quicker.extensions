using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quicker.UI.DevExpress.Components.Base.Contracts;

namespace Quicker.UI.DevExpress.Components.Base.DataBindings
{
    public interface IQuickerDataBindingsBase : IQuickerDevExpressComponentBase
    {
        string Entity { get; set; }
        string Property { get; set; }
    }

    public interface IXTBaseDataSource : IQuickerDevExpressComponentBase
    {

        void LoadDataFrom(object data);
    }


}
