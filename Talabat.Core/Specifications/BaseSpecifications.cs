using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Module;

namespace Talabat.Core.Specifications
{
	public class BaseSpecifications<T> : ISpecifications<T> where T : BaseEntity
	{
		public Expression<Func<T, bool>>? criteria { get ; set ; } = null;
		public List<Expression<Func<T, object>>> includes { get; set; } = new List<Expression<Func<T, object>>>();
        public Expression<Func<T,object>> OrderBy { get; set; } = null;
        public Expression<Func<T, object>> OrderByDesc { get; set; } = null;
		public int Skip { get; set ; }
		public int Take { get ; set ; }
		public bool IsPaginationEnable { get ; set ; }

		public BaseSpecifications()
        {
        }

        public BaseSpecifications(Expression<Func<T,bool>> criteriaExpression)
        {
            criteria = criteriaExpression;
            includes = new List<Expression<Func<T, object>>>();

		}

        public void AddOrderBy(Expression<Func<T, object>> orderByExpression)
        {
			OrderBy = orderByExpression;
        }
        public void AddOrderByDesc(Expression<Func<T,object>> orderByDescExpression)
        {
            OrderByDesc = orderByDescExpression;
        }
        public void ApplyPagination(int skip , int take)
        {
            IsPaginationEnable = true;
            Skip = skip;
            Take = take;
        }
    }
}
