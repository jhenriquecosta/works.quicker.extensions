using System;
using Quicker.Application.Features;
using Quicker.Authorization;
using Quicker.UI;
using QuickerAspNetCoreDemo.Core.Application.Dtos;

namespace QuickerAspNetCoreDemo.Core.Application
{
    public class TestAppService : DemoAppServiceBase
    {
        public string Test1()
        {
            return "FortyTwo";
        }

        public ProductDto Test2()
        {
            return new ProductDto()
            {
                Id = 42,
                Name = "My product 1",
                Price = 99.9f
            };
        }

        public ProductDto Test3()
        {
            throw new Exception("This should be gone as internal server error!");
        }

        public ProductDto Test4()
        {
            throw new UserFriendlyException("Please don't call this method or you get the exception!");
        }

        [QuickerAuthorize()]
        public ProductDto Test5()
        {
            return null;
        }

        [QuickerAuthorize("MyTestPermissionName")]
        public ProductDto Test6()
        {
            return null;
        }

        [RequiresFeature("MyTestFeatureName")]
        public ProductDto Test7()
        {
            return null;
        }
    }
}