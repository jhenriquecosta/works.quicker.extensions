using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Registrator;
using DevExpress.XtraLayout;
using System;
using System.Windows.Forms;
using Quicker.UI.DevExpress.Components.Inputs.Controls;
using Quicker.UI.DevExpress.Components.Inputs;
using Quicker.UI.DevExpress.Shared.Enums;

namespace Quicker.UI.DevExpress.Helpers
{
    public static class QuickerDevExpressHelper
    {


        public static bool Pergunta(string strPergunta, string strCaption = "ORION Sistemas")
        {
            return XtraMessageBox.Show(strPergunta, strCaption, MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes;
        }
        public static void Informa(string message, string strCaption = "ATENÇÃO")
        {
            XtraMessageBox.Show(message, strCaption, MessageBoxButtons.OK,MessageBoxIcon.Information);
        }
        public static void Error(string message, string strCaption = "ERROR!")
        {
            XtraMessageBox.Show(message, strCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);

        }
        public static void ProcessAllControls(Control rootControl, Action<Control> action)
        {
            foreach (Control childControl in rootControl.Controls)
            {
                ProcessAllControls(childControl, action);
                action(childControl);
            }
        }

        public static BaseEdit ChangeType(string editorTypeName, BaseEdit editor)
        {
            return QuickerDevExpressEditorHelper.ChangeType(editorTypeName, editor);
        }

        public static BaseEdit ChangeEditor(string editorTypeName, BaseEdit sourceEditor)
        {
            return QuickerDevExpressEditorHelper.ChangeEditor(editorTypeName, sourceEditor);

        }

        public static BaseEdit ConvertEditorType(string editorTypeName, BaseEdit sourceEditor, Control container)
        {
            var layoutControl = container;
            var currEdit = sourceEditor;
            if (layoutControl is LayoutControl)
            {
                ((LayoutControl)layoutControl).Root.BeginUpdate();
                var item = ((LayoutControl)layoutControl).GetItemByControl(container);
                item.Owner.BeginUpdate();
                EditorClassInfo info = EditorRegistrationInfo.Default.Editors[editorTypeName];
                if (info == null) return null;
                BaseEdit edit = info.CreateEditor();
                edit.Location = sourceEditor.Location;
                edit.Size = sourceEditor.Size;
                edit.Parent = sourceEditor.Parent;
                edit.Properties.Assign(sourceEditor.Properties);
                edit.Update();
                edit.Invalidate();
                item.Control = edit;
                item.Owner.EndUpdate();
                ((LayoutControl)layoutControl).Root.EndUpdate();
                sourceEditor.Dispose();
                sourceEditor = null;
                currEdit = edit;
            }
            return currEdit;
        }
        public static void InitData(QuickerEditor edit, object record, string[] members)
        {
            //edit.Properties.ValueMember = members[0];
            //edit.Properties.DisplayMember = members[1];
            //edit.Properties.DataSource = record;
        }
        public static void AddEnumToCombo(QuickerEditor edit, Type record)
        {
            if (edit.Properties.Editor == EditorType.ComboEditImage)
            {
                // var props = (XTComboEditImage)edit.Properties.Control;
                edit.GetEditor<QuickerComboEditImage>().Properties.AddEnum(record);
                //   props.Properties.AddEnum

            }
        }

    }
}
