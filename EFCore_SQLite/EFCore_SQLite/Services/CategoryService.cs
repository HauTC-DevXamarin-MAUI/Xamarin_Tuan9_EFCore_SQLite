using EFCore_SQLite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore_SQLite.Services
{
    public class CategoryService : BaseService<Category>, ICategoryService
    {
        private DatabaseContext _dbContext = new DatabaseContext();
        public CategoryService()
        {

        }
        public async Task<IEnumerable<Category>> GetCategoryAsync()
        {
            return await Task.FromResult(_dbContext.Categorys.ToList());
        }

        public async Task<bool> UpdateCategoryAsync(Category category)
        {
            try
            {
                var categoryResult = _dbContext.Categorys.Where(t => t.Id.Equals(category.Id)).FirstOrDefault();
                if (categoryResult != null)
                {
                    categoryResult.Name = category.Name;

                    _dbContext.Categorys.Update(categoryResult);
                    await _dbContext.SaveChangesAsync();
                }
                return true;
                //_dbContext.Items.Remove(item);
                //await _dbContext.SaveChangesAsync();
                //return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }

    #region Bỏ (để ghi chú)
    //public class CategoryService : ICategoryService
    //{
    //    private DatabaseContext _dbContext = new DatabaseContext();

    //    public CategoryService()
    //    {

    //    }
    //    public async Task<bool> AddCategoryAsync(Category category)
    //    {
    //        await _dbContext.Categorys.AddAsync(category);
    //        await _dbContext.SaveChangesAsync();
    //        return true;
    //    }

    //    public async Task<bool> DeleteCategoryAsync(int id)
    //    {
    //        var categoryResult = _dbContext.Categorys.Where(t => t.Id.Equals(id)).FirstOrDefault();
    //        if (categoryResult != null)
    //        {
    //            _dbContext.Categorys.Remove(categoryResult);
    //            await _dbContext.SaveChangesAsync();
    //        }
    //        return true;
    //    }

    //    public async Task<Category> GetCategoryAsync(int id)
    //    {
    //        return await Task.FromResult(_dbContext.Categorys.Where(t => t.Id.Equals(id)).FirstOrDefault());
    //    }

    //    public async Task<IEnumerable<Category>> GetCategoryAsync()
    //    {
    //        return await Task.FromResult(_dbContext.Categorys.ToList());

    //    }

    //    public async Task<bool> UpdateCategoryAsync(Category category)
    //    {
    //        try
    //        {
    //            var categoryResult = _dbContext.Categorys.Where(t => t.Id.Equals(category.Id)).FirstOrDefault();
    //            if (categoryResult != null)
    //            {
    //                categoryResult.Name = category.Name;

    //                _dbContext.Categorys.Update(categoryResult);
    //                await _dbContext.SaveChangesAsync();
    //            }
    //            return true;
    //            //_dbContext.Items.Remove(item);
    //            //await _dbContext.SaveChangesAsync();
    //            //return true;
    //        }
    //        catch (Exception ex)
    //        {
    //            Console.WriteLine(ex.Message);
    //            return false;
    //        }
    //    }


    //}
    #endregion
}
