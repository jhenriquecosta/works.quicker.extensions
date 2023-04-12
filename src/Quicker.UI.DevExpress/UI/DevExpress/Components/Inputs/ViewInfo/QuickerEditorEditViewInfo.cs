using System;
using System.Drawing;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.ViewInfo;
using Quicker.UI.DevExpress.Components.Base.Contracts;

namespace Quicker.UI.DevExpress.Components.Inputs.ViewInfo
{
    public class QuickerEditorEditViewInfo : BaseEditViewInfo
    {
        private Bitmap drawBitmap;

        public QuickerEditorEditViewInfo(RepositoryItem item) : base(item)
        {
        }

        public override bool AllowDrawFocusRect
        {
            get { return false; }
            set { }
        }

        public IQuickerEditor DrawControlInstance
        {
            get { return Item.GetDrawControlInstance(); }
        }

        public new QuickerEditorRepositoryItem Item
        {
            get { return base.Item as QuickerEditorRepositoryItem; }
        }

        public override void CalcViewInfo(Graphics g)
        {
            base.CalcViewInfo(g);
            //todo
        }

        protected override Size CalcContentSize(Graphics g)
        {
            Size res = base.CalcContentSize(g);
            if (DrawControlInstance == null) return res;
            Size editor = DrawControlInstance.CalcSize(g);
            res.Height = Math.Max(editor.Height, res.Height);
            res.Width = Math.Max(editor.Width, res.Width);
            return res;
        }

        protected override void OnEditValueChanged()
        {
            base.OnEditValueChanged();
            if (DrawControlInstance != null)
            {
                DrawControlInstance.EditValue = EditValue;
                SetDisplayText(DrawControlInstance.GetDisplayText(EditValue));
            }
        }

        public override void Dispose()
        {
            if (drawBitmap != null) drawBitmap.Dispose();
            drawBitmap = null;
            base.Dispose();
        }

        public Bitmap EnsureBitmap(Size size)
        {
            if (drawBitmap != null && (drawBitmap.Width < size.Width || drawBitmap.Height < size.Height))
            {
                drawBitmap.Dispose();
                drawBitmap = null;
            }
            if (drawBitmap == null)
            {
                drawBitmap = new Bitmap(size.Width, size.Height);
            }
            return drawBitmap;
        }
    }
}