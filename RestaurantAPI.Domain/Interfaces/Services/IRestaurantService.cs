using RestaurantAPI.Domain.Entities;

namespace RestaurantAPI.Domain.Interfaces.Services
{
   public interface IRestaurantService 
    {
        Task<IEnumerable<Restaurant>> GetAllAsync();
        Task<Restaurant?> GetByIdAsync(int id);
        Task<Restaurant> CreateAsync(Restaurant restaurant);
        Task UpdateAsync(int id, Restaurant restaurant);
        Task DeleteAsync (int id);
        Task<Restaurant?> GetByNameAsync(string name);
        Task<IEnumerable<Restaurant>> GetByCityAsync(string city);
    }
}
