namespace RestaurantAPI.Domain.Entities;

public class Customer : AuditBase
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;

    // Relación: un Customer puede tener muchas Reservations
    public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}