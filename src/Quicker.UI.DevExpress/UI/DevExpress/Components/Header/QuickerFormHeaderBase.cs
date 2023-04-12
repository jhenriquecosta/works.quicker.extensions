using DevExpress.Utils.Frames;
using System;
using DevExpress.Utils.Drawing;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;
using DevExpress.Utils;

namespace Quicker.UI.DevExpress.Components.Header
{
    public class QuickerFormHeaderBase : FrameControl
    {
        protected static Image fImageLogo = null;
        private static Font defaultFont1 = null;
        private static Font defaultFont2 = null;
        private static string defaultFontName = "Arial Narrow";
        private Color defaultBackColor = Color.FromArgb(byte.MaxValue, 195, 113);
        private Color defaultBackColor2 = Color.FromArgb(byte.MaxValue, 131, 12);
        private Image image;
        private Font font2;
        private string text2;
        private bool showLogo;

        public QuickerFormHeaderBase()
        {
            image = null;
            BackColor = defaultBackColor;
            BackColor2 = defaultBackColor2;
            base.GradientMode = LinearGradientMode.Vertical;
            base.ForeColor = Color.White;
            text2 = "";
            showLogo = false;
            ResetFont2();
        }

        public static Image ImageLogo
        {
            get
            {
                if (fImageLogo == null)
                    fImageLogo = ResourceImageHelperCore.CreateImageFromResources("imposto_logo.png", typeof(QuickerFormHeaderBase).Assembly);
                return fImageLogo;
            }
        }

        protected virtual Image DXLogo
        {
            get
            {
                return ImageLogo;
            }
        }

        private static Font DefaultFont1
        {
            get
            {
                if (defaultFont1 == null)
                    defaultFont1 = CreateDefaultFont(defaultFontName, 18, FontStyle.Bold);
                if (defaultFont1.Name != defaultFontName)
                    defaultFont1 = CreateDefaultFont(AppearanceObject.DefaultFont.Name, 16, FontStyle.Bold);
                return defaultFont1;
            }
        }

        private static Font DefaultFont2
        {
            get
            {
                if (defaultFont2 == null)
                    defaultFont2 = CreateDefaultFont(defaultFontName, 10, FontStyle.Regular);
                if (defaultFont2.Name != defaultFontName)
                    defaultFont2 = CreateDefaultFont(AppearanceObject.DefaultFont.Name, 9, FontStyle.Regular);
                return defaultFont2;
            }
        }

        public static Font CreateDefaultFont(string name, int fontSize, FontStyle fontStyle)
        {
            try
            {
                return new Font(new FontFamily(name), fontSize, fontStyle);
            }
            catch
            {
            }
            return DefaultFont;
        }

        public override void ResetBackColor()
        {
            BackColor = defaultBackColor;
        }

        protected override bool ShouldSerializeBackColor()
        {
            return BackColor != defaultBackColor;
        }

        protected override void ResetBackColor2()
        {
            BackColor2 = defaultBackColor2;
        }

        protected override bool ShouldSerializeBackColor2()
        {
            return BackColor2 != defaultBackColor2;
        }

        public void ShowLogo(bool show)
        {
            showLogo = show;
            Invalidate();
        }

        [DefaultValue(LinearGradientMode.Vertical)]
        public override LinearGradientMode GradientMode
        {
            get
            {
                return base.GradientMode;
            }
            set
            {
                base.GradientMode = value;
                BeginResize();
            }
        }

        public override void ResetForeColor()
        {
            ForeColor = Color.White;
        }

        protected virtual bool ShouldSerializeForeColor()
        {
            return ForeColor != Color.White;
        }

        public override Color ForeColor
        {
            get
            {
                return base.ForeColor;
            }
            set
            {
                base.ForeColor = value;
                BeginResize();
            }
        }

        protected override bool ShouldSerializeFont()
        {
            return !Font.Equals(DefaultFont1);
        }

        public override Font Font
        {
            get
            {
                return base.Font;
            }
            set
            {
                if (value == null)
                    value = DefaultFont1;
                base.Font = value;
                BeginResize();
            }
        }

        protected virtual void ResetFont2()
        {
            Font2 = null;
        }

        protected virtual bool ShouldSerializeFont2()
        {
            return !Font2.Equals(DefaultFont2);
        }

        public virtual Font Font2
        {
            get
            {
                return font2;
            }
            set
            {
                if (value == null)
                    value = DefaultFont2;
                font2 = value;
                BeginResize();
            }
        }

        [DefaultValue("")]
        public string Text2
        {
            get
            {
                return text2;
            }
            set
            {
                text2 = value;
                BeginResize();
            }
        }

        protected virtual void ResetImage()
        {
            Image = null;
        }

        protected virtual bool ShouldSerializeImage()
        {
            return Image != null;
        }

        public Image Image
        {
            get
            {
                return image;
            }
            set
            {
                image = value;
                BeginResize();
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public override bool ParentAutoHeight
        {
            get
            {
                return false;
            }
            set
            {
            }
        }

        protected virtual int deltaX
        {
            get
            {
                return 10;
            }
        }

        protected virtual int deltaY
        {
            get
            {
                return -3;
            }
        }

        protected virtual int deltaText2
        {
            get
            {
                return 3;
            }
        }

        protected virtual int CaptionFontHeight
        {
            get
            {
                return Font.Height + Font2.Height + deltaY + deltaX;
            }
        }

        protected virtual int DXLogoHeight
        {
            get
            {
                return DXLogo.Height + deltaX;
            }
        }

        private void SetHeight()
        {
            int val1 = Math.Max(DXLogoHeight, CaptionFontHeight);
            if (image != null)
                val1 = Math.Max(val1, image.Height + deltaX);
            if (Height >= val1)
                return;
            Height = val1;
        }

        protected virtual void FillBackground(GraphicsCache cache, Rectangle r)
        {
            Brush gradientBrush = cache.GetGradientBrush(r, BackColor, BackColor2 == Color.Empty ? BackColor : BackColor2, GradientMode);
            cache.Graphics.FillRectangle(gradientBrush, r);
            BorderHelper.GetPainter(BorderStyle).DrawObject(new BorderObjectInfoArgs(cache, null, r));
        }

        protected virtual void DrawCaptions(GraphicsCache cache, Rectangle r, int textLeft)
        {
            using (SolidBrush solidBrush = new SolidBrush(ForeColor))
            {
                int y = (Height - (Font.Height + (Text2 != "" ? Font2.Height + deltaY : 0))) / 2 - 1;
                r = new Rectangle(textLeft, y, Width - textLeft - DXLogo.Width - deltaX, Font.Height);
                cache.Graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
                if (r.Width > 0)
                    cache.Graphics.DrawString(Text, Font, solidBrush, (RectangleF)r, TextStringFormat);
                r = new Rectangle(textLeft + deltaText2, y + Font.Height + deltaY, Width - textLeft - DXLogo.Width - deltaX - deltaText2, Font2.Height);
                if (r.Width <= 0)
                    return;
                cache.Graphics.DrawString(Text2, Font2, solidBrush, (RectangleF)r, TextStringFormat);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            GraphicsCache cache = new GraphicsCache(e.Graphics);
            if (NeedResize)
                SetHeight();
            Rectangle r = new Rectangle(0, 0, Width, Height);
            FillBackground(cache, r);
            int deltaX = this.deltaX;
            if (image != null && Width > this.deltaX * 2 + image.Width + DXLogo.Width)
            {
                e.Graphics.DrawImage(image, this.deltaX, (Height - image.Height) / 2);
                deltaX += this.deltaX + image.Width;
            }
            if (showLogo)
                e.Graphics.DrawImage(DXLogo, Width - DXLogo.Width - this.deltaX, (Height - DXLogo.Height) / 2);
            DrawCaptions(cache, r, deltaX);
            cache.Dispose();
        }
    }
}
