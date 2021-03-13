using System;
using System.Reflection;
using ELibrary.Business.CrossCuttingConcerns.Caching;
using PostSharp.Aspects;

namespace ELibrary.Business.Aspects.PostSharp.CacheAspects
{
    public class CacheRemoveAspect:OnMethodBoundaryAspect
    {
        private string _pattern;
        private Type _cacheType;
        private ICacheManager _cacheManager;

        public CacheRemoveAspect(string pattern, Type cacheType):this(cacheType)
        {
            _pattern = pattern;
        }

        public CacheRemoveAspect(Type cacheType)
        {
            _cacheType = cacheType;
        }

        public override void RuntimeInitialize(MethodBase method)
        {
            if (!typeof(ICacheManager).IsAssignableFrom(_cacheType))
            {
                throw new Exception("Cache type is wrong!");
            }

            _cacheManager = (ICacheManager)Activator.CreateInstance(_cacheType);

            base.RuntimeInitialize(method);
        }

        public override void OnSuccess(MethodExecutionArgs args)
        {
           _cacheManager.RemoveByPattern(
              string.IsNullOrEmpty(_pattern)?(string.Format("{0}.{1}.*",args.Method.ReflectedType.Namespace,
                   args.Method.ReflectedType.Name)):_pattern);
        }
    }
}
