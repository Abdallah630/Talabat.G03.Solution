using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Module;
using Talabat.Core.Specifications;

namespace Talabat.Core.Repositories.Contract
{
	public interface IGenericRepository <T> where T : BaseEntity
	{
		Task<T?> GetASync(int id);
		Task<IEnumerable<T>> GetAllAsync();

		Task<T?> GetWithSpecASync(ISpecifications<T> spec);
		Task<IEnumerable<T>> GetAllWithSpecAsync(ISpecifications<T> spec);
	}
}
