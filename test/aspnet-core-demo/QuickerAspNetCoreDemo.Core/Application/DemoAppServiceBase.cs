using Quicker.Application.Services;

namespace QuickerAspNetCoreDemo.Core.Application
{
    public class DemoAppServiceBase : ApplicationService
    {
        public DemoAppServiceBase()
        {
            LocalizationSourceName = "QuickerAspNetCoreDemoModule";
        }
    }
}
