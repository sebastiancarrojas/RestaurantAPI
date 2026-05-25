using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using RestaurantAPI.Domain.Entities;
using RestaurantAPI.Domain.Interfaces.Repositories;

namespace RestaurantAPI.DataAccess.Repositories
{
    public class RestaurantRepository : GenericRepository<Restaurant>, IRestaurantRepository
    {
        public RestaurantRepository(RestaurantDbContext context) : base(context)
        {                           
        }

        public async Task<Restaurant?> GetByNameAsync(string name)
        {
            return await _dbSet
                .FirstOrDefaultAsync(r => r.Name.ToLower() == name.ToLower());
        }
        public async Task<IEnumerable<Restaurant>> GetByCityAsync(string city)
        {
            return await _dbSet
                .Where(r => r.Address.ToLower() == city.ToLower())
                .ToListAsync();
        }
    }

}
