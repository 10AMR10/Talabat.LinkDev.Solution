using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Repositry.Data.config
{
	internal class ProductConfiguration : IEntityTypeConfiguration<Product>
	{
		public void Configure(EntityTypeBuilder<Product> builder)
		{
			builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
			builder.Property(x => x.Description).IsRequired();
			builder.Property(x=>x.PictureUrl).IsRequired();
			builder.Property(x=>x.Price).IsRequired()
				.HasColumnType("decimal(18,4)");
			//create the realtion (produt, produtbrand)
			builder.HasOne(x => x.Brand)
				.WithMany(/*x=>x.Products*/)
				.HasForeignKey(x => x.BrandId);
			//create the realtion (produt, produtcategory)
			builder.HasOne(x=>x.CategoryId)
				.WithMany(/*x=>x.Products*/)
				.HasForeignKey(x=>x.CategoryId);

		}
	}
}
