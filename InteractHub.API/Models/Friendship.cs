namespace InteractHub.API.Models
{
    public class Friendship
    {
        public int Id { get; set; }
        public string RequesterId { get; set; } = string.Empty;
        public User Requester { get; set; } = null!;

        public string AddresseeId { get; set; } = string.Empty;
        public User Addressee { get; set; } = null!;

        public int Status { get; set; } 
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}