using System.Linq;
using Castle.DynamicProxy;
using ELibrary.Business.CrossCuttingConcerns.Caching;
using ELibrary.Business.Utility.Interceptors;
using ELibrary.Business.Utility.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace ELibrary.Business.Aspects.Autofac.Caching
{
    public class CacheAspect:MethodInterception
    {
        private int _cacheTime;
        private ICacheManager _cacheManager;
        public CacheAspect(int cacheTime)
        {
            _cacheTime = cacheTime;
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
        }

        public override void Intercept(IInvocation invocation)
        {
            var methodName = string.Format($"{invocation.Method.ReflectedType.FullName}.{invocation.Method.Name}");
            var arguments = invocation.Arguments.ToList();
            var key=$"{methodName}({string.Join(",",arguments.Select(a=>a?.ToString()??"<Null>"))})";
            if (_cacheManager.IsAdd(key))
            {
                invocation.ReturnValue = _cacheManager.Get<ICacheManager>(key);
                return;
            }
            invocation.Proceed();
            _cacheManager.Add(key,invocation.ReturnValue,_cacheTime);
        }
    }
}
