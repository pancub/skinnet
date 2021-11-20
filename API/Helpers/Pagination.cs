using System.Collections.Generic;

namespace API.Helpers
{
    public class Pagination<T> where T : class
    {

        public Pagination(int pageIndex, int pageSize, int totalItems,IReadOnlyList<T> data)
        {
            this.PageIndex = pageIndex;
            this.PageSize = pageSize;
            this.TotalItems = totalItems;
            this.Data = data;

        }
        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public int TotalItems { get; set; }

        public IReadOnlyList<T> Data { get; set; }
    }
}