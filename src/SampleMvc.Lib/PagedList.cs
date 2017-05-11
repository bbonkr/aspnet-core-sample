using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SampleMvc.Lib
{
    public class PagedList<T> : IPagedList<T> where T : class
    {
        public PagedList(IEnumerable<T> innerList, int pageSize, int currentPage)
        {
            _pageSize = pageSize;

            CurrentPage = currentPage;

            TotalCount = innerList?.Count() ?? 0;
            TotalPages = (int)(TotalCount / PageSize) + (TotalCount % PageSize > 0 ? 1 : 0);

            _innerList = innerList?.Skip((currentPage - 1) * pageSize).Take(PageSize);
        }

        public PagedList(IEnumerable<T> innerList, int pageSize) : this(innerList, pageSize, 1)
        {

        }

        public PagedList(IEnumerable<T> innerList) : this(innerList, 10)
        {

        }

        /// <summary>
        /// 항목의 수
        /// </summary>
        public int TotalCount { get; private set; }

        /// <summary>
        /// 전체 페이지
        /// </summary>
        public int TotalPages { get; private set; }

        /// <summary>
        /// 현재 페이지
        /// </summary>
        public int CurrentPage { get; set; } = 1;

        /// <summary>
        /// 한 페이지에 표시할 항목의 수
        /// </summary>
        public int PageSize
        {
            get { return _pageSize; }
            set
            {
                if (value > 0)
                {
                    _pageSize = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("PageSize", "Page size has to be bigger than 0.");
                }
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (var item in _innerList)
            {
                yield return item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private IEnumerable<T> _innerList = null;
        private int _pageSize = 1;
    }
}
