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
	internal class OrderItemConfigurations : IEntityTypeConfiguration<ProductItemOrder>
	{
		public void Configure(EntityTypeBuilder<ProductItemOrder> builder)
		{
			builder.OwnsOne(order => order.product, product => product.WithOwner());
			builder.Property(order => order.Price)
				.HasColumnType("decimal(12,2)");
		}
	}
}
