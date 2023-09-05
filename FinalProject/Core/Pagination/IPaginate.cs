using System.Collections;

namespace Core.Pagination
{
    public interface IPaginate
    {
        int Page { get; }
        int Size { get; }
        int Count { get; }
        int Pages { get; }
        bool HasPrevisous { get; }
        bool HasNext { get; }
        IEnumerable Items { get; }
    }
}
