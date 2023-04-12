using System.Drawing;

using DevExpress.XtraEditors;

using System;
using System.ComponentModel;
using DevExpress.Utils;
using Enum = System.Enum;
using Quicker.Ui.DevExpressEx.Components;
using Quicker.UI.DevExpress.Shared.Enums;
using Quicker.UI.DevExpress.Icons;
using Quicker.UI.DevExpress.Components.SharedImages;

namespace Quicker.UI.DevExpress.Components.Buttons
{
    [ToolboxItem(true)]
    public class QuickerButton : SimpleButton
    {
        private ButtonType action = ButtonType.Save;
        private IconLibraryExtNet icon;
        QuickerIconLibrary<IconLibraryExtNet> worksIconLibrary = QuickerIconLibrary<IconLibraryExtNet>.Initialize();

        public QuickerButton()
        {
            Initialize();


        }

        private void Initialize()
        {
            var imageCollectionExtNet = new QuickerSharedImages();
            icon = (IconLibraryExtNet)worksIconLibrary.Get(IconLibraryExtNet.ApplicationViewIcons);
            ImageOptions.ImageList = worksIconLibrary.ImageList;
            ImageOptions.ImageIndex = worksIconLibrary.ByIndex(IconLibraryExtNet.Find);
            MinimumSize = new Size(100, 30);
            Font = new Font(AppearanceObject.DefaultFont, FontStyle.Bold);
            Size = new Size(100, 30);
            Text = "&Gravar";
            ImageOptions.ImageIndex = worksIconLibrary.ByIndex(IconLibraryExtNet.DatabaseSave);

        }

        [Browsable(true)]
        [Category("Quicker"), Description("Sets or Gets the Icon")]
        public IconLibraryExtNet Icon
        {
            get
            {
                return icon;
            }
            set
            {
                icon = (IconLibraryExtNet)worksIconLibrary.Get(value);
                ImageOptions.ImageIndex = worksIconLibrary.ByIndex(icon);
                OnControlChanged();
            }
        }

        [Browsable(true)]
        [Category("Quicker"), Description("Sets or Gets the Control text")]
        public ButtonType Action
        {
            get
            {
                return action;
            }
            set
            {
                action = (ButtonType)Enum.Parse(typeof(ButtonType), value.ToString());
                OnControlChanged();
            }
        }
        protected virtual void OnControlChanged()
        {
            GetComando();
            OnPropertiesChanged();

        }
        private void GetComando()
        {
            if (action == ButtonType.Save)
            {
                ImageOptions.ImageIndex = worksIconLibrary.ByIndex(IconLibraryExtNet.DatabaseSave);
            }
            if (action == ButtonType.Exit)
            {
                ImageOptions.ImageIndex = worksIconLibrary.ByIndex(IconLibraryExtNet.Cancel);
            }

        }
    }
}
