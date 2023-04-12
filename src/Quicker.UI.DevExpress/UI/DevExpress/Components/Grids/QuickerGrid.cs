using System.ComponentModel;
using DevExpress.XtraGrid.Controls;
using DevExpress.XtraGrid.Registrator;
using DevExpress.XtraGrid.Views.Base;
using Quicker;
using Quicker.UI.DevExpress.Components.Grids.GridView;

namespace Quicker.UI.DevExpress.Components.Grids
{
    [ToolboxItem(true)]
    public class QuickerGrid : QuickerGridControl
    {
        private static QuickerGridView _worksView;
        private bool _worksCustomPanel;

        public QuickerGrid()
        {
            _worksView = MainView as QuickerGridView;
        }
        public QuickerGrid(QuickerGridView gridView) : this()
        {
            _worksView = gridView;
            _worksView.OptionsBehavior.Editable = false;
            _worksView.OptionsFind.FindDelay = 100;
            _worksView.OptionsView.ShowGroupPanel = false;
            MainView = _worksView;
            ViewCollection.AddRange(new BaseView[] { _worksView });
            _worksView.GridControl = this;
            Name = "worksGrid";
            _worksView.Name = "worksView";
        }
        public bool ShowCustomPanel
        {
            get
            {
                return _worksCustomPanel;
            }
            set
            {
                _worksCustomPanel = value;
                GetPanel(_worksCustomPanel);
            }

        }

        private void GetPanel(bool xtCustomPanel)
        {
            CurrentView.OptionsFind.AlwaysVisible = xtCustomPanel;
            if (xtCustomPanel)
            {
                if (CurrentView.IsFindPanelVisible)
                {

                }
            }
        }

        public QuickerGridView CurrentView
        {
            get
            {
                if (_worksView == null) _worksView = MainView as QuickerGridView;
                return _worksView;
            }
            set
            {
                _worksView = value;
            }
        }
        protected override BaseView CreateDefaultView()
        {
            return CreateView("QuickerGridView");
        }

        protected override void RegisterAvailableViewsCore(InfoCollection collection)
        {
            base.RegisterAvailableViewsCore(collection);
            collection.Add(new QuickerGridInfoRegistrator());
        }
    }
}
