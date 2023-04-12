using Quicker.AspNetCore.Mvc.Views;
using Quicker.Runtime.Session;
using Microsoft.AspNetCore.Mvc.Razor.Internal;

namespace QuickerAspNetCoreDemo.PlugIn.Views;

public abstract class PluginRazorPageBase<TModel> : QuickerRazorPage<TModel>
{
    [RazorInject] public IQuickerSession QuickerSession { get; set; }
}
