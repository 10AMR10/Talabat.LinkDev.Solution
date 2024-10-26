using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities
{
	public class ProductBrand:BaseEntity
	{
        public string Name { get; set; }
        //public ICollection<Product> products { get; set; } we don't need it so we will make it by fluent api
    }
}
