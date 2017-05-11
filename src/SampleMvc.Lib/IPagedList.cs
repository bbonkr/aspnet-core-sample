using System;
using System.Collections.Generic;

namespace SampleMvc.Lib
{
    public interface IPagedList
    {
        /// <summary>
        /// 항목의 수
        /// </summary>
        int TotalCount { get; }

        /// <summary>
        /// 전체 페이지
        /// </summary>
        int TotalPages { get; }

        /// <summary>
        /// 현재 페이지
        /// </summary>
        int CurrentPage { get; set; }

        /// <summary>
        /// 한 페이지에 표시할 항목의 수
        /// </summary>
        int PageSize { get; set; }
    }

    public interface IPagedList<out T> : IPagedList, IEnumerable<T> where T : class
    {

    }
}
