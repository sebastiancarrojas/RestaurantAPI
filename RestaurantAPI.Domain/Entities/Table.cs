namespace RestaurantAPI.Domain.Entities;

public class Table : AuditBase
{
    public int TableId { get; set; }

    // 1 mesa -> muchas reservas
    public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}