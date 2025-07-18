﻿using LiteDB;
using System.Linq.Expressions;

namespace Domain.Repositories.RepositoryBase
{
    public interface IBaseRepository<T> : IDisposable where T : class   
    {
        void Insert(T entity);
        void Insert(IEnumerable<T> entity);
        T GetById(Guid id);
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAll(Func<T, bool> func);
        IEnumerable<T> GetAll(Func<T, bool> func, int pageNumber, int pageSize);
        Task<IEnumerable<T>> GetAllAsync(Func<T, bool> func, int pageNumber, int pageSize);
        IEnumerable<T> GetAll(int pageNumber, int pageSize);
        ILiteCollection<T> GetCollection();
        int Count();
        int CountBy(Func<T, bool> func);
        Task<int> CountAsync();
        void DeleteById(Guid id);
        void DeleteAll();
        void Update(T entity);
        void Update(IEnumerable<T> entity);
    }
}
