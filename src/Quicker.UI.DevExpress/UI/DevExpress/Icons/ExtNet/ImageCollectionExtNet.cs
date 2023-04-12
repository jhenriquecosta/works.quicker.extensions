using DevExpress.Utils;
using Quicker.Extensions;
using Quicker.Helpers;
using Quicker.UI.Common;
using System;

namespace Quicker.UI.DevExpress.Icons.ExtNet
{
    public class ImageCollectionExtNet
    {
        private static readonly Lazy<ImageCollectionExtNet> lazy = new Lazy<ImageCollectionExtNet>(() => QuickerReflection.CreateInstance<ImageCollectionExtNet>());
        private ImageCollection imageCollection = null;
        public ImageCollectionExtNet()
        {
            AddImages();
        }
        public static ImageCollectionExtNet Initialize { get { return lazy.Value; } }
        public ImageCollection ImageCollection => imageCollection;

        private void AddImages()
        {
            // 
            var appCurrent = AppDomain.CurrentDomain.BaseDirectory;


            var icons = typeof(QuickerUICommonModule).Assembly.GetManifestResourceNames();


            imageCollection = new ImageCollection();
            var prefix = "Quicker.UI.Icons.ExtNet.";
            foreach (var item in icons)
            {
                if (item.Contains(prefix) && item.Contains(".png"))
                {
                    var image = ResourceImageHelperCore.CreateImageFromResources(item, typeof(QuickerUICommonModule).Assembly); ;
                    var name = item.RemovePreFix(prefix);
                    imageCollection.AddImage(image, name);
                }


            }

        }
    }
}
