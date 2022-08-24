using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TibiaModels.BL;

namespace TibiaRepositories.BL.Interfaces
{
    public interface IItemInstanceRepository
    {
        Task<int> AddAsync(ItemInstance itemInstance);
    }
}
