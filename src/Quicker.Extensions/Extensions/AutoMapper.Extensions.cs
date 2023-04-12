using Quicker.Dependency;
using Quicker.ObjectMapping;
using System.Linq;

namespace Quicker.Extensions
{
    public static partial class AutoMapperExtensions
    {
        private static readonly IObjectMapper internalMapper = IocManager.Instance.Resolve<IObjectMapper>();

        /// <summary>
        /// Mapeia uma consulta 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static IQueryable<T> MapTo<T>(this IQueryable obj)
        {
            if (obj == null) return null;
            var result = internalMapper.ProjectTo<T>(obj);
            return result;
        }

        /// <summary>
        /// Mapeia um objeto para outro
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T MapTo<T>(this object obj)
        {
            if (obj == null) return default;
            var result = internalMapper.Map<T>(obj);
            return result;
        }
        /// <summary>
        /// Mapeia um objeto para outro
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="obj"></param>
        /// <param name="typeDestination"></param>
        /// <returns></returns>
        public static TDestination MapTo<TSource, TDestination>(this TSource obj, TDestination typeDestination)
        {
            if (obj == null) return default;
            var result = internalMapper.Map<TSource, TDestination>(obj, typeDestination);
            return result;
        }
        
        //public static LookupValueDto MapToLookupValue(this object obj)
        //{
        //    if (obj == null) return default;
        //    var result = internalMapper.Map<LookupValueDto>(obj);
        //    return result;
        //}

           //public static IEnumerable<T> MapTo<T>(this IEnumerable<T> obj)
        //{
        //    if (obj == null) return null;
        //    var result = internalMapper.ProjectTo<T>(obj.AsQueryable());
        //    return result.ToList();
        //}
        //public static TDestination MapWithCacheTo<TDestination>(this object obj)
        //{
        //    var id = QuickerIDGenerator.Instance.Generate;
        //    internalCache.GetCache(QuickerConsts.Cache.EasyCachingContext).Set(id,obj);
        //    var result = internalMapper.Map<TDestination>(obj);
        //    if (result.GetType().IsEntityWithCache())
        //    {
        //        (result as IEntityDtoCache).SetObjectId(id);
        //    }
        //    return result;
        //}


    }
}
