using DevExpress.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using Quicker.Data.Common;
using Quicker.Helpers;
using Quicker.UI.DevExpress.Components.SharedImages;
using Quicker.Extensions;
namespace Quicker.UI.DevExpress.Icons
{

    public class QuickerIconLibrary<TEnum> where TEnum : Enum
    {
        private static readonly Lazy<QuickerIconLibrary<TEnum>> lazy = new Lazy<QuickerIconLibrary<TEnum>>(() => QuickerReflection.CreateInstance<QuickerIconLibrary<TEnum>>());
        List<Item> _icons;
        ImageCollection _imageCollection;
        public QuickerIconLibrary()
        {
            _icons = QuickerEnum.GetItems<TEnum>();
            var desc = QuickerReflection.GetDescription<TEnum>();
            _imageCollection = new QuickerSharedImages();



        }
        public List<Item> Icons => _icons;

        public static QuickerIconLibrary<TEnum> Initialize()
        {
            var ret = lazy.Value;
            return ret;
        }

        public ImageCollection ImageList => _imageCollection;
        public int ByIndex(Enum value)
        {
            var desc = value.Description();
            var icon = _icons.FirstOrDefault(key => key.Text.Equals(desc));
            return icon.SortId.ChangeType<int>();
        }
        public Enum ByDescription(Enum value)
        {
            var desc = value.Description();
            var icon = _icons.FirstOrDefault(key => key.Text.Equals(desc));
            return (TEnum)icon.Value;
        }
        public Enum Get(Enum value)
        {
            var desc = value.Description();
            var icon = _icons.FirstOrDefault(key => key.Text.Equals(desc));
            return (TEnum)icon.Value;
        }
    }
}

