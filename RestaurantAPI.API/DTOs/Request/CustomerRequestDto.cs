using System.ComponentModel.DataAnnotations;

namespace RestaurantAPI.API.DTOs.Request;

public class CustomerRequestDto
{
    [Required(ErrorMessage = "El nombre es requerido")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "El apellido es requerido")]
    public string LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "El email es requerido")]
    [EmailAddress(ErrorMessage = "El email no es válido")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "El teléfono es requerido")]
    public string Phone { get; set; } = string.Empty;
}