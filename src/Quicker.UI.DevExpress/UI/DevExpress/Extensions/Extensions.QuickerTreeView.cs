using DevExpress.Utils;
using DevExpress.XtraEditors.Controls;
using Quicker.DevExpress.Ui.Components;
using System.Collections;


namespace Quicker.Ui.DevExpressEx.Extensions
{
    public static partial class DevExpressExtensions
    {
        public static QuickerTreeView AddDataSource(this QuickerTreeView editor, object data)
        {
            editor.DataSource = data;
            
            //// Show hierarchy lines
            //editor.OptionsView.ShowTreeLines = DevExpress.Utils.DefaultBoolean.True;
            //// Hierarchy line style 
            //editor.OptionsView.TreeLineStyle = LineStyle; // Solid, Light, Dark, Wide, Large
            return editor;
        }
        public static QuickerTreeView WithParent(this QuickerTreeView editor, string strFieldName)
        {
            editor.ParentFieldName = strFieldName;
            return editor;
        }

        public static QuickerTreeView WithColumn(this QuickerTreeView editor, string strFieldName)
        {
            //To display single - column data in the TreeView style, 
            //  specify the display column with the TreeList.TreeViewColumn or TreeList.TreeViewFieldName property.
            editor.TreeViewFieldName = strFieldName;
            return editor;
        }


    }
}
