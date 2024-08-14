using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Module;
using Talabat.Core.Specifications;

namespace Talabat.Repository
{
	public static class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
	{
		public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecifications<TEntity> spec)
		{
			var query = inputQuery; // _dbContext.set<Product>()
			if(spec.criteria is not null) // p => p.Id ==1
				query = query.Where(spec.criteria);

			//query = _dbContext.set<Product>().where(p => p.Id ==1)
			// 1.includeExpretions	
			//	1.1 p=>p.Brand
			//	1.2 p=>p.Category

			query = spec.includes.Aggregate(query, (currentQuery, includeExpression) => currentQuery.Include(includeExpression));

			// query = _dbContext.set<Product>().where(p => p.Id ==1).Include(p=>p.Brand).include(p=>p.Category)
			return query;
		}
	}
}
