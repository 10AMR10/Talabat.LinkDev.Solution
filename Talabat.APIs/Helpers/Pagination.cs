using Talabat.APIs.Dtos;

namespace Talabat.APIs.Helpers
{
	public class Pagination<T> //order , product ect...
	{

		public Pagination(int pageSize, int pageIndex, IReadOnlyList<T> mapped,int count)
		{
			PageSize = pageSize;
			PageIndex = pageIndex;
			Data = mapped;
			Count = count;
		}

		public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public int Count { get; set; }
        public IReadOnlyList<T> Data { get; set; }
    }
}
