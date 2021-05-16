using DiscsAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiscsAPI.Repositories
{
    public interface IDiscRepository
    {
        Task<IEnumerable<Disc>> Get();
        Task<Disc> Get(int id);
        Task<Disc> GetDisc(string name);
        Task<Disc> Create(Disc disc);
        Task Update(Disc disc);
        Task Delete(int id);
    }
}
