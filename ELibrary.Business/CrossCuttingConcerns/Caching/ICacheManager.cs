using System.Runtime.InteropServices.ComTypes;

namespace ELibrary.Business.CrossCuttingConcerns.Caching
{
    public interface ICacheManager
    {
        T Get<T>(string key);

        bool IsAdd(string key);
        void Add(string key, object data, int cacheTime);
        void Remove(string key);
        void Clear();
        void RemoveByPattern(string pattern);
    }
}
