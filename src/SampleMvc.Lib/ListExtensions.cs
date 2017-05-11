using System;
using System.Collections.Generic;
using System.Text;

namespace SampleMvc.Lib
{
    public static class ListExtensions
    {
        public static IPagedList<T> ToPagedList<T>(this IEnumerable<T> innerList, int pageSize, int currentPage = 1) where T : class
        {
            var pagedList = new PagedList<T>(innerList, pageSize, currentPage);

            return pagedList;
        }
    }
}
