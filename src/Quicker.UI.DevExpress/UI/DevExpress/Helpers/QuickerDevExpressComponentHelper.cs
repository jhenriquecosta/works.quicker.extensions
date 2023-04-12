using DevExpress.XtraBars.Navigation;
using DevExpress.XtraEditors;
using DevExpress.XtraNavBar;
using DevExpress.XtraTreeList;
using System.Windows.Forms;
using DevExpress.XtraLayout;
using DevExpress.Utils;
using DevExpress.XtraTreeList.Columns;
using Quicker.UI.DevExpress.Icons;
using DevExpress.XtraGrid;

namespace Quicker.UI.DevExpress.Helpers
{
    public static class QuickerDevExpressComponentHelper
    {

        public static LayoutControlGroup LayoutControlGroupBuilder(string name)
        {
            return new LayoutControlGroup
            {
                EnableIndentsWithoutBorders = DefaultBoolean.True,
                GroupBordersVisible = false,
                Name = "Root",
                Size = new System.Drawing.Size(702, 240),
                TextVisible = false,
            };

        }
        public static LayoutControl LayoutControlBuilder(string name = null)
        {
            return new LayoutControl
            {
                CustomizationMode = CustomizationModes.Quick,
                Dock = DockStyle.Fill,
                Location = new System.Drawing.Point(0, 0),
                Name = name,
                Size = new System.Drawing.Size(702, 271),
                TabIndex = 0
            };
        }
        public static ImageCollection GetImageCollection => QuickerIconLibrary<IconLibraryExtNet>.Initialize().ImageList;
        public static SimpleButton SimpleButtonBuilder(string strCaption, string strName = null)
        {

            var item = new SimpleButton();
            item.ImageOptions.ImageList = GetImageCollection;
            item.Name = strName ?? "cmd" + strCaption;
            item.Size = new System.Drawing.Size(113, 36);
            item.Text = strCaption;
            return item;
        }
        public static PanelControl PanelControlBuilder()
        {
            var item = new PanelControl
            {
                Dock = DockStyle.Bottom
            };
            return item;
        }
        public static NavBarGroupControlContainer CreateNavBarContainer()
        {
            var navContainer = new NavBarGroupControlContainer();
            navContainer.Appearance.BackColor = System.Drawing.SystemColors.Control;
            navContainer.Appearance.Options.UseBackColor = true;
            return navContainer;
        }
        public static AccordionControlElement CreateItemAccordion(string strNome, string strTexto, string strIcon = null)
        {
            var strIcone = string.IsNullOrWhiteSpace(strIcon) ? "Home;Office2013" : strIcon;
            var item = new AccordionControlElement(); //cria o elemento
            item.Expanded = true;
            item.Name = "item" + strNome;
            item.Text = strTexto;
            item.Style = ElementStyle.Item;
            item.Appearance.Normal.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            item.Appearance.Normal.Options.UseFont = true;
            return item;
        }
        public static NavBarGroup CreateNavBarGroup(string strCaption)
        {
            var group = new NavBarGroup
            {
                Caption = strCaption,
                GroupClientHeight = 600,
                GroupStyle = NavBarGroupStyle.ControlContainer
            };
            group.Appearance.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            group.Appearance.Options.UseFont = true;
            return group;
        }

        public static AccordionControl CreateAccordion(string strName)
        {
            var acc = new AccordionControl
            {
                Name = "acc" + strName,
                AllowItemSelection = true,
                Dock = DockStyle.Fill,
                Location = new System.Drawing.Point(0, 0),
                Images = GetImageCollection
            };
            acc.Appearance.AccordionControl.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            acc.Appearance.AccordionControl.Options.UseFont = true;
            return acc;
        }
        public static NavBarControl CreateNavBar(DockStyle dockStyle =  DockStyle.Fill, NavBarViewKind navBarViewKind = NavBarViewKind.ExplorerBar,ImageCollection imageCollection = null)
        {
            return new NavBarControl
            {
                Dock = dockStyle,
                PaintStyleKind = NavBarViewKind.ExplorerBar,
                SmallImages = imageCollection,
                LargeImages = imageCollection
            };
        }
         public static TreeList CreateTreeList(string name,ImageCollection imageCollection = null)
        {

            var treeMenu = new TreeList
            {
                Cursor = Cursors.Default,
                Name = "tree" + name,
                Dock = DockStyle.Fill,
                Location = new System.Drawing.Point(0, 0),
                Margin = new Padding(3, 2, 3, 2)
            };

            treeMenu.OptionsBehavior.AutoChangeParent = false;
            treeMenu.OptionsBehavior.AutoNodeHeight = false;
            treeMenu.OptionsBehavior.AutoSelectAllInEditor = false;
            treeMenu.OptionsBehavior.CloseEditorOnLostFocus = false;
            treeMenu.OptionsBehavior.Editable = false;
            treeMenu.OptionsBehavior.ResizeNodes = false;
            treeMenu.OptionsBehavior.SmartMouseHover = false;
            treeMenu.OptionsMenu.EnableFooterMenu = false;
            treeMenu.OptionsPrint.PrintHorzLines = false;
            treeMenu.OptionsPrint.PrintVertLines = false;
            treeMenu.OptionsSelection.EnableAppearanceFocusedCell = false;
            treeMenu.OptionsSelection.KeepSelectedOnClick = false;
            treeMenu.OptionsView.ShowHorzLines = false;
            treeMenu.OptionsView.ShowIndicator = false;
            treeMenu.OptionsView.ShowVertLines = false;
            treeMenu.StateImageList = imageCollection;  //ImageHelper.GetImageExtNet();
            treeMenu.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            treeMenu.Appearance.Row.Options.UseFont = true;
            
            return treeMenu;
        }
        
        public static TreeList CreateTreeList(string strNome, object images, object datasource = null, bool createMenu = false)
        {

            var treeMenu = new TreeList
            {
                Cursor = Cursors.Default,
                Name = "tree" + strNome,
                Dock = DockStyle.Fill,
                Location = new System.Drawing.Point(0, 0),
                Margin = new Padding(3, 2, 3, 2)
            };

            treeMenu.OptionsBehavior.AutoChangeParent = false;
            treeMenu.OptionsBehavior.AutoNodeHeight = false;
            treeMenu.OptionsBehavior.AutoSelectAllInEditor = false;
            treeMenu.OptionsBehavior.CloseEditorOnLostFocus = false;
            treeMenu.OptionsBehavior.Editable = false;
            treeMenu.OptionsBehavior.ResizeNodes = false;
            treeMenu.OptionsBehavior.SmartMouseHover = false;
            treeMenu.OptionsMenu.EnableFooterMenu = false;
            treeMenu.OptionsPrint.PrintHorzLines = false;
            treeMenu.OptionsPrint.PrintVertLines = false;
            treeMenu.OptionsSelection.EnableAppearanceFocusedCell = false;
            treeMenu.OptionsSelection.KeepSelectedOnClick = false;
            treeMenu.OptionsView.ShowHorzLines = false;
            treeMenu.OptionsView.ShowIndicator = false;
            treeMenu.OptionsView.ShowVertLines = false;
            treeMenu.StateImageList = images;  //ImageHelper.GetImageExtNet();
            treeMenu.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            treeMenu.Appearance.Row.Options.UseFont = true;


            if (createMenu && datasource != null)
            {
                //   treeMenu.Size = new System.Drawing.Size(644, 339);



                var colItem = new TreeListColumn();
                var colForm = new TreeListColumn();
                var colId = new TreeListColumn();
                var colIcon = new TreeListColumn();

                // 
                // treeMenu
                // 
                treeMenu.Columns.AddRange(new TreeListColumn[]
                {
                  colItem,colForm,colId,colIcon
                });
                // 
                // colItem
                // 
                colItem.Caption = "Item";
                colItem.FieldName = "Name";
                colItem.MinWidth = 23;
                colItem.Name = "colItem";
                colItem.Visible = true;
                colItem.VisibleIndex = 0;
                // 
                // colForm
                // 
                colForm.Caption = "Form";
                colForm.FieldName = "ObjectName";
                colForm.MinWidth = 23;
                colForm.Name = "colForm";

                // 
                // colId
                // 
                colId.Caption = "colId";
                colId.FieldName = "ID";
                colId.MinWidth = 23;
                colId.Name = "colId";
                // 
                // colIcon
                // 
                colIcon.Caption = "colIcon";
                colIcon.FieldName = "ImageCls";
                colIcon.MinWidth = 23;
                colIcon.Name = "colIcon";

                treeMenu.ParentFieldName = "Parent.ID";
                treeMenu.DataSource = datasource;
            }
            // 
            // 
            return treeMenu;
        }

       
    }
}
