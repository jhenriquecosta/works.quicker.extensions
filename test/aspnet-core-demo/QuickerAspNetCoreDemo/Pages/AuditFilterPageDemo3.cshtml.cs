using Quicker.Auditing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace QuickerAspNetCoreDemo.Pages
{
    [IgnoreAntiforgeryToken]
    public class AuditFilterPageDemo3Model : PageModel
    {
        [DisableAuditing]
        public void OnGet()
        {

        }

        [DisableAuditing]
        public void OnPost()
        {

        }
    }
}