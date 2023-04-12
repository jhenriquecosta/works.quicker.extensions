using System;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Registrator;
using DevExpress.XtraEditors.Repository;
using Quicker.UI.DevExpress.Components.Base.Contracts;
using Quicker.UI.DevExpress.Components.Inputs.Controls;
using Quicker.UI.DevExpress.Components.Inputs.Painter;
using Quicker.UI.DevExpress.Components.Inputs.ViewInfo;
using Quicker.UI.DevExpress.Shared.Enums;

namespace Quicker.UI.DevExpress.Components.Inputs

{
    [UserRepositoryItem("Register")]
    public class QuickerEditorRepositoryItem : RepositoryItem
    {
        internal const string EditorName = "QuickerEditorEdit";
        private QuickerEditor _QuickerEditor;
        private IQuickerEditor control;
        private IQuickerEditor drawControlInstance;
        private EditorType tipoEditor = EditorType.TextEdit;

        static QuickerEditorRepositoryItem()
        {
            Register();
        }
        public QuickerEditorRepositoryItem()
        {

        }


        public override string EditorTypeName
        {
            get { return EditorName; }
        }



        [Browsable(true)]
        [Category("Custom Properties"), Description("Sets or Gets the Control text")]

        public EditorType Editor
        {
            get
            {
                return tipoEditor;
            }
            set
            {
                tipoEditor = (EditorType)Enum.Parse(typeof(EditorType), value.ToString());
                Control = GetControl(tipoEditor);
                OnControlChanged();
            }
        }
        public IQuickerEditor GetControl(QuickerEditor xtEdit)
        {
            _QuickerEditor = xtEdit;
            return GetControl(tipoEditor);
        }
        public IQuickerEditor GetControl(EditorType item)
        {
            if (item == EditorType.TextEdit)
            {
                return new QuickerTextEdit
                {
                    Enabled = _QuickerEditor.Enabled,
                    ForeColor = _QuickerEditor.ForeColor,
                    Font = _QuickerEditor.Font
                };
            }
            else if (item == EditorType.ComboEdit)
            {
                return new QuickerComboEdit();
            }
            else if (item == EditorType.ComboEditLookUp)
            {
                return new QuickerComboEditLookUp();
            }
            else if (item == EditorType.ComboEditImage)
            {
                return new QuickerComboEditImage();
            }
             else if (item == EditorType.CheckEdit)
            {
                return new QuickerCheckEdit();
            }
            else
            {
                return new QuickerTextEdit();
            }
        }


        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IQuickerEditor Control
        {
            get { return control; }
            set
            {
                if (!(value is Control)) value = null;
                if (control == value) return;
                if (control != null) OnControlRelease(control);
                control = value;
                if (control != null) OnControlSubscribe(control);
                OnControlChanged();
            }
        }

        public override BorderStyles BorderStyle
        {
            get
            {
                if (Control != null && !Control.AllowBorder) return BorderStyles.NoBorder;
                return base.BorderStyle;
            }
            set { base.BorderStyle = value; }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new QuickerEditor OwnerEdit
        {
            get { return base.OwnerEdit as QuickerEditor; }
        }

        internal bool AllowDisposeControl { get; set; }

        public static void Register()
        {
            EditorRegistrationInfo.Default.Editors.Add(new EditorClassInfo(EditorName, typeof(QuickerEditor),
            typeof(QuickerEditorRepositoryItem), typeof(QuickerEditorEditViewInfo),
            new QuickerEditorEditPainter(), true, null));


        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                IQuickerEditor control = Control;
                Control = null;
                if (AllowDisposeControl) Dispose(control);
            }
            base.Dispose(disposing);
        }

        public override void Assign(RepositoryItem item)
        {
            base.Assign(item);
            var source = item as QuickerEditorRepositoryItem;
            if (source == null) return;
            if (OwnerEdit != null)
            {
                Control = source.CreateControlInstance();
            }
            else
                Control = source.Control;
            //todo
        }

        //protected override bool IsNeededKeyCore(Keys keyData)
        //{
        //    return base.IsNeededKeyCore(keyData);
        //}
        protected override bool NeededKeysContains(Keys key)
        {
            if (base.NeededKeysContains(key)) return true;
            switch (key)
            {
                case Keys.F2:
                case Keys.A:
                case Keys.Add:
                case Keys.B:
                case Keys.Back:
                case Keys.C:
                case Keys.Clear:
                case Keys.D:
                case Keys.D0:
                case Keys.D1:
                case Keys.D2:
                case Keys.D3:
                case Keys.D4:
                case Keys.D5:
                case Keys.D6:
                case Keys.D7:
                case Keys.D8:
                case Keys.D9:
                case Keys.Decimal:
                case Keys.Delete:
                case Keys.Divide:
                case Keys.E:
                case Keys.End:
                case Keys.F:
                case Keys.F20:
                case Keys.G:
                case Keys.H:
                case Keys.Home:
                case Keys.I:
                case Keys.Insert:
                case Keys.J:
                case Keys.K:
                case Keys.L:
                case Keys.Left:
                case Keys.M:
                case Keys.Multiply:
                case Keys.N:
                case Keys.NumPad0:
                case Keys.NumPad1:
                case Keys.NumPad2:
                case Keys.NumPad3:
                case Keys.NumPad4:
                case Keys.NumPad5:
                case Keys.NumPad6:
                case Keys.NumPad7:
                case Keys.NumPad8:
                case Keys.NumPad9:
                case Keys.Alt:
                case Keys.RButton | Keys.ShiftKey:
                case Keys.O:
                case Keys.Oem8:
                case Keys.OemBackslash:
                case Keys.OemCloseBrackets:
                case Keys.Oemcomma:
                case Keys.OemMinus:
                case Keys.OemOpenBrackets:
                case Keys.OemPeriod:
                case Keys.OemPipe:
                case Keys.Oemplus:
                case Keys.OemQuestion:
                case Keys.OemQuotes:
                case Keys.OemSemicolon:
                case Keys.Oemtilde:
                case Keys.P:
                case Keys.Q:
                case Keys.R:
                case Keys.Right:
                case Keys.S:
                case Keys.Space:
                case Keys.Subtract:
                case Keys.T:
                case Keys.U:
                case Keys.V:
                case Keys.W:
                case Keys.X:
                case Keys.Y:
                case Keys.Z:
                    return true;
            }
            return false;
        }

        protected virtual void OnControlChanged()
        {
            OnPropertiesChanged();
        }


        protected virtual void OnControlSubscribe(IQuickerEditor control)
        {
            if (OwnerEdit != null) OwnerEdit.OnControlSubscribe(control);
        }

        public override BaseEdit CreateEditor()
        {
            var editor = base.CreateEditor() as QuickerEditor;
            if (Control != null)
            {
                editor.Properties.Control = CreateControlInstance();
                editor.Properties.AllowDisposeControl = true;
            }
            return editor;
        }

        protected virtual void OnControlRelease(IQuickerEditor control)
        {
            Dispose(drawControlInstance);
            drawControlInstance = null;
            if (OwnerEdit != null) OwnerEdit.OnControlRelease(control);
        }

        protected internal IQuickerEditor GetDrawControlInstance()
        {
            if (OwnerEdit != null) return Control;
            EnsureDrawControlInstance();
            return drawControlInstance;
        }

        internal IQuickerEditor CreateControlInstance()
        {
            //IAnyControlEdit ctrl = null;
            //if (Control != null)
            //{
            //  ctrl = Activator.CreateInstance(Control.GetType()) as IAnyControlEdit;
            //  if (ctrl is RepositoryItem1)
            //  {
            //    //(ctrl as RepositoryItem1).SetParentUC((Control as RepositoryItem1).GetParentUC());
            //    (ctrl as RepositoryItem1).CopyEvents(Control as RepositoryItem1);
            //  }
            //  return ctrl;
            //}
            //return null;

            ICloneable cloneable = Control as ICloneable;
            if (cloneable != null) return cloneable.Clone() as IQuickerEditor;
            if (Control != null) return Activator.CreateInstance(Control.GetType()) as IQuickerEditor;
            return null;

        }

        protected internal void EnsureDrawControlInstance()
        {
            if (drawControlInstance == null && Control != null)
            {
                drawControlInstance = CreateControlInstance();
                drawControlInstance.SetupAsDrawControl();
            }
        }

        private void Dispose(IQuickerEditor control)
        {
            var disposable = control as IDisposable;
            if (disposable != null) disposable.Dispose();
        }
    }
}