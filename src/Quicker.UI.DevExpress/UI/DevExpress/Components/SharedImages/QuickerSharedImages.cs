using DevExpress.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quicker.UI.DevExpress.Icons.ExtNet;

namespace Quicker.UI.DevExpress.Components.SharedImages
{

    public class QuickerSharedImages : ImageCollection
    {


        public QuickerSharedImages() : base(new System.ComponentModel.Container())
        {
            //this.ImageStream = ImageCollectionExtNet.Initialize.ImageCollection.ImageStream;
            InitializeComponent();
        }


        private void InitializeComponent()
        {


            ((System.ComponentModel.ISupportInitialize)this).BeginInit();

            // 
            // imageCollection1
            // 
            ImageStream = ImageCollectionExtNet.Initialize.ImageCollection.ImageStream;


            ((System.ComponentModel.ISupportInitialize)this).EndInit();
        }


        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {

            base.Dispose(disposing);
        }
    }

}


