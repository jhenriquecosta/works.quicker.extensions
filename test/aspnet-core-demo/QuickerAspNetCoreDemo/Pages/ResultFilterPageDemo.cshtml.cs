using Quicker.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;

namespace QuickerAspNetCoreDemo.Pages
{
    [IgnoreAntiforgeryToken]
    public class ResultFilterPageDemoModel : QuickerPageModel
    {
        public IActionResult OnGet()
        {
            return Content("OnGet");
        }

        public JsonResult OnPost()
        {
            return new JsonResult("OnPost");
        }
    }
}