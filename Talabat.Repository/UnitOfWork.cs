using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Module;
using Talabat.Core.Repositories.Contract;
using Talabat.Repository._Data.DataContext;
using Talabat.Repository.GenericRepository;

namespace Talabat.Repository
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly StoreContext _dbContext;

		private Hashtable _repositories;
		public UnitOfWork(StoreContext dbContext)
		{
			_dbContext = dbContext;
			_repositories = new Hashtable();
		}

		public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
		{
			var key = typeof(TEntity).Name;
			if (!_repositories.ContainsKey(key))
			{
				var repository = new GenericRepository<TEntity>(_dbContext);
				_repositories.Add(key, repository);
			}
			return _repositories[key] as IGenericRepository<TEntity>;
		}
		public async Task<int> CompleteAsync()
		 => await _dbContext.SaveChangesAsync();

		public ValueTask DisposeAsync()
		 => _dbContext.DisposeAsync();

	}
}
