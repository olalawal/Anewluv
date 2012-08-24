using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Omu.Awesome.Core;

namespace Shell.MVC2.Infrastructure
{
    public class PaginatedList<T> : List<T>
    {

        public IPageable<T> GetPageable(IEnumerable<T> source, int page, int pageSize)
        {
            var Count = source.Count();
            return new Pageable<T>
            {
                Page = source.Skip((page - 1) * pageSize).Take(pageSize),
                PageCount = GetPageCount(pageSize, Count),
                PageIndex = page

            };
        }



        public IPageable<T> GetPageableList(List<T> source, int page, int pageSize)
        {
            if (source == null) return null;
            var Count = source.Count();
            return new Pageable<T>
            {
                Page = source.Skip((page - 1) * pageSize).Take(pageSize),
                PageCount = GetPageCount(pageSize, Count),
                PageIndex = page
            };
        }
        static int GetPageCount(int pageSize, int count)
        {
            try
            {
                var pages = count / pageSize;
                if (count % pageSize > 0) pages++;
                return pages;

            }
            catch
            { }
            return 0;

        }

        //testing overload with pageindex
        public IPageable<T> GetPageableList(List<T> source, int page, int pageSize, int pageIndex)
        {
            var Count = source.Count();
            return new Pageable<T>
            {
                Page = source.Skip((page - 1) * pageSize).Take(pageSize),
                PageCount = GetPageCount(pageSize, Count),
                PageIndex = pageIndex
            };
        }

        public class PagedList<T>
        {
            public bool HasNext { get; set; }
            public bool HasPrevious { get; set; }
            public List<T> Entities { get; set; }
        }

    }
}