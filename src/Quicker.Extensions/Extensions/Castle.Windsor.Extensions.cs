

using Castle.Windsor.MsDependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Quicker.Dependency;
using System;

namespace Quicker.Extensions
{
    public static class CastleWindsorExtensions
    {
        public static IServiceCollection _services;
        private static IServiceCollection GetServiceCollection()
        {
            if (_services == null) _services = new ServiceCollection();
            return _services;
        }
        
        public static IServiceCollection GetServiceCollection(this IIocManager iocManager)
        {
            return GetServiceCollection();
        }
        public static IServiceCollection GetServices(this IIocManager iocManager)
        {
            return GetServiceCollection();
        }
        public static IServiceCollection AddServices(this IIocManager iocManager, Action<IServiceCollection> action)
        {
            _services = GetServiceCollection();
            action(_services);
            return _services;
        }
        public static IServiceCollection AddServices(this IIocManager iocManager,IServiceCollection services)
        {
            _services = services;
            return _services;
        }
        public static void CreateServiceProvider(this IIocManager iocManager)
        {
            WindsorRegistrationHelper.CreateServiceProvider(iocManager.IocContainer, _services);
        }


    }
}
