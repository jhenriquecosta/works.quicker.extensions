using DevExpress.XtraGrid;
using Quicker.Application.Services.Dto;
using Quicker.Dependency;
using Quicker.Domain.Entities;
using Quicker.Domain.Repositories;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quicker.UI.DevExpress.Components.Forms.Contracts
{
    public interface IQuickerFormCrud: ITransientDependency
    {
       bool IsNewRecord {get;set;} 
       object ViewModel {get;set; }
       object DataSource {get;set; } 
       GridControl GetGridView(); 
       IIocManager Ioc {get; }
        TReturn UsingDb<T,TReturn>(Func<IRepository<T>, TReturn> func) where T : class, IEntity;
       
       void InitializeServices<TEntity,TEntityDto>() 
            where TEntity : class, IEntity
            where TEntityDto : class, IEntityDto;
    
    }
}
