using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.SpecificationTest
{
	public class BaseSpecificationTest<T> :ISpecificationTest<T> where T : BaseEntity
	{
		public Expression<Func<T, bool>?> Criteria { get; set; } = null;
		public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();


		public BaseSpecificationTest()
        {
           
        }
        public BaseSpecificationTest(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }
    }
}
