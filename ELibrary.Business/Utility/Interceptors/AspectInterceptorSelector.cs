using System;
using System.Linq;
using System.Reflection;
using Castle.DynamicProxy;
using ELibrary.Business.Aspects.Autofac.Exception;
using ELibrary.Business.CrossCuttingConcerns.Logging.Log4Net.Loggers;

namespace ELibrary.Business.Utility.Interceptors
{
    public class AspectInterceptorSelector:IInterceptorSelector
    {
        public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
        {
            var classAttributes = type
                .GetCustomAttributes<MethodInterceptionBaseAttribute>(true).ToList();
            var methodAttributes = type.GetMethod(method.Name)
                .GetCustomAttributes<MethodInterceptionBaseAttribute>(true);
            classAttributes.AddRange(methodAttributes);
            classAttributes.Add(new ExceptionLogAspect(typeof(FileLogger)));
            return classAttributes.OrderBy(a=>a.Priority).ToArray();
        }
    }
}
