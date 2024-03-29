﻿using FakeCommerce.DataAccess.Data;
using FakeCommerce.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FakeCommerce.DataAccess.Repositories.Implementations
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private readonly CommerceDbContext _context;

        public RepositoryBase(CommerceDbContext context)
        {
            _context = context;
        }

        public void Create(T entity) => _context.Set<T>().Add(entity);

        public void Delete(T entity) => _context.Set<T>().Remove(entity);

        public IQueryable<T> FindAll(bool trackChanges) =>
            !trackChanges ? _context.Set<T>().AsNoTracking() : _context.Set<T>();

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges) =>
            !trackChanges ? _context.Set<T>().Where(expression).AsNoTracking() : _context.Set<T>().Where(expression);
    }
}
