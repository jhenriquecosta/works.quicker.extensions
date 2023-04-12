using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Controls;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quicker.UI.DevExpress.Components.Grids.GridView
{
    public class QuickerGridView : GridView
    {
        public QuickerGridView()
        { }

        public QuickerGridView(GridControl grid) : base(grid)
        { }

        protected override string ViewName => "QuickerGridView";

        protected override FindControl CreateFindPanel(object findPanelProperties)
        {
            return new QuickerGridFindControl(this, findPanelProperties);
        }
    }
}
