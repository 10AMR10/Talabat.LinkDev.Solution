using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.specification.Products_Specific
{
	public class ProductSpecPrams
	{
		private const int MaxPageSize = 10;
		public string? Sort  { get; set; }
        public int? BrandId  { get; set; }
        public int? CategoryId  { get; set; }
		private int pageSize=5;

		public int PageSize
		{
			get { return pageSize; }
			set { pageSize = value> MaxPageSize ? MaxPageSize : value; } // must set full property => user can send 1000000
		}

		public int PageIndex { get; set; } = 1; // must set defult value 
        public string?  Search { get; set; }

    }
}
