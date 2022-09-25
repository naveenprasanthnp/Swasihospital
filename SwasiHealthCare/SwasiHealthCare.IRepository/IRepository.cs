using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwasiHealthCare.IRepository
{
    public interface IRepository <T> where T : class
    {
        Task<T> GetById(long Id);
        Task<IEnumerable<T>> GetAll();
        Task Insert(T entity);
        Task Update(T entity);
        Task Delete(object Id);
        Task RemoveRange(IEnumerable<T> entity);
        T GetByIds(long Id);
        IEnumerable<T> GetAllList();
    }
}
