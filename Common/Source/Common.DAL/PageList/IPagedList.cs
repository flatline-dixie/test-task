using System.Collections.Generic;

namespace TestTask.Common.DAL
{
    public interface IPagedList<out T> : IEnumerable<T>
    {
        int TotalCount { get; }
        int PageCount { get; }
        int Page { get; }
        int PageSize { get; }
    }
}
