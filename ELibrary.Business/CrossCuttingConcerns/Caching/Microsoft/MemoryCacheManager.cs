using System;
using System.Linq;
using System.Runtime.Caching;
using System.Text.RegularExpressions;

namespace ELibrary.Business.CrossCuttingConcerns.Caching.Microsoft
{
    public class MemoryCacheManager:ICacheManager
    {
        protected ObjectCache Cache => MemoryCache.Default;

        public T Get<T>(string key)
        {
            return (T) Cache["key"];
        }

        public bool IsAdd(string key)
        {
            return Cache.Contains(key);
        }

        public void Add(string key, object data, int cacheTime)
        { 
            if (data==null)
            {
                return;
            }
            var policy=new CacheItemPolicy
            {
                AbsoluteExpiration = DateTime.Now+TimeSpan.FromMinutes(cacheTime)
            };
            Cache.Add(new CacheItem(key, data), policy);
        }

        public void Remove(string key)
        {
            Cache.Remove(key);
        }

        public void Clear()
        {
            foreach (var item in Cache)
            {
                Cache.Remove(item.Key);
            }
        }

        public void RemoveByPattern(string pattern)
        {
           var regex=new Regex(pattern,RegexOptions.Singleline| RegexOptions.Compiled|RegexOptions.IgnoreCase);
           var keysToRemove = Cache.
               Where(c => regex.IsMatch(c.Key)).
               Select(c=>c.Key).ToList();

           foreach (var item in keysToRemove)
           {
               Remove(item);
           }
        }
    }
}
