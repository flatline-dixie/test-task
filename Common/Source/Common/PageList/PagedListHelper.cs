using System;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Collections.Generic;

namespace TestTask.Common
{
    public static class PagedListHelper
    {
        public static async Task<IPagedList<T>> ToPagedListAsync<T>(this IQueryable<T> source, int pageSize, int page)
        {
            var pagedList = new PagedList<T>(source, pageSize, page);
            await pagedList.FillPagedDataAsync();

            return pagedList;
        }

        public static IPagedList<T> ToPagedList<T>(this IQueryable<T> source, int pageSize, int page)
        {
            var pagedList = new PagedList<T>(source, pageSize, page);
            pagedList.FillPagedData();

            return pagedList;
        }

        public static IPagedList<TResult> SelectFromPageList<T, TResult>(this IPagedList<T> pagedList, Func<T, TResult> select)
        {
            var items = Enumerable.Select(pagedList, select);

            return new PagedList<TResult>(items, pagedList.TotalCount, pagedList.PageCount, pagedList.Page + 1,
                pagedList.PageSize);
        }

        public static async Task<IList<T>> ToListAsync<T>(this IQueryable<T> source)
        {
            return await QueryableExtensions.ToListAsync(source);
        }
    }
}
