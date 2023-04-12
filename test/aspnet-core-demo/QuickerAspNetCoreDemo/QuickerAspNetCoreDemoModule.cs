using System;
using System.Threading;
using Quicker.AspNetCore;
using Quicker.AspNetCore.Configuration;
using Quicker.AspNetCore.OData;
using Quicker.AspNetCore.OData.ResultWrapping;
using Quicker.Castle.Logging.Log4Net;
using Quicker.Configuration.Startup;
using Quicker.Dependency;
using Quicker.EntityFrameworkCore;
using Quicker.Modules;
using Quicker.Reflection.Extensions;
using QuickerAspNetCoreDemo.Core;
using QuickerAspNetCoreDemo.Db;
using Castle.MicroKernel.Registration;
using Microsoft.AspNetCore.Builder;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Caching.Memory;

namespace QuickerAspNetCoreDemo
{
    [DependsOn(
        typeof(QuickerAspNetCoreModule),
        typeof(QuickerAspNetCoreDemoCoreModule),
        typeof(QuickerEntityFrameworkCoreModule),
        typeof(QuickerCastleLog4NetModule),
        typeof(QuickerAspNetCoreODataModule)
        )]
    public class QuickerAspNetCoreDemoModule : QuickerModule
    {
        public static AsyncLocal<Action<IQuickerStartupConfiguration>> ConfigurationAction =
            new AsyncLocal<Action<IQuickerStartupConfiguration>>();

        public override void PreInitialize()
        {
            RegisterDbContextToSqliteInMemoryDb(IocManager);

            Configuration.Modules.QuickerAspNetCore()
                .CreateControllersForAppServices(
                    typeof(QuickerAspNetCoreDemoCoreModule).GetAssembly()
                );

            Configuration.Modules.QuickerWebCommon().WrapResultFilters.Add(new QuickerODataDontWrapResultFilter());

            Configuration.IocManager.Resolve<IQuickerAspNetCoreConfiguration>().EndpointConfiguration.Add(endpoints =>
            {
                endpoints.MapControllerRoute("defaultWithArea", "{area}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });

            Configuration.Caching.MemoryCacheOptions = new MemoryCacheOptions
            {
                SizeLimit = 2048
            };
            
            ConfigurationAction.Value?.Invoke(Configuration);
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(QuickerAspNetCoreDemoModule).GetAssembly());
        }

        private static void RegisterDbContextToSqliteInMemoryDb(IIocManager iocManager)
        {
            var builder = new DbContextOptionsBuilder<MyDbContext>();

            var inMemorySqlite = new SqliteConnection("Data Source=:memory:");
            builder.UseSqlite(inMemorySqlite);

            iocManager.IocContainer.Register(
                Component
                    .For<DbContextOptions<MyDbContext>>()
                    .Instance(builder.Options)
                    .LifestyleSingleton()
            );

            inMemorySqlite.Open();
            var ctx = new MyDbContext(builder.Options);
            ctx.Database.EnsureCreated();
        }
    }
}