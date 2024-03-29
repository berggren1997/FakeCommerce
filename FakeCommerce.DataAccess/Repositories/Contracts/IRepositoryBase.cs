﻿using System.Linq.Expressions;

namespace FakeCommerce.DataAccess.Repositories.Interfaces
{
    public interface IRepositoryBase<T>
    {
        IQueryable<T> FindAll(bool trackChanges);
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges);

        void Create(T entity);
        void Delete(T entity);
    }
}
