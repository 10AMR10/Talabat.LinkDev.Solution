using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Repositry.Data
{
    public class StoreContextSeeding
    {
        public async static Task SeedingAsync(StoreContex _dbcontext)
        {
            if (_dbcontext.Set<ProductBrand>().Count() == 0)
            {
                var brandData = File.ReadAllText("../Talabat.Repositry/Data/DataSeeding/brands.json"); //reading file as string or json
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandData); //transforming json to List of porduct brand
                if (brands?.Count > 0)
                {
                    //brands = brands.Select(x => new ProductBrand
                    //{
                    //	Name = x.Name,
                    //}).ToList();
                    foreach (var brand in brands)
                    {
                        _dbcontext.Set<ProductBrand>().Add(brand);
                    }
                    await _dbcontext.SaveChangesAsync();
                }
            }
            if (_dbcontext.Set<ProductCategory>().Count() == 0)
            {
                var categoryData = File.ReadAllText("../Talabat.Repositry/Data/DataSeeding/categories.json"); //reading file as string or json
                var categories = JsonSerializer.Deserialize<List<ProductCategory>>(categoryData); //transforming json to List of porduct brand
                if (categories?.Count > 0)
                {
                    //brands = brands.Select(x => new ProductBrand
                    //{
                    //	Name = x.Name,
                    //}).ToList();
                    foreach (var category in categories)
                    {
                        _dbcontext.Set<ProductCategory>().Add(category);
                    }
                    await _dbcontext.SaveChangesAsync();
                }
            }
            if (_dbcontext.Set<Product>().Count() == 0)
            {
                var productData = File.ReadAllText("../Talabat.Repositry/Data/DataSeeding/products.json"); //reading file as string or json
                var products = JsonSerializer.Deserialize<List<Product>>(productData); //transforming json to List of porduct brand
                if (products?.Count > 0)
                {
                    //brands = brands.Select(x => new ProductBrand
                    //{
                    //	Name = x.Name,
                    //}).ToList();
                    foreach (var pr in products)
                    {
                        _dbcontext.Set<Product>().Add(pr);
                    }
                    await _dbcontext.SaveChangesAsync();
                }
            }
        }
    }
}
