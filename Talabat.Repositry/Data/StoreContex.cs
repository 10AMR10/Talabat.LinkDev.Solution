using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Repositry.Data.config;

namespace Talabat.Repositry.Data
{
	public class StoreContex : DbContext
	{
        ///this way doesn't support DP injection 
        ///public StoreContex():base()
        ///
        ///}
        ///protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        ///{
        ///	base.OnConfiguring(optionsBuilder);
        ///}
        ///less constructor -> less construnctor -> prameterize constructor take object from DbContextOptions
        ///so we override on OnConifguring to add options 
        // chaning directly on prameterize constructor
        public StoreContex(DbContextOptions<StoreContex> options):base(options) 
        {
            
        }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
            ///old
			///modelBuilder.ApplyConfiguration(new ProductConfiguration());
			///modelBuilder.ApplyConfiguration(new ProductBrandConfiguration());
			///modelBuilder.ApplyConfiguration(new ProductCategoryConfiguration());
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
		}
		public DbSet<Product> products { get; set; }
        public DbSet<ProductBrand> productBrands { get; set; }
        public DbSet<ProductCategory> productCategories { get; set; }
    }
}
