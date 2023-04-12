using System.Linq;
using Quicker.Dependency;
using Quicker.Runtime.Caching;
using Quicker.Runtime.Caching.Memory;
using AutoMapper.Internal;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Shouldly;
using Xunit;

namespace QuickerAspNetCoreDemo.IntegrationTests.Tests
{
    public class MemoryCacheOptions_Test: IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _applicationFactory;
        
        public MemoryCacheOptions_Test()
        {
            _applicationFactory = new WebApplicationFactory<Startup>();
            _applicationFactory.CreateClient();
        }
        
        [Fact]
        public void MemoryCacheOption_Size_Test()
        {
            var memoryCacheManager = _applicationFactory.Services.GetService(typeof(ICacheManager)) as ICacheManager;
            
            memoryCacheManager.ShouldNotBeNull();
            memoryCacheManager.GetType().ShouldBe(typeof(QuickerMemoryCacheManager));
            
            var memoryCache= memoryCacheManager.GetCache("Test");
            
            memoryCache.ShouldNotBeNull();
            memoryCache.GetType().ShouldBe(typeof(QuickerMemoryCache));
            
            var memberPath =  ReflectionHelper.GetMemberPath(typeof(QuickerMemoryCache), "_memoryCacheOptions").First();
            var memoryCacheOptions = memberPath.GetMemberValue(memoryCache) as IOptions<MemoryCacheOptions>;
            memoryCacheOptions.ShouldNotBeNull();
            memoryCacheOptions.Value.SizeLimit.ShouldBe(2048);
        }
    }
}