using Quicker.AspNetCore.Mvc.RazorPages;
using Quicker.Domain.Uow;
using Quicker.UI;
using Microsoft.AspNetCore.Mvc;

namespace QuickerAspNetCoreDemo.Pages
{
    [UnitOfWork(IsDisabled = true)]
    [IgnoreAntiforgeryToken]
    public class UowFilterPageDemo2 : QuickerPageModel
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public UowFilterPageDemo2(IUnitOfWorkManager unitOfWorkManager)
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

        public void OnPost()
        {
            if (_unitOfWorkManager.Current == null)
            {
                throw new UserFriendlyException("Current UnitOfWork is null");
            }
        }
    }
}