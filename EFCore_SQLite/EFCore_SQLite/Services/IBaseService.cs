using EFCore_SQLite.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EFCore_SQLite.Services
{
    public interface IBaseService<T> where T : class
    {
        Task<bool> AddAsync(T entity);
        //Task<bool> UpdateAsync(T entity);
        Task<bool> DeleteAsync(int id);
        Task<T> GetAsync(int id);
        //Task<IEnumerable<T>> GetAsync();
        Task<IEnumerable<Item>> GetListStudentAsync(int idCategory);
    }
}
