namespace RestaurantAPI.Domain.Entities;

public class Order : AuditBase
{
    public int OrderId { get; set; }

    // FK 1:1 con Reservation
    public int ReservationId { get; set; }

    public Reservation Reservation { get; set; } = null!;
}