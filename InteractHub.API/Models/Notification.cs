namespace InteractHub.API.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public bool IsRead { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public string UserId { get; set; } = string.Empty;
        public User User { get; set; } = null!;
    }
}