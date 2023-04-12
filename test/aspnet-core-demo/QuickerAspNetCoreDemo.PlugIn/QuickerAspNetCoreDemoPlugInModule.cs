using System.Reflection;
using Quicker.AspNetCore;
using Quicker.Modules;
using Quicker.Reflection.Extensions;
using Quicker.Resources.Embedded;

namespace QuickerAspNetCoreDemo.PlugIn
{
    [DependsOn(typeof(QuickerAspNetCoreModule))]
    public class QuickerAspNetCoreDemoPlugInModule : QuickerModule
    {
        public override void PreInitialize()
        {

            Configuration.EmbeddedResources.Sources.Add(
                new EmbeddedResourceSet(
                    "/Views/",
                    typeof(QuickerAspNetCoreDemoPlugInModule).GetAssembly(),
                    "QuickerAspNetCoreDemo.PlugIn.Views"
                )
            );
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(QuickerAspNetCoreDemoPlugInModule).GetAssembly());
        }
    }
}
