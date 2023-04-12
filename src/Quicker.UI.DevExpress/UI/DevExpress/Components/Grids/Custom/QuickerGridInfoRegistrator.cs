using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Registrator;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Base.Handler;
using DevExpress.XtraGrid.Views.Base.ViewInfo;
using Quicker.UI.DevExpress.Components.Grids.GridView;
using Quicker.UI.DevExpress.UI.DevExpress.Components.Grids.Custom;

namespace Quicker.UI.DevExpress.Components.Grids.Custom
{
    public class QuickerGridInfoRegistrator : GridInfoRegistrator
    {
        public override string ViewName => "QuickerGridView";

        public override BaseView CreateView(GridControl grid)
        {
            return new QuickerGridView(grid);
        }

        public override BaseViewInfo CreateViewInfo(BaseView view)
        {
            return new QuickerGridViewInfo(view as QuickerGridView);
        }

        public override BaseViewHandler CreateHandler(BaseView view)
        {
            return new QuickerGridViewHandler(view as QuickerGridView);
        }

        public override BaseViewPainter CreatePainter(BaseView view)
        {
            return new QuickerGridViewPainter(view as QuickerGridView);
        }
    }
}
