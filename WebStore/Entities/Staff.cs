namespace WebStore.Entities
{
    public class Staff
    {
        public int StaffId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Phone { get; set; }
        public int StoreId { get; set; }

        public Store Store { get; set; } = null!;
    }
} 