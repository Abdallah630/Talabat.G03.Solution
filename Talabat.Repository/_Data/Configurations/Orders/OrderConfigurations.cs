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
	internal class OrderConfigurations : IEntityTypeConfiguration<Order>
	{
		public void Configure(EntityTypeBuilder<Order> builder)
		{
			builder.OwnsOne(order => order.ShippingAddress, shippingAddress => shippingAddress.WithOwner());

			builder.Property(status => status.Status).HasConversion
				(
				(orderStatus) => orderStatus.ToString(),
				(orderStatus) => (OrderStatus) Enum.Parse(typeof(OrderStatus),orderStatus)
				);
			builder.Property(order => order.SubTotal)
				.HasColumnType("decimal(12,2)");
		}
	}
}
