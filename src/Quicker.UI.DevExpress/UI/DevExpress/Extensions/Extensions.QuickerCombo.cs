using System;
using DevExpress.Utils;
using DevExpress.XtraEditors.Controls;
using System.Collections;
using Quicker.UI.DevExpress.Components.Inputs.Controls;
using Quicker.UI.DevExpress.Components.Inputs;
using Quicker.UI.DevExpress.Shared.Enums;

namespace Quicker.Ui.DevExpressEx.Extensions
{
    public static partial class DevExpressExtensions
    {
        public static void GetDataFrom(this QuickerEditor editor, object data, string displayMember, string valueMember = "This")
        {
            if (editor.Properties.Editor != EditorType.ComboEditLookUp) return;
            var edit = editor.GetEditor<QuickerComboEditLookUp>().Properties;
          
            edit.ValueMember = valueMember;
            edit.DisplayMember = displayMember;
            edit.DataSource = data;
            edit.Columns.Clear();
            edit.Columns.Add(new LookUpColumnInfo(displayMember));
            edit.ShowHeader = false;
            edit.DropDownRows = 14;
        }
        public static QuickerEditor GetDataFrom(this QuickerEditor editor, Type data)
        {   
            if (editor.Properties.Editor != EditorType.ComboEditImage) return editor;
            editor.GetEditor<QuickerComboEditImage>().Properties.AddEnum(data);
            return editor;
        }


        public static void GetImages(this QuickerComboEditImage editor, ImageCollection imageCollection)
        {
            var imgs = imageCollection.Images;
            var dataImage = new ArrayList();
            for (var i = 0; i < imgs.Count; i++)
            {
                dataImage.Add(new ImageComboBoxItem(imgs.Keys[i], i));
            }
            editor.LargeImages(imageCollection);
            editor.SmallImages(imageCollection);
            editor.AddImages(dataImage);
        }
        public static void AddImages(this QuickerComboEditImage editor, IList images)
        {
            editor.Properties.Items.AddRange(images);

        }
        public static void LargeImages(this QuickerComboEditImage editor, ImageCollection imageCollection)
        {
            editor.Properties.LargeImages = imageCollection;
        }
        public static void SmallImages(this QuickerComboEditImage editor, ImageCollection imageCollection)
        {
            editor.Properties.SmallImages = imageCollection;
        }

    }
}
