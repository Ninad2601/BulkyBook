using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository.IRepository
{
    //For using this Interface as generic We define <T> as generic class
    public interface IRepository <T> where T : class
    {
        /// <summary>
        /// Declaration of methods for generic use from controller
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        T GetFirstOrDefault(Expression<Func<T, bool>> filter, string? includeProperties = null, bool tracked = true);
        IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, string? includeProperties = null);
        void Add (T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entity);

    }
}
