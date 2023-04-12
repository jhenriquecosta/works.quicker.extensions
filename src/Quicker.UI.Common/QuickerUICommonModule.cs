using Quicker.Modules;
using System.Reflection;

namespace Quicker.UI.Common
{


    public class QuickerUICommonModule : QuickerModule
    {

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
        public override void PreInitialize()
        {
            base.PreInitialize();

            //Adding custom AutoMapper configuration



        }


    }
}
