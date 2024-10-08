﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Module;
using Talabat.Core.Module.Product;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Specifications;
using Talabat.Repository._Data.DataContext;

namespace Talabat.Repository.GenericRepository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreContext _context;

        public GenericRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            if (typeof(T) == typeof(Products))
                return (IReadOnlyList<T>)await _context.Set<Products>().Include(p => p.Brand).Include(p => p.Category).ToListAsync();
            return await _context.Set<T>().ToListAsync();
        }
        public async Task<T?> GetAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecifications<T> spec)
        {
            return await ApplySpecifications(spec).AsNoTracking().ToListAsync();
        }


        public async Task<T?> GetWithSpecASync(ISpecifications<T> spec)
        {
            return await ApplySpecifications(spec).FirstOrDefaultAsync();
        }


		public Task<int> GetCountAsync(ISpecifications<T> spec)
		{
            return ApplySpecifications(spec).CountAsync();
		}

        private IQueryable<T> ApplySpecifications(ISpecifications<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_context.Set<T>(), spec);
        }
		
		public void AddAsync(T entity)
            => _context.Set<T>().Add(entity);
        public void Update(T entity)
         => _context.Set<T>().Update(entity);

        public void Delete(T entity)
         => _context.Set<T>().Remove(entity);

	
	}


}
