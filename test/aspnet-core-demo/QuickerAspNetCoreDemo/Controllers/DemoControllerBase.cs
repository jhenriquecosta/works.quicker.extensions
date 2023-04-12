using Quicker.AspNetCore.Mvc.Controllers;

namespace QuickerAspNetCoreDemo.Controllers
{
    public class DemoControllerBase : QuickerController
    {
        public DemoControllerBase()
        {
            LocalizationSourceName = "QuickerAspNetCoreDemoModule";
        }
    }
}
