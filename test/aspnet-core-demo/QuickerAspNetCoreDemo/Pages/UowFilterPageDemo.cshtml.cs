using Quicker.AspNetCore.Mvc.RazorPages;
using Quicker.Domain.Uow;
using Quicker.UI;
using Microsoft.AspNetCore.Mvc;

namespace QuickerAspNetCoreDemo.Pages
{
    [IgnoreAntiforgeryToken]
    public class UowFilterPageDemo : QuickerPageModel
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public UowFilterPageDemo(IUnitOfWorkManager unitOfWorkManager)
        {
            _unitOfWorkManager = unitOfWorkManager;
        }

        public void OnGet()
        {
            if (_unitOfWorkManager.Current == null)
            {
                throw new UserFriendlyException("Current UnitOfWork is null");
            }
        }

        [UnitOfWork(IsDisabled = true)]
        public void OnPost()
        {
            if (_unitOfWorkManager.Current == null)
            {
                throw new UserFriendlyException("Current UnitOfWork is null");
            }
        }
    }
}