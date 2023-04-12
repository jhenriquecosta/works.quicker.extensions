using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.Utils.Drawing;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using Quicker.UI.DevExpress.Components.Base.Contracts;
using Quicker.UI.DevExpress.Components.Inputs.Settings;

namespace Quicker.UI.DevExpress.Components.Inputs
{



    [ToolboxItem(true)]
    public class QuickerEditor : BaseEdit
    {
        readonly QuickerEditorSettings _worksEditorSettings = new QuickerEditorSettings();
        static QuickerEditor()
        {
            QuickerEditorRepositoryItem.Register();
        }
        public QuickerEditor()
        {
            Properties.Control = Properties.GetControl(this);
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

        }


        public override string EditorTypeName
        {
            get { return QuickerEditorRepositoryItem.EditorName; }
        }

        //settings
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Editor("Quicker.DevExpressEx.Ui.Settings", typeof(System.Drawing.Design.UITypeEditor))]
        public virtual QuickerEditorSettings Settings
        {
            get
            {
                return _worksEditorSettings;
            }
        }



        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public new QuickerEditorRepositoryItem Properties
        {
            get { return base.Properties as QuickerEditorRepositoryItem; }
        }

        [Browsable(false)]
        public override bool IsEditorActive
        {
            get
            {
                if (!Enabled)
                    return false;
                IContainerControl container = GetContainerControl();
                if (container == null) return EditorContainsFocus;
                return container.ActiveControl == this || Contains(container.ActiveControl);
            }
        }

        [Browsable(false)]
        public T GetEditor<T>()
        {
            return (T)Properties.Control;
        }
        protected override Control InnerControl
        {
            get { return Properties.Control as Control; }
        }

        public override bool IsNeededKey(KeyEventArgs e)
        {
            return base.IsNeededKey(e) || Properties.Control != null && Properties.Control.IsNeededKey(e);
        }

        public override bool AllowMouseClick(Control control, Point p)
        {
            p = PointToClient(p);
            if (base.AllowMouseClick(control, p)) return true;
            if (GetChildAtPoint(p) != null) return true;
            if (Properties.Control != null && Properties.Control.AllowClick(p)) return true;
            return false;
        }

        protected override bool ProcessKeyMessage(ref Message m)
        {
            return base.ProcessKeyMessage(ref m);
        }

        protected override bool ProcessKeyPreview(ref Message m)
        {
            if (InplaceType != InplaceType.Standalone)
            {
                return ProcessKeyMessage(ref m);
            }
            return base.ProcessKeyPreview(ref m);
        }

        internal void OnControlSubscribe(IQuickerEditor control)
        {
            var c = control as Control;
            if (c != null)
            {
                if (!DesignMode)
                {
                    c.Dock = DockStyle.None;
                    c.Parent = this;
                    c.PreviewKeyDown += OnControlPreviewKeyDown;
                    c.ParentChanged += OnControl_ParentChanged;
                    c.Visible = true;
                }
            }
            control.EditValueChanged += OnControlEditValueChanged;
        }

        private void OnControlPreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
        }

        internal void OnControlRelease(IQuickerEditor control)
        {
            var c = control as Control;
            if (c != null)
            {
                c.ParentChanged -= OnControl_ParentChanged;
                c.PreviewKeyDown -= OnControlPreviewKeyDown;
            }
            if (c != null && Controls.Contains(c))
            {
                if (!DesignMode) c.Parent = null;
            }
            control.EditValueChanged -= OnControlEditValueChanged;
        }

        private void OnControlEditValueChanged(object sender, EventArgs e)
        {
            if (Properties.Control != null)
            {
                EditValue = Properties.Control.EditValue;
                IsModified = true;
            }
        }

        private void OnControl_ParentChanged(object sender, EventArgs e)
        {
            var c = sender as Control;
            if (c.Parent != this && Properties.Control == c) c.Parent = this;
        }

        protected override void LayoutChanged()
        {
            base.LayoutChanged();
            UpdateControlBounds();
        }

        protected override void OnAfterUpdateViewInfo()
        {
            base.OnAfterUpdateViewInfo();
            UpdateControlBounds();
        }

        protected void UpdateControlBounds()
        {
            var c = Properties.Control as Control;
            if (c != null)
            {
                if (!ViewInfo.IsReady)
                    c.Bounds = ClientRectangle;
                else
                    c.Bounds = ViewInfo.ContentRect;
            }
        }

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            var c = Properties.Control as Control;
            if (c != null)
            {
                if (ContainsFocus && !c.ContainsFocus) c.Focus();
            }
        }
    }

    //public class ControlEdit : IAnyControlEdit
    //{
    //	#region IAnyControlEdit Members

    //	object IAnyControlEdit.EditValue { get; set; }

    //	event EventHandler IAnyControlEdit.EditValueChanged
    //	{
    //		add { editValueChanged += value; }
    //		remove { editValueChanged -= value; }
    //	}

    //	bool IAnyControlEdit.IsNeededKey(KeyEventArgs e)
    //	{
    //		return false;
    //	}

    //	void IAnyControlEdit.SetupAsDrawControl()
    //	{
    //	}

    //	void IAnyControlEdit.SetupAsEditControl()
    //	{
    //	}

    //	Size IAnyControlEdit.CalcSize(Graphics g)
    //	{
    //		return Size.Empty;
    //	}

    //	bool IAnyControlEdit.AllowClick(Point p)
    //	{
    //		return false;
    //	}

    //	bool IAnyControlEdit.SupportsDraw
    //	{
    //		get { return true; }
    //	}

    //	bool IAnyControlEdit.AllowBorder
    //	{
    //		get { return true; }
    //	}

    //	string IAnyControlEdit.GetDisplayText(object editValue)
    //	{
    //		if (editValue == null || ReferenceEquals(editValue, DBNull.Value)) return "";
    //		return editValue.ToString();
    //	}

    //	void IAnyControlEdit.Draw(GraphicsCache cache, AnyControlEditViewInfo viewInfo)
    //	{
    //		viewInfo.PaintAppearance.FillRectangle(cache, viewInfo.ContentRect);
    //		viewInfo.PaintAppearance.DrawString(cache, viewInfo.DisplayText, viewInfo.ContentRect);
    //	}

    //	private event EventHandler editValueChanged;

    //	private void OnEditValueChanged()
    //	{
    //		if (editValueChanged != null) editValueChanged(this, EventArgs.Empty);
    //	}

    //	#endregion
    //}

    //public class ControlEditAlt : UserControl, IQuickerEditor
    //{
    //	private readonly TextBox te;
    //	private int lockValueChanged;

    //	public ControlEditAlt()
    //	{
    //		te = new TextBox();
    //		te.TextChanged += OnTextChanged;
    //		te.BorderStyle = BorderStyle.None;
    //		Controls.Add(te);
    //	}

    //	event EventHandler IQuickerEditor.EditValueChanged
    //	{
    //		add { editValueChanged += value; }
    //		remove { editValueChanged -= value; }
    //	}

    //	protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
    //	{
    //		base.SetBoundsCore(x, y, width, height, specified);
    //		if (te != null)
    //		{
    //			te.Bounds = new Rectangle(0, 0, width, height);
    //			if (te.Height < height) te.Top += (height - te.Height)/2;
    //		}
    //	}

    //	private event EventHandler editValueChanged;

    //	private void OnEditValueChanged()
    //	{
    //		if (editValueChanged != null) editValueChanged(this, EventArgs.Empty);
    //	}

    //	private void OnTextChanged(object sender, EventArgs e)
    //	{
    //		if (lockValueChanged > 0) return;
    //		editValue = te.Text;
    //		OnEditValueChanged();
    //	}

    //	#region IAnyControlEdit Members

    //	private object editValue;

    //	object IQuickerEditor.EditValue
    //	{
    //		get { return editValue; }
    //		set
    //		{
    //			editValue = value;
    //			lockValueChanged ++;
    //			try
    //			{
    //				te.Text = ((IQuickerEditor) this).GetDisplayText(editValue);
    //			}
    //			finally
    //			{
    //				lockValueChanged--;
    //			}
    //		}
    //	}

    //	bool IQuickerEditor.IsNeededKey(KeyEventArgs e)
    //	{
    //		return false;
    //	}

    //	string IQuickerEditor.GetDisplayText(object editValue)
    //	{
    //		if (editValue == null || ReferenceEquals(editValue, DBNull.Value)) return "";
    //		return editValue.ToString();
    //	}

    //	Size IQuickerEditor.CalcSize(Graphics g)
    //	{
    //		return new Size(10, 20);
    //	}

    //	bool IQuickerEditor.SupportsDraw
    //	{
    //		get { return false; }
    //	}

    //	bool IQuickerEditor.AllowBorder
    //	{
    //		get { return true; }
    //	}

    //	void IQuickerEditor.SetupAsDrawControl()
    //	{
    //	}

    //	void IQuickerEditor.SetupAsEditControl()
    //	{
    //	}

    //	bool IQuickerEditor.AllowClick(Point p)
    //	{
    //		return false;
    //	}

    //	void IQuickerEditor.Draw(GraphicsCache cache, QuickerEditorEditViewInfo viewInfo)
    //	{
    //		throw new NotImplementedException();
    //	}

    //	protected override void OnBackColorChanged(EventArgs e)
    //	{
    //		base.OnBackColorChanged(e);
    //		te.BackColor = BackColor;
    //	}

    //	protected override void OnForeColorChanged(EventArgs e)
    //	{
    //		base.OnForeColorChanged(e);
    //		te.ForeColor = ForeColor;
    //	}

    //	protected override bool IsInputKey(Keys keyData)
    //	{
    //		return base.IsInputKey(keyData);
    //	}

    //	#endregion
    //}
}