using System.Collections.Generic;

namespace TestTask.Common
{
    public interface IPagedList<out T> : IEnumerable<T>
    {
        int TotalCount { get; }
        int PageCount { get; }
        int Page { get; }
        int PageSize { get; }
    }
}
