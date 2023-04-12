using Quicker.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace QuickerAspNetCoreDemo.PlugIn.Controllers
{
    public class BlogController : QuickerController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
