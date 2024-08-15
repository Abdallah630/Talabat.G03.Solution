using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Module.OrderAggregate;

namespace Talabat.Repository._Data.Configurations.Orders
{
	internal class OrderItemConfigurations : IEntityTypeConfiguration<OrderItem>
	{
		public void Configure(EntityTypeBuilder<OrderItem> builder)
		{
			builder.OwnsOne(order => order.product, product => product.WithOwner());
			builder.Property(order => order.Price)
				.HasColumnType("decimal(12,2)");
		}
	}
}
