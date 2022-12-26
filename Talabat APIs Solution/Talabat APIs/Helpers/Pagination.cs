using System.Collections.Generic;

namespace Talabat_APIs.Helpers
{
    public class Pagination<T>
    {
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public int Count { get; set; }
        public IEnumerable<T> Data { get; set; }
        public Pagination(IEnumerable<T> data, int pageSize, int pageIndex, int count)
        {
            Data = data;
            PageSize = pageSize;
            PageIndex = pageIndex;
            Count = count;
        }
    }
}
