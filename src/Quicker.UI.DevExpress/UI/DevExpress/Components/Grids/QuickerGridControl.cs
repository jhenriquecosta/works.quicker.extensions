using DevExpress.XtraGrid;
using Quicker.UI.DevExpress.Components.Base.Contracts;
using System;
using System.ComponentModel;

namespace Quicker.UI.DevExpress.Components.Grids
{
    public abstract class QuickerGridControl : GridControl, IQuickerDevExpressComponentBase
    {
        public event QuickerEventHandler QuickerGridEvent_DoubleClick;
        public delegate void QuickerEventHandler(object sender, EventArgs e);
        public QuickerGridControl()
        {
            DoubleClick += QuickerGrid_DoubleClick;
        }
        protected void QuickerGrid_DoubleClick(object sender, EventArgs e)
        {
            QuickerGridEvent_DoubleClick?.Invoke(this, e);
        }

        public void LoadDataFrom(object data)
        {
            DataSource = data;
            RefreshDataSource();
            Refresh();
        }

        [Category("Quicker Components"), DefaultValue(typeof(string), "")]
        public string DataSourceEntity { get; set; }

        [Category("Quicker Components"), DefaultValue(typeof(string), "")]
        public string DataSourceProperty { get; set; }
    }
}