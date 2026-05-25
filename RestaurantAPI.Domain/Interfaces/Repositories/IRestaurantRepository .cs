using RestaurantAPI.Domain.Entities;

namespace RestaurantAPI.Domain.Interfaces.Repositories
{
    public interface IRestaurantRepository : IGenericRepository<Restaurant> 
    {
        Task<Restaurant?> GetByNameAsync (string name);
        Task<IEnumerable<Restaurant>> GetByCityAsync(string city);
    }
}
