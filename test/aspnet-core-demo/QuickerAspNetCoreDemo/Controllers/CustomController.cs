using Microsoft.AspNetCore.Mvc;

namespace QuickerAspNetCoreDemo.Controllers
{
    [Route("api/Custom")]
    [ApiExplorerSettings(IgnoreApi = false)]
    public class CustomController : DemoControllerBase
    {
        [Route("action-one")]
        public IActionResult Action1()
        {
            return Content("42");
        }
    }
}