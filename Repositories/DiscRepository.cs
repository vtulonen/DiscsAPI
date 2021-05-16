using DiscsAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiscsAPI.Repositories
{
    /// <summary>
    ///     Repository works as a layer between the application and DataAccess layer (DiscContext)
    /// </summary>
    public class DiscRepository : IDiscRepository
    {
        private readonly DiscContext _context; 

        public DiscRepository(DiscContext context) // Context dependency injected trough the constructor
        {
            _context = context;
        }
        public async Task<Disc> Create(Disc disc)
        {
            _context.Discs.Add(disc);
            await _context.SaveChangesAsync();
            return disc;
        }

        public async Task Delete(int id)
        {
            var discToDelete = await _context.Discs.FindAsync(id);
            _context.Discs.Remove(discToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Disc>> Get()
        {
            return await _context.Discs.ToListAsync();
        }

        public async Task<Disc> Get(int id)
        {
            return await _context.Discs.FindAsync(id);
        }

        public async Task<Disc> GetDisc(string name)
        {
            return await _context.Discs.FirstOrDefaultAsync(d => d.Name == name);
        }

        public async Task Update(Disc disc)
        {
            _context.Entry(disc).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
