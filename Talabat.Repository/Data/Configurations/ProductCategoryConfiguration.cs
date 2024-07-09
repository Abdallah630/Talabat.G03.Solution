﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Module;

namespace Talabat.Repository.DataContext.Configurations
{
	internal class ProductCategoryConfiguration : IEntityTypeConfiguration<ProductCategory>
	{
		public void Configure(EntityTypeBuilder<ProductCategory> builder)
		{
			builder.Property(p => p.Name)
				.IsRequired();
		}
	}
}
