using EFCore_SQLite.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore_SQLite.Services
{
    public class BaseService<T> : IBaseService<T> where T : class
    {
        private DatabaseContext _dbContext;
        private DbSet<T> _dbSet;
        public BaseService()
        {
            _dbContext = new DatabaseContext();
            _dbSet = _dbContext.Set<T>();
        }
        public async Task<bool> AddAsync(T entity)
        {
            try
            {
                await _dbSet.AddAsync(entity);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException dbce)
            {
                Console.WriteLine(dbce.Message);
            }
            catch (DbUpdateException dbue)
            {
                Console.WriteLine(dbue.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return false;
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            try
            {
                _dbSet.Remove(entity);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException dbce)
            {
                Console.WriteLine(dbce.Message);
            }
            catch (DbUpdateException dbue)
            {
                Console.WriteLine(dbue.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return false;
        }

        public async Task<T> GetAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAsync()
        {
            return await Task.FromResult(_dbSet.ToList());
        }

        public async Task<IEnumerable<Item>> GetListStudentAsync(int idCategory)
        {
            return await Task.FromResult(_dbContext.Items.Where(t => t.IdCategory == idCategory).ToList());
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            try
            {
                _dbSet.Update(entity);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException dbce)
            {
                Console.WriteLine(dbce.Message);
            }
            catch (DbUpdateException dbue)
            {
                Console.WriteLine(dbue.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return false;
        }
    }
}
