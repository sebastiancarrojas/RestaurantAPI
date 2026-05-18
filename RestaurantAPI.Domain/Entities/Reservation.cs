using System.Reflection.Emit;

namespace RestaurantAPI.Domain.Entities;

public class Reservation : AuditBase
{
    public int ReservationId { get; set; }

    // 1:N Customer
    public int CustomerId { get; set; }
    public Customer Customer { get; set; } = null!;

    // 1:N Table
    public int TableId { get; set; }
    public Table Table { get; set; } = null!;

    public DateTime ReservationDate { get; set; }

    public int PartySize { get; set; }

    public ReservationStatus Status { get; set; }

    public string? Notes { get; set; }

    // 1:1 Order
    public Order? Order { get; set; }
}