using Microsoft.Extensions.Logging;
using RestaurantAPI.Domain.Entities;
using RestaurantAPI.Domain.Interfaces.Repositories;
using RestaurantAPI.Domain.Interfaces.Services;

namespace RestaurantAPI.Domain.Services;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;
    private readonly ILogger<CustomerService> _logger;

    public CustomerService(ICustomerRepository customerRepository, ILogger<CustomerService> logger)
    {
        _customerRepository = customerRepository;
        _logger = logger;
    }

    public async Task<IEnumerable<Customer>> GetAllAsync()
    {
        _logger.LogInformation("Retrieving all customers");
        return await _customerRepository.GetAllAsync();
    }

    public async Task<Customer?> GetByIdAsync(int id)
    {
        _logger.LogInformation("Retrieving customer with ID: {CustomerId}", id);
        var customer = await _customerRepository.GetByIdAsync(id);
        if (customer == null)
            _logger.LogWarning("Customer with ID {CustomerId} not found", id);
        return customer;
    }

    public async Task<Customer> CreateAsync(Customer customer)
    {
        // Validar email único
        var existingEmail = await _customerRepository.GetByEmailAsync(customer.Email);
        if (existingEmail != null)
        {
            _logger.LogWarning("Customer with email '{Email}' already exists", customer.Email);
            throw new InvalidOperationException($"Ya existe un cliente con el email '{customer.Email}'");
        }

        // Validar phone único
        var existingPhone = await _customerRepository.GetByPhoneAsync(customer.Phone);
        if (existingPhone != null)
        {
            _logger.LogWarning("Customer with phone '{Phone}' already exists", customer.Phone);
            throw new InvalidOperationException($"Ya existe un cliente con el teléfono '{customer.Phone}'");
        }

        _logger.LogInformation("Creating customer: {FirstName} {LastName}", customer.FirstName, customer.LastName);
        return await _customerRepository.CreateAsync(customer);
    }

    public async Task UpdateAsync(int id, Customer customer)
    {
        var existing = await _customerRepository.GetByIdAsync(id);
        if (existing == null)
        {
            _logger.LogWarning("Customer with ID {CustomerId} not found for update", id);
            throw new KeyNotFoundException($"No se encontró el cliente con ID {id}");
        }

        // Validar email único si cambió
        if (existing.Email != customer.Email)
        {
            var existingEmail = await _customerRepository.GetByEmailAsync(customer.Email);
            if (existingEmail != null)
                throw new InvalidOperationException($"Ya existe un cliente con el email '{customer.Email}'");
        }

        // Validar phone único si cambió
        if (existing.Phone != customer.Phone)
        {
            var existingPhone = await _customerRepository.GetByPhoneAsync(customer.Phone);
            if (existingPhone != null)
                throw new InvalidOperationException($"Ya existe un cliente con el teléfono '{customer.Phone}'");
        }

        existing.FirstName = customer.FirstName;
        existing.LastName = customer.LastName;
        existing.Email = customer.Email;
        existing.Phone = customer.Phone;

        _logger.LogInformation("Updating customer with ID: {CustomerId}", id);
        await _customerRepository.UpdateAsync(existing);
    }
}