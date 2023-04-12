using System.Drawing;
using System.Windows.Forms;
using DevExpress.Utils.Drawing;
using DevExpress.XtraEditors.Drawing;
using Quicker.UI.DevExpress.Components.Base.Contracts;
using Quicker.UI.DevExpress.Components.Inputs.ViewInfo;

namespace Quicker.UI.DevExpress.Components.Inputs.Painter
{
    public class QuickerEditorEditPainter : BaseEditPainter
    {
        public override void Draw(ControlGraphicsInfoArgs info)
        {
            base.Draw(info);
        }

        protected override void DrawContent(ControlGraphicsInfoArgs info)
        {
            var viewInfo = info.ViewInfo as QuickerEditorEditViewInfo;
            info.ViewInfo.PaintAppearance.DrawBackground(info.Cache, info.Bounds);
            IQuickerEditor drawControl = viewInfo.DrawControlInstance;
            if (drawControl == null)
            {
                DrawEmptyContent(info);
                return;
            }
            if (viewInfo.OwnerEdit != null) return;
            if (drawControl.SupportsDraw)
            {
                drawControl.Draw(info.Cache, viewInfo);
            }
            else
            {
                DrawAsBitmap(info.Cache, drawControl, viewInfo);
            }
        }

        private void DrawEmptyContent(ControlGraphicsInfoArgs info)
        {
            info.Cache.FillRectangle(Brushes.Red, info.Bounds);
            info.ViewInfo.PaintAppearance.DrawString(info.Cache, "Empty", info.Bounds);
        }

        private void DrawAsBitmap(GraphicsCache cache, IQuickerEditor drawControl, QuickerEditorEditViewInfo viewInfo)
        {
            var c = drawControl as Control;
            if (c == null) return;
            drawControl.EditValue = viewInfo.EditValue;
            c.Size = viewInfo.ContentRect.Size;
            c.BackColor = viewInfo.PaintAppearance.BackColor;
            c.ForeColor = viewInfo.PaintAppearance.ForeColor;
            Bitmap bitmap = viewInfo.EnsureBitmap(viewInfo.ContentRect.Size);
            c.DrawToBitmap(bitmap, new Rectangle(Point.Empty, viewInfo.ContentRect.Size));
            cache.Paint.DrawImage(cache.Graphics, bitmap, viewInfo.ContentRect,
                new Rectangle(Point.Empty, viewInfo.ContentRect.Size), true);
        }
    }
}