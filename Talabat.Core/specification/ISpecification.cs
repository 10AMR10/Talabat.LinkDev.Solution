using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.specification
{
	public interface ISpecification<T> where T : BaseEntity
	{
		//Signeture property
        public Expression<Func<T,bool>>? Criteria { get; set; }
        public List<Expression<Func<T,object>>> Includes { get; set; } // out of func be BaseEntity if return only brand and catergory but if i return either and collection i will use object the parent of all 
        public Expression<Func<T,object>> OrderBy { get; set; }
		public Expression<Func<T, object>> OrderByDesc { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
        public bool IsPagination { get; set; }


    }
}
