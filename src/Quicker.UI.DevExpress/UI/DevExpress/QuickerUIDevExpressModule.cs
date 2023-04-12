using System.Reflection;
using Quicker.Modules;
using Quicker.UI.Common;

namespace Quicker.UI.DevExpress
{

    [DependsOn(typeof(QuickerUICommonModule))]
    public class QuickerUIDevExpressModule : QuickerModule
    {

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
        public override void PreInitialize()
        {
            base.PreInitialize();
             
        }


    }
}
