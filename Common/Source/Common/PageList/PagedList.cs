using System;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Collections.Generic;

namespace TestTask.Common
{
    public class PagedList<T> : List<T>, IPagedList<T>
    {
        public int TotalCount { get; private set; }
        public int PageCount { get; private set; }
        public int Page { get; private set; }
        public int PageSize { get; private set; }

        private readonly IQueryable<T> _source;

        public PagedList(IQueryable<T> source, int pageSize, int page)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            _source = source;

            TotalCount = source.Count();
            PageSize = pageSize;
            Page = page < 1 ? 0 : page - 1;
            PageCount = GetPageCount(TotalCount, PageSize);
        }

        public PagedList(IEnumerable<T> items, int totalCount, int pageCount, int page, int pageSize)
        {
            TotalCount = totalCount;
            PageCount = pageCount;
            Page = page;
            PageSize = pageSize;

            base.AddRange(items);
        }

        public async Task FillPagedDataAsync()
        {
            this.Clear();
            AddRange(await GetPagedQuery().ToListAsync());
        }

        public void FillPagedData()
        {
            this.Clear();
            AddRange(GetPagedQuery().ToList());
        }

        private IQueryable<T> GetPagedQuery()
        {
            return _source.Skip(PageSize * Page).Take(PageSize);
        }

        private int GetPageCount(int totalCount, int pageSize)
        {
            if (pageSize == 0)
                return 0;

            return (int)Math.Ceiling((float)totalCount / pageSize);
        }
    }
}
