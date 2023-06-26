using System;
using System.Collections.Generic;

namespace Waterschapshuis.CatchRegistration.Core.Pagination
{
    public class PagedResponse<T>
    {
        /// <summary>
        /// Paged items
        /// </summary>
        public IEnumerable<T> Items { get; set; } = new List<T>();

        /// <summary>
        /// Total items count
        /// </summary>
        public int ItemsTotalCount { get; set; }

        /// <summary>
        /// Current page
        /// </summary>
        public int CurrentPage { get; set; }

        /// <summary>
        /// Page size
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Total number of pages
        /// </summary>
        public int TotalPages => PageSize > 0 ? (int)Math.Ceiling(ItemsTotalCount / (double)PageSize) : 0;
        
        /// <summary>
        /// Indicator if there is next page
        /// </summary>
        public bool HasNextPage => CurrentPage < TotalPages;
    }
}
