using Quicker.AspNetCore.OData.Controllers;
using Quicker.Dependency;
using Quicker.Domain.Repositories;
using Quicker.ObjectMapping;
using QuickerAspNetCoreDemo.Core.Application.Dtos;
using QuickerAspNetCoreDemo.Core.Domain;

namespace QuickerAspNetCoreDemo.Controllers
{
    public class ProductsDtoController : QuickerODataDtoController<Product, ProductDto, ProductCreateInput>, ITransientDependency
    {
        public ProductsDtoController(IRepository<Product> repository, IObjectMapper objectMapper) : base(repository, objectMapper)
        {
            GetPermissionName = "GetProductPermission";
            GetAllPermissionName = "GetAllProductsPermission";
            CreatePermissionName = "CreateProductPermission";
            UpdatePermissionName = "UpdateProductPermission";
            DeletePermissionName = "DeleteProductPermission";
        }
    }
}