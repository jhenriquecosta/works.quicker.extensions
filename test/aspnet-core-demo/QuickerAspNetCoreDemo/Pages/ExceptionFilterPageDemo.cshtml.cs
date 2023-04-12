using Quicker.AspNetCore.Mvc.RazorPages;
using Quicker.UI;
using Microsoft.AspNetCore.Mvc;

namespace QuickerAspNetCoreDemo.Pages
{
    [IgnoreAntiforgeryToken]
    public class ExceptionFilterPageDemoModel : QuickerPageModel
    {
        public JsonResult OnGet()
        {
            throw new UserFriendlyException("OnGet");
        }

        public IActionResult OnPost()
        {
            throw new UserFriendlyException("OnPost");
        }
    }
}