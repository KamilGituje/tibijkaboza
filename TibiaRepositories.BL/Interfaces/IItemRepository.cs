using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TibiaModels.BL;

namespace TibiaRepositories.BL.Interfaces
{
    public interface IItemRepository
    {
        Task<Item> GetAsync(int itemId);
        Task<Item> GetByNameAsync(string name);
        Task<bool> IsExistAsync(string name);
    }
}
