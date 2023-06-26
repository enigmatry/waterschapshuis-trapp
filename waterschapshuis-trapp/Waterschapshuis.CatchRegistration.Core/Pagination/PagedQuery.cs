using System;

namespace Waterschapshuis.CatchRegistration.Core.Pagination
{
    public class PagedQuery
    {
        private const int DefaultCurrentPage = 1;
        private const int DefaultPageSize = 5;

        public string SortField { get; set; } = String.Empty;
        public string SortDirection { get; set; } = String.Empty;
        public int PageSize { get; set; } = DefaultPageSize;
        public int CurrentPage { get; set; } = DefaultCurrentPage;
    }
}
