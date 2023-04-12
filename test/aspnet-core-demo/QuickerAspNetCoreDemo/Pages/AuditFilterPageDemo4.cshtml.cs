using System.Threading.Tasks;
using Quicker.AspNetCore.Mvc.RazorPages;
using Quicker.Auditing;
using Microsoft.AspNetCore.Mvc;

namespace QuickerAspNetCoreDemo.Pages
{
    [DisableAuditing]
    [IgnoreAntiforgeryToken]
    public class AuditFilterPageDemo4Model : QuickerPageModel
    {
        public IActionResult OnGet()
        {
            return Page();
        }

        public void OnPost()
        {
        }
    }
}