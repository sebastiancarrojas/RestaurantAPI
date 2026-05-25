using AutoMapper;
using RestaurantAPI.API.DTOs.Request;
using RestaurantAPI.API.DTOs.Response;
using RestaurantAPI.Domain.Entities;

namespace RestaurantAPI.API.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CustomerRequestDto, Customer>();
        CreateMap<Customer, CustomerResponseDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));

        CreateMap<RestaurantRequestDTO, Restaurant>();
        CreateMap<Restaurant, RestaurantResponseDTO>();
    }
}