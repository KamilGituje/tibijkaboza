using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TibiaModels.BL;

namespace TibiaRepositories.BL.Interfaces
{
    public interface IMonsterRepository
    {
        Task<Monster> GetAsync(int monsterId);
        Task<Monster> GetWithItemsAsync(int monsterId);
    }
}
