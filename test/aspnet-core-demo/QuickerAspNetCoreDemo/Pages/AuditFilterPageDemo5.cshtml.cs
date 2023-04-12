using Quicker.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;

namespace QuickerAspNetCoreDemo.Pages
{
    [IgnoreAntiforgeryToken]
    public class AuditFilterPageDemo5Model : QuickerPageModel
    {
        public JsonResult OnPostJson()
        {
            return new JsonResult(new {StrValue = "Forty Two", IntValue = 42});
        }

        public ObjectResult OnPostObject()
        {
            return new ObjectResult(new { StrValue = "Forty Two", IntValue = 42 });
        }

        public IActionResult OnPostString()
        {
            return Content("test");
        }
    }
}