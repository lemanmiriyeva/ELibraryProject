using System;
using System.Linq;
using System.Reflection;
using ELibrary.Business.CrossCuttingConcerns.Caching;
using PostSharp.Aspects;

namespace ELibrary.Business.Aspects.PostSharp.CacheAspects
{
    [Serializable]
    public class CacheAspect:MethodInterceptionAspect
    {
        private Type _cacheType;
        private int _cacheByMinute;
        private ICacheManager _cacheManager;

        public CacheAspect(Type cacheType, int cacheByMinute=180)
        {
            _cacheType = cacheType;
            _cacheByMinute = cacheByMinute;
        }
        public override void RuntimeInitialize(MethodBase method)
        {
            if (!typeof(ICacheManager).IsAssignableFrom(_cacheType))
            {
                throw new Exception("Cache type is wrong!");
            }

            _cacheManager = (ICacheManager) Activator.CreateInstance(_cacheType);
            base.RuntimeInitialize(method);
        }

        public override void OnInvoke(MethodInterceptionArgs args)
        {
            var methodName = string.Format("{0}.{1}.{2}", args.Method.ReflectedType.Namespace,
                args.Method.ReflectedType.Name, args.Method.Name);
            var arguments = args.Arguments.ToList();
            var key = string.Format("{0}({1})",methodName,string.Join(",",arguments.Select(a=>a!=null?a.ToString():"<Null>")));
            if (_cacheManager.IsAdd(key))
            {
                args.ReturnValue = _cacheManager.Get<object>(key);
            }
            
            base.OnInvoke(args);
            _cacheManager.Add(key,args.ReturnValue,_cacheByMinute);
        }
    }
}
