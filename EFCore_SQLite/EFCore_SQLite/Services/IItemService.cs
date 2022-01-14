using EFCore_SQLite.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EFCore_SQLite.Services
{
    public interface IItemService
    {
        Task<bool> AddItemAsync(Item item);
        Task<bool> UpdateItemAsync(Item item);
        Task<bool> DeleteItemAsync(int id);
        Task<Item> GetItemAsync(int id);
        Task<IEnumerable<Item>> GetItemAsync();
    }
}
