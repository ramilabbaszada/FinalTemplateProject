
namespace Core.CrossCuttingConcerns.Cashing
{
    public interface ICacheManager
    {
        T Get<T>(string key);
        object Get(string key);
        public void Add(string key, object value, int duration);
        public bool IsAdd(string key);
        public void Remove(string key);
        public void RemoveByPattern(string pattern);
    }
}
