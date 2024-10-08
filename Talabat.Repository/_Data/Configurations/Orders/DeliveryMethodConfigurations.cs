﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Module.OrderAggregate;

namespace Talabat.Repository._Data.Configurations.Orders
{
	internal class DeliveryMethodConfigurations : IEntityTypeConfiguration<DeliveryMethod>
	{
		public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
		{
			builder.Property(delivery => delivery.Cost)
				.HasColumnType("decimal(12,2)");
		}
	}
}
