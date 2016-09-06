using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace wedding.Contracts.Repositories
{
    public interface IRepositoryBase<TEntity>
     where TEntity : class
    {
        TEntity GetById(object id);
        IQueryable<TEntity> GetAll();
        IEnumerable<TEntity> SQLquery(string searchExpression);
        IQueryable<TEntity> GetPaged(int top = 20, int skip = 0, object orderBy = null, object filter = null);
        IQueryable<TEntity> GetAll(object filter);
        IEnumerable<TEntity> Where(Expression<Func<TEntity, bool>> predicate);
        TEntity GetFullObject(object id);
        int Count();
        int CountById(int id);
        void Insert(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        void Delete(object id);
        void Commit();
        void Dispose();
    }
}
