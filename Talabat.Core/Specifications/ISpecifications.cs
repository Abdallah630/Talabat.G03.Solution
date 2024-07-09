using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Module;

namespace Talabat.Core.Specifications
{
	public interface ISpecifications<T> where T : BaseEntity 
	{
        public Expression<Func<T,bool>>? criteria { get; set; }
        public List<Expression<Func<T,object>>> includes { get; set; }
    }
}
