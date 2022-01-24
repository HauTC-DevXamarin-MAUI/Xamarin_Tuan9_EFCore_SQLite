using EFCore_SQLite.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EFCore_SQLite.Services
{
    public interface ICategoryService
    {
        Task<bool> AddCategoryAsync(Category category);
        Task<bool> UpdateCategoryAsync(Category category);
        Task<bool> DeleteCategoryAsync(int id);
        Task<Category> GetCategoryAsync(int id);
        Task<IEnumerable<Category>> GetCategoryAsync();
    }
}
