using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmManager.DAL.Repositories.Interfaces;

public interface IBaseRepository<T>
{
    Task<IEnumerable<T>> GetAll();
    Task<T> GetById(int id);
    Task<int?> Create(T entity);
    Task<int?> Update(T entity);
    Task<int?> Delete(int id);
}
