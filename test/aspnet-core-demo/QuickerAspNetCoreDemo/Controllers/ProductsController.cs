using QuickerAspNetCoreDemo.Core.Domain;
using Quicker.AspNetCore.OData.Controllers;
using Quicker.Dependency;
using Quicker.Domain.Repositories;

namespace QuickerAspNetCoreDemo.Controllers
{
    public class ProductsController : QuickerODataEntityController<Product>, ITransientDependency
    {
        public ProductsController(IRepository<Product> repository) : base(repository)
        {
            GetPermissionName = "GetProductPermission";
            GetAllPermissionName = "GetAllProductsPermission";
            CreatePermissionName = "CreateProductPermission";
            UpdatePermissionName = "UpdateProductPermission";
            DeletePermissionName = "DeleteProductPermission";
        }
    }
}
