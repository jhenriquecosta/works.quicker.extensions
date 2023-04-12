using DevExpress.Utils.Svg;
using System;
using System.Drawing;
using System.Windows.Forms;
using Quicker.Reflection.Extensions;
using DevExpress.XtraEditors;
using DevExpressData = DevExpress.Data.Utils;
using DevExpressUtils = DevExpress.Utils;
using Quicker.Extensions;

namespace Quicker.UI.DevExpress
{
    public static class AppHelper
    {
        public static void ProcessStart(string name)
        {
            ProcessStart(name, string.Empty);
        }
        public static void ProcessStart(string name, string arguments)
        {
            try
            {
                DevExpressData.SafeProcess.Open(name, arguments);
            }
            catch (System.ComponentModel.Win32Exception) { }
        }
        public static string ApplicationID
        {
            get { return string.Format("Components_{0}_Demo_Center_{0}", AssemblyInfo.VersionShort.Replace(".", "_")); }
        }
        public static SvgImage AppIcon
        {
            get { return DevExpressUtils.ResourceImageHelper.CreateSvgImageFromResources("DevExpress.DevAV.Resources.AppIcon.svg", CurrentControl.GetAssembly()); }
        }
        static Image img;
        public static Image AppImage
        {
            get
            {
                if (img == null)
                    img = AppIcon.Render(null);
                return img;
            }
        }

        static WeakReference wRefCurrentControl;
        static WeakReference wRef;

        public static Form MainForm
        {
            get { return wRef != null ? wRef.Target as Form : null; }
            set { wRef = new WeakReference(value); }
        }

        public static Control CurrentControl
        {
            get { return wRefCurrentControl != null ? wRefCurrentControl.Target as Control : null; }
            set { wRefCurrentControl = new WeakReference(value); }
        }
        public static Form CurrentForm
        {
            get { return CurrentControl.FindForm(); }
        }
        
        public static float GetDefaultSize()
        {
            return 8.25F;
        }
    }
}
