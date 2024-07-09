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

		public BaseSpecifications()
        {
        }

        public BaseSpecifications(Expression<Func<T,bool>> criteriaExpression)
        {
            criteria = criteriaExpression;
            includes = new List<Expression<Func<T, object>>>();

		}
    }
}
