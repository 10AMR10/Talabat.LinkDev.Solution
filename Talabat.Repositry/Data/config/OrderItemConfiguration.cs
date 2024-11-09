using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.orderAgregrate;

namespace Talabat.Repositry.Data.config
{
	public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
	{
		public void Configure(EntityTypeBuilder<OrderItem> builder)
		{
			builder.Property(oi => oi.Price).HasColumnType("decimal(18,2)");
			builder.OwnsOne(oi => oi.Product, p => p.WithOwner());

		}
	}
}
