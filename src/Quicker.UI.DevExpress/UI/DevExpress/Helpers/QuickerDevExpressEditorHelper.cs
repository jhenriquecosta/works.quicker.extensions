
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Registrator;
using System.Collections;
using DevExpress.Utils;
using System.ComponentModel.Design;
using System.ComponentModel;
using System.Drawing;


namespace Quicker.UI.DevExpress.Helpers
{
    class QuickerDevExpressEditorHelper
    {
        public static BaseEdit ChangeEditor(string editorTypeName, BaseEdit editor)
        {

            var newEdit = ChangeType(editorTypeName, editor);
            editor = ChangeType(editorTypeName, newEdit);
            newEdit.Dispose();
            editor.BringToFront();
            return editor;
        }
        public static BaseEdit ChangeType(string editorTypeName, BaseEdit editor)
        {
            string name = null;
            BaseEdit edit = null;
            try
            {
                EditorClassInfo info = EditorRegistrationInfo.Default.Editors[editorTypeName];
                if (info == null) throw new ArgumentException("Editor not found");
                edit = info.CreateEditor();
                Hashtable properties = new Hashtable(), events = new Hashtable();
                CopyProperties(editor, typeof(Control), properties);
                bool? autoHeight = null;
                if (!editor.Properties.AutoHeight)
                {
                    if (editor is MemoEdit || editor is PictureEdit)
                        autoHeight = true;
                    else
                        autoHeight = false;
                }
                edit.Properties.Assign(editor.Properties);
                edit.EditValue = editor.EditValue;
                ArrayList biCache = new ArrayList(editor.DataBindings);
                foreach (Binding bi in biCache)
                {
                    try
                    {
                        editor.DataBindings.Remove(bi);
                        edit.DataBindings.Add(bi);
                    }
                    catch { }
                }
                CopyBaseEditProperties(editor, edit);
                CopyButtons(editor, edit);
                Rectangle bounds = editor.Bounds;
                Control parent = editor.Parent;
                name = editor.Name;
                if (edit.Parent == null) edit.Parent = parent;
                edit.Bounds = bounds;
                if (edit.Site != null) edit.Site.Name = name;
                PasteProperties(edit, properties);
                if (autoHeight != null) edit.Properties.AutoHeight = autoHeight.Value;
                // edit.BringToFront();
                //   edit.Visible = false;
                //editor.Parent = null;
                //editor.Dispose();
                //editor = null;
            }
            catch
            {

            }
            return edit;
        }

        static void CopyBaseEditProperties(BaseEdit source, BaseEdit dest)
        {
            dest.TabStop = source.TabStop;
            dest.MenuManager = source.MenuManager;
            dest.ToolTipController = source.ToolTipController;
            dest.ToolTip = source.ToolTip;
            dest.ToolTipIconType = source.ToolTipIconType;
            dest.ToolTipTitle = source.ToolTipTitle;
            if (source.SuperTip != null)
                dest.SuperTip = source.SuperTip.Clone() as SuperToolTip;
        }
        static void CopyButtons(BaseEdit source, BaseEdit dest)
        {
            ButtonEdit destButtonEdit = dest as ButtonEdit;
            ButtonEdit sourceButtonEdit = source as ButtonEdit;
            if (destButtonEdit == null || sourceButtonEdit == null) return;
            CopyPopupButtons(source, dest);
            CopySpinButtons(source, dest);
        }
        static void CopySpinButtons(BaseEdit source, BaseEdit dest)
        {
            BaseSpinEdit destSpinBaseEdit = dest as BaseSpinEdit;
            BaseSpinEdit sourceSpinBaseEdit = source as BaseSpinEdit;
            if (destSpinBaseEdit != null && sourceSpinBaseEdit != null || destSpinBaseEdit == null && sourceSpinBaseEdit == null) return;
            if (destSpinBaseEdit != null)
            {
                destSpinBaseEdit.Properties.Buttons.Clear();
                destSpinBaseEdit.Properties.CreateDefaultButton();
            }
        }
        static void CopyPopupButtons(BaseEdit source, BaseEdit dest)
        {
            PopupBaseEdit destPopupBaseEdit = dest as PopupBaseEdit;
            PopupBaseEdit sourcePopupBaseEdit = source as PopupBaseEdit;
            if (destPopupBaseEdit != null && sourcePopupBaseEdit != null || destPopupBaseEdit == null && sourcePopupBaseEdit == null) return;
            ButtonEdit destButtonEdit = dest as ButtonEdit;
            if (destButtonEdit != null)
            {
                destButtonEdit.Properties.Buttons.Clear();
                destButtonEdit.Properties.CreateDefaultButton();
            }
        }
        static void CopyProperties(object source, Type propertiesSource, Hashtable dictionary)
        {
            CopyProperties(source, TypeDescriptor.GetProperties(propertiesSource), dictionary);
        }
        static void CopyProperties(object source, PropertyDescriptorCollection properties, Hashtable dictionary)
        {
            foreach (PropertyDescriptor property in properties)
            {
                if (!AllowCopyProperty(property)) continue;
                try
                {
                    if (!property.ShouldSerializeValue(source)) continue;
                    dictionary[property] = property.GetValue(source);
                }
                catch { }
            }
        }
        static void PasteProperties(object destination, Hashtable dictionary)
        {
            PropertyDescriptorCollection coll = TypeDescriptor.GetProperties(destination);
            foreach (DictionaryEntry entry in dictionary)
            {
                PropertyDescriptor source = entry.Key as PropertyDescriptor;
                PropertyDescriptor dest = coll[source.Name];
                if (dest == null || dest.SerializationVisibility != DesignerSerializationVisibility.Visible) continue;
                if (dest.IsReadOnly || !source.PropertyType.Equals(dest.PropertyType)) continue;
                dest.SetValue(destination, entry.Value);
            }
        }
        static bool AllowCopyProperty(PropertyDescriptor property)
        {
            if (property.Name == "TabStop") return false;
            if (property.IsReadOnly) return false;
            if (!property.IsBrowsable) return false;
            if (property.SerializationVisibility != DesignerSerializationVisibility.Visible) return false;
            return true;
        }

    }
}
