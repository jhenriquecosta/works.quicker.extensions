using Quicker.Auditing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace QuickerAspNetCoreDemo.Pages
{
    [DisableAuditing]
    [IgnoreAntiforgeryToken]
    public class AuditFilterPageDemo2Model : PageModel
    {
        public void OnGet()
        {
            
        }

        public void OnPost()
        {

        }
    }
}