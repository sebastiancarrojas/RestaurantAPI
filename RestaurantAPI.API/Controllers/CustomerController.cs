using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.API.DTOs.Request;
using RestaurantAPI.API.DTOs.Response;
using RestaurantAPI.Domain.Entities;
using RestaurantAPI.Domain.Interfaces.Services;

namespace RestaurantAPI.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
    private readonly ICustomerService _customerService;
    private readonly IMapper _mapper;
    private readonly ILogger<CustomersController> _logger;

    public CustomersController(ICustomerService customerService, IMapper mapper, ILogger<CustomersController> logger)
    {
        _customerService = customerService;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CustomerResponseDto>>> GetAll()
    {
        var customers = await _customerService.GetAllAsync();
        var customersDto = _mapper.Map<IEnumerable<CustomerResponseDto>>(customers);
        return Ok(customersDto);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CustomerResponseDto>> GetById(int id)
    {
        var customer = await _customerService.GetByIdAsync(id);
        if (customer == null)
            return NotFound(new { message = $"Cliente con ID {id} no encontrado" });

        var customerDto = _mapper.Map<CustomerResponseDto>(customer);
        return Ok(customerDto);
    }

    [HttpPost]
    public async Task<ActionResult<CustomerResponseDto>> Create(CustomerRequestDto dto)
    {
        try
        {
            var customer = _mapper.Map<Customer>(dto);
            var created = await _customerService.CreateAsync(customer);
            var responseDto = _mapper.Map<CustomerResponseDto>(created);
            return CreatedAtAction(nameof(GetById), new { id = responseDto.Id }, responseDto);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, CustomerRequestDto dto)
    {
        try
        {
            var customer = _mapper.Map<Customer>(dto);
            await _customerService.UpdateAsync(id, customer);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }
}