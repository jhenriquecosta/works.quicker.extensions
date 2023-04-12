using Castle.Core.Logging;
using Quicker.Application.Services;
using Quicker.Application.Services.Dto;
using Quicker.Dependency;
using Quicker.Domain.Entities;
using Quicker.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Quicker.UI.DevExpress.Components.Forms.Contracts
{
    public interface IQuickerFormCrudService : IQuickerFormCrudService<IEntity,IEntityDto>
    { }
    public interface IQuickerFormCrudService<TEntity,TEntityDto> : ITransientDependency
        where TEntity : class, IEntity
        where TEntityDto : class, IEntityDto
    {
       
        ILogger Logger { get; set; } 
        TEntity Model { get; set; }
        TEntityDto ViewModel { get; set; }
        Task<TEntityDto> SaveOrUpdateAsync(TEntityDto entityDto);
        Task DeleteAsync(TEntityDto entityDto);
        Task<TEntityDto> GetAsync(TEntityDto entityDto);
        List<TEntityDto> GetAll(Expression<Func<TEntity, bool>> filter = null);
    }
    
}
