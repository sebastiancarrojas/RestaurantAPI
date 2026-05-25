using Microsoft.Extensions.Logging;
using RestaurantAPI.Domain.Entities;
using RestaurantAPI.Domain.Interfaces.Repositories;
using RestaurantAPI.Domain.Interfaces.Services;

namespace RestaurantAPI.Domain.Services
{
    public class RestaurantService : IRestaurantService
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly ILogger<RestaurantService> _logger;

        public RestaurantService(IRestaurantRepository restaurantRepository, ILogger<RestaurantService> logger)
        {
            _restaurantRepository = restaurantRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<Restaurant>> GetAllAsync()
        {
            _logger.LogInformation("Ver todos los restaurantes");
            return await _restaurantRepository.GetAllAsync();
        }

        public async Task<Restaurant?> GetByIdAsync(int id)
        {
            _logger.LogInformation("Buscar restaurante por ID: {Id}", id);
            return await _restaurantRepository.GetByIdAsync(id);
        }

        public async Task<Restaurant> CreateAsync(Restaurant restaurant)
        {
            // Validación: nombre único
            var existing = await _restaurantRepository.GetByNameAsync(restaurant.Name);
            if (existing != null)
                throw new InvalidOperationException($"Ya existe un restaurante con el nombre '{restaurant.Name}'");

            _logger.LogInformation("Crear restaurante: {Name}", restaurant.Name);
            return await _restaurantRepository.CreateAsync(restaurant);
        }

        public async Task UpdateAsync(int id, Restaurant restaurant)
        {
            var existing = await _restaurantRepository.GetByIdAsync(id);
            if (existing == null)
                throw new KeyNotFoundException($"No se encontró el restaurante con ID {id}");

            // Si cambia el nombre, validar que no haya duplicado
            if (existing.Name != restaurant.Name)
            {
                var nameConflict = await _restaurantRepository.GetByNameAsync(restaurant.Name);
                if (nameConflict != null)
                    throw new InvalidOperationException($"Ya existe un restaurante con el nombre '{restaurant.Name}'");
            }

            existing.Name = restaurant.Name;
            existing.Address = restaurant.Address;
            existing.Phone = restaurant.Phone;
            existing.Email = restaurant.Email;
            existing.OpenTime = restaurant.OpenTime;
            existing.CloseTime = restaurant.CloseTime;

            _logger.LogInformation("Actualizar restaurante con ID: {Id}", id);
            await _restaurantRepository.UpdateAsync(existing);
        }

        public async Task DeleteAsync(int id)
        {
            var exists = await _restaurantRepository.ExistsAsync(id);
            if (!exists)
                throw new KeyNotFoundException($"No se encontró el restaurante con ID {id}");

            _logger.LogInformation("Eliminar restaurante con ID: {Id}", id);
            await _restaurantRepository.DeleteAsync(id);
        }

        public async Task<Restaurant?> GetByNameAsync(string name)
        {
            _logger.LogInformation("Buscar restaurante por nombre: {Name}", name);
            return await _restaurantRepository.GetByNameAsync(name);
        }

        public async Task<IEnumerable<Restaurant>> GetByCityAsync(string city)
        {
            _logger.LogInformation("Buscar restaurantes en la ciudad: {City}", city);
            return await _restaurantRepository.GetByCityAsync(city);
        }
    }
}
