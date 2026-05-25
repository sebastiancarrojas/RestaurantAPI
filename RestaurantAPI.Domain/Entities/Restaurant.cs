namespace RestaurantAPI.Domain.Entities
{
   public class Restaurant : AuditBase 
    {
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public TimeSpan OpenTime { get; set; }
        public TimeSpan CloseTime { get; set; }

        // Navegation properies
        public ICollection<Table> Tables { get; set; } = new List<Table>();        
    }
}
