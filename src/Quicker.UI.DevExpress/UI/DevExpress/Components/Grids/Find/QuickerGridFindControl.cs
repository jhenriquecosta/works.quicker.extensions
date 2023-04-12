using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Controls;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraLayout;
using DevExpress.XtraLayout.Utils;

using System.Drawing;
using System.Windows.Forms;
using Quicker.UI.DevExpress.Components.Grids.GridView;
using Quicker.UI.DevExpress.Icons;

namespace Quicker.UI.DevExpress.Components.Grids.Find
{

    public class QuickerGridFindControl : FindControl
    {
        QuickerGridView _worksGridView;
        QuickerIconLibrary<IconLibraryExtNet> worksIconLibrary = QuickerIconLibrary<IconLibraryExtNet>.Initialize();
        public QuickerGridFindControl(ColumnView view, object properties) : base(view, properties)
        {
            _worksGridView = view as QuickerGridView;
            CustomizeControl();
        }


        private void CustomizeControl()
        {
            CustomizeButtons();
            CustomizeEditor();
            CustomizeLayoutControl();
        }
        private Control FindControl(string controlName)
        {
            return findLayoutControl.GetControlByName(controlName);
        }
        private void CustomizeButtons()
        {
            //create button

            ButtonEdit be = FindControl("teFind") as ButtonEdit;
            SimpleButton clear = FindControl("btClear") as SimpleButton;
            SimpleButton find = FindControl("btFind") as SimpleButton;


            clear.MinimumSize = new Size(100, 0);
            clear.Font = new Font(AppearanceObject.DefaultFont, FontStyle.Bold);
            clear.ImageOptions.ImageList = worksIconLibrary.ImageList;
            clear.ImageOptions.ImageIndex = worksIconLibrary.ByIndex(IconLibraryExtNet.PageWhite);

            find.MinimumSize = new Size(100, 0);
            find.Font = new Font(AppearanceObject.DefaultFont, FontStyle.Bold);
            find.ImageOptions.ImageList = worksIconLibrary.ImageList;
            find.ImageOptions.ImageIndex = worksIconLibrary.ByIndex(IconLibraryExtNet.Find);

            var add = CreateButton("Adicionar");
            var edit = CreateButton("Editar");
            var print = CreateButton("Print");

            AddButton(add, be, InsertType.Left);
            AddButton(edit, be, InsertType.Left);
            AddButton(print, ClearButton, InsertType.Right);
            // add.MinimumSize = new Size(100, 0);
            add.ImageOptions.ImageIndex = worksIconLibrary.ByIndex(IconLibraryExtNet.Add);
            edit.ImageOptions.ImageIndex = worksIconLibrary.ByIndex(IconLibraryExtNet.NoteEdit);
            print.ImageOptions.ImageIndex = worksIconLibrary.ByIndex(IconLibraryExtNet.Printer);
            add.Click += (s, e) =>
            {
                _worksGridView.FocusedRowHandle = GridControl.NewItemRowHandle;
                _worksGridView.ShowEditForm();
            };





        }
        private SimpleButton CreateButton(string caption, string icon = "")
        {
            SimpleButton btn = new SimpleButton
            {
                Text = caption,
                Size = new Size(100, 0),
                Font = new Font(AppearanceObject.DefaultFont, FontStyle.Bold)
            };
            btn.ImageOptions.ImageList = worksIconLibrary.ImageList;
            return btn;
        }
        private void AddButton(SimpleButton btn, Control control, InsertType insertType)
        {
            // var parent = layoutControl1.GetItemByControl(control);
            //  LayoutControlItem lci = layoutControl1.Root.AddItem(string.Empty, btn,parent, insertType);           
            // lci.SizeConstraintsType = SizeConstraintsType.Custom;
            // lci.TextVisible = false;
            // Size newSize = new Size(ClearButton.Size.Width, ClearButton.Size.Height + 10);
            // lci.MaxSize = newSize;
            // lci.MinSize = newSize;


        }
        //private void AddItem(int item,)

        private void CustomizeEditor()
        {
            //ButtonEdit be = FindControl("teFind") as ButtonEdit;
            //be.Properties.Buttons.Add(new EditorButton(ButtonPredefines.Ellipsis));
            //be.ButtonClick += be_ButtonClick;
            //be.Properties.NullValuePrompt = "MyCustomPanel";
            //be.Properties.NullValuePromptShowForEmptyValue = true;
        }

        void be_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            //if (e.Button.Kind == ButtonPredefines.Ellipsis)
            //    using (Form form = new Form())
            //    {
            //        form.ShowDialog();
            //    }
        }


        private void CustomizeLayoutControl()
        {
            // layoutControl1.AllowCustomization = true;
        }
    }
}
