using DevExpress.XtraSplashScreen;
using System.Threading;

namespace Quicker.UI.DevExpress.Services
{
    public static class WaitingService
    {
        public static void ShowWaitForm(string caption)
        {
            SplashScreenManager.ShowDefaultWaitForm(AppHelper.MainForm != null ? AppHelper.MainForm : null, false, false, caption);
            Thread.Sleep(25);
        }
        public static void UpdateWaitForm(string caption)
        {
            SplashScreenManager.ShowDefaultWaitForm(caption);
            Thread.Sleep(300);
        }
        public static void UpdateWaitForm(string caption, string description)
        {
            SplashScreenManager.ShowDefaultWaitForm(caption, description);
            Thread.Sleep(300);
        }

        public static void CloseWaitForm()
        {
            var ssm = SplashScreenManager.Default;
            if (ssm != null && ssm.ActiveSplashFormTypeInfo != null && ssm.ActiveSplashFormTypeInfo.Mode == Mode.WaitForm)
                SplashScreenManager.CloseForm(false, 250, AppHelper.MainForm);
        }
    }

    //public interface IWaitingService : ISingletonDependency
    //{
    //    void BeginWaiting(object parameter);
    //    void EndWaiting();
    //}
    //class WaitingService : IWaitingService
    //{
    //    void IWaitingService.BeginWaiting(object parameter) {
    //        ShowWaitForm(DevExpress.XtraEditors.EnumDisplayTextHelper.GetDisplayText(parameter));
    //    }
    //    void IWaitingService.EndWaiting() {
    //        CloseWaitForm();
    //    }
    //    static void ShowWaitForm(string caption) 
    //    {
    //        if(SplashScreenManager.Default == null) 
    //            SplashScreenManager.ShowDefaultWaitForm(AppHelper.MainForm, false, false, false, 250, caption);
    //    }
    //    static void CloseWaitForm() {
    //        var ssm = SplashScreenManager.Default;
    //        if(ssm != null && ssm.ActiveSplashFormTypeInfo != null && ssm.ActiveSplashFormTypeInfo.Mode == Mode.WaitForm)
    //            SplashScreenManager.CloseForm(false, 750, AppHelper.MainForm);
    //    }
    //}

    //class LoadingService : IWaitingService
    //{
    //    System.Windows.Forms.UserControl owner;
    //    public LoadingService(System.Windows.Forms.UserControl owner) 
    //    {
    //        this.owner = owner;
    //    }
    //    void IWaitingService.BeginWaiting(object parameter) 
    //    {
    //        ShowWaitForm(owner, parameter.ToString());
    //    }
    //    void IWaitingService.EndWaiting() {
    //        CloseWaitForm();
    //    }
    //    static void ShowWaitForm(System.Windows.Forms.UserControl owner, string caption) {
    //        if(SplashScreenManager.Default == null)
    //            SplashScreenManager.ShowDefaultWaitForm((owner != null) ? owner.FindForm() : null, false, false, caption);
    //    }
    //    static void CloseWaitForm() {
    //        var ssm = SplashScreenManager.Default;
    //        if(ssm != null && ssm.ActiveSplashFormTypeInfo != null && ssm.ActiveSplashFormTypeInfo.Mode == Mode.WaitForm)
    //            SplashScreenManager.CloseForm(false, 250, AppHelper.MainForm);
    //    }
    //}

    //public static class WaitingServiceExtension {
    //    static WaitingServiceExtension() {
    //        SplashScreenManager.ActivateParentOnWaitFormClosing = false;
    //    }
    //    public static IDisposable Enter(this IWaitingService service, object parameter, bool effective = true) {
    //        return new WaitingBatch(effective ? service : null, parameter);
    //    }
    //    class WaitingBatch : IDisposable {
    //        IWaitingService service;
    //        public WaitingBatch(IWaitingService service, object parameter) {
    //            this.service = service;
    //            if(service != null)
    //                service.BeginWaiting(parameter);
    //        }
    //        public void Dispose() {
    //            if(service != null)
    //                service.EndWaiting();
    //        }
    //    }
    //}
}
