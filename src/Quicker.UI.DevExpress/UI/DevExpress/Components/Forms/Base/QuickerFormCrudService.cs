using Castle.Core.Logging;
using DevExpress.Utils.Drawing.Helpers;
using DevExpress.XtraEditors;
using Quicker.Application.Services;
using Quicker.Application.Services.Dto;
using Quicker.Dependency;
using Quicker.Domain.Entities;
using Quicker.Domain.Repositories;
using Quicker.Domain.Uow;
using Quicker.Extensions;
using Quicker.Helpers;
using Quicker.Reflection;
using Quicker.UI.DevExpress.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Quicker.UI.DevExpress.Components.Forms.Contracts
{
    public class QuickerFormCrudService
    {
        
        public static IQuickerFormCrudService<TEntity, TEntityDto> Instance<TEntity, TEntityDto>()
             where TEntity : class, IEntity
             where TEntityDto : class, IEntityDto
        {
            return SingletonInstance<QuickerFormCrudService<TEntity, TEntityDto>>.Instance;
        }
    }
    
    public class QuickerFormCrudService<TEntity, TEntityDto> : IQuickerFormCrudService<TEntity, TEntityDto>
        where TEntity : class, IEntity
        where TEntityDto : class, IEntityDto
    {
        public QuickerFormCrudService()
        {
             
            Logger = NullLogger.Instance;
        }
        public IRepository<TEntity> Repository { get; set; }
        public TEntity Model { get; set; }
        public TEntityDto ViewModel { get; set; }
        public ILogger Logger { get; set; } 

        #region CRUD
        public async Task<TEntityDto> GetAsync(TEntityDto entityDto)
        {
            try
            {
                Logger.Debug($"{nameof(Repository)}-Selecionando {entityDto.Id}");
                Model = await Repository.GetAsync(entityDto.Id);

            }
            catch (Exception ex)
            {
                var msg = $"Erro ao LOCALIZAR O Registro {entityDto.Id}{Environment.NewLine}{ex.Message}";
                QuickerDevExpressHelper.Error(msg);
                Logger.Debug(msg);
            }

            ViewModel = Model.MapTo<TEntityDto>();
            return ViewModel;

        }
        public List<TEntityDto> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            List<TEntityDto> resultListDto = new();
            try
            {
                Logger.Debug($"{nameof(Repository)}-Selecionando todos os registros");
                //var resultList = filter == null ? await Repository.GetAllListAsync() : await Repository.GetAllListAsync(filter);
                //if (resultList.Any()) resultListDto = resultList.AsQueryable().MapTo<TEntityDto>().ToList();

                 var result = UsingDb<TEntity,List<TEntityDto>>( r =>
                 { 
                        var resultList = filter == null ?  r.GetAllList() :  r.GetAllList(filter);
                        if (resultList.Any()) resultListDto = resultList.AsQueryable().MapTo<TEntityDto>().ToList();
                        return resultListDto;
                 });
                

            }
            catch (Exception ex)
            {
                var msg = $"ERRO-{nameof(Repository)}-Selecionando todos os registros{Environment.NewLine}{ex.Message}";
                QuickerDevExpressHelper.Error(msg);
                Logger.Debug(msg);
            }
            return resultListDto;

        }
        public async Task<TEntityDto> SaveOrUpdateAsync(TEntityDto entityDto)
        {
            Model = entityDto.MapTo<TEntity>();
            try
            {
                Logger.Debug($"{nameof(Repository)}-Salvando {entityDto.Id}-{entityDto}");
                Model = Model.Id == 0 ? await Repository.InsertAsync(Model) : await Repository.UpdateAsync(Model);
            }
            catch (Exception ex)
            {
                var msg = $"Erro ao SALVAR O Registro {entityDto.Id}{Environment.NewLine}{ex.Message}";
                QuickerDevExpressHelper.Error(msg);
                Logger.Debug(msg);
            }
            ViewModel = Model.MapTo<TEntityDto>();
            return ViewModel;
        }
        public async Task DeleteAsync(TEntityDto entityDto)
        {
            Model = entityDto.MapTo<TEntity>();
            try
            {
                Logger.Debug($"{nameof(Repository)}-Deletando {entityDto.Id}");
                await Repository.DeleteAsync(Model);
            }
            catch (Exception ex)
            {
                var msg = $"Erro ao deletar o registro {entityDto.Id}{Environment.NewLine}{ex.Message}";
                QuickerDevExpressHelper.Error(msg);
                Logger.Debug(msg);
            }
        }
        #endregion

        public virtual TReturn UsingDb<T,TReturn>(Func<IRepository<T>, TReturn> func) where T : class, IEntity
        {
            TReturn result;

           var repository = IocManager.Instance.Resolve<IRepository<T>>();
            
            using (var uow = IocManager.Instance.Resolve<IUnitOfWorkManager>().Begin())
            {
                result = func(repository);
                uow.Complete();
            }

            return result;
        }

    }

}
