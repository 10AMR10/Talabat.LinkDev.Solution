using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.specification.Products_Specific
{
	public class ProductCountSpecification:BaseSpecification<Product>
	{
        // barndID , categoryId
        public ProductCountSpecification(ProductSpecPrams productPrams)
            :base(p=>
                 (!productPrams.BrandId.HasValue || p.BrandId==productPrams.BrandId)  &&
                (!productPrams.CategoryId.HasValue || p.CategoryId==productPrams.CategoryId ) 
            )
        {
            
        }
    }
}
