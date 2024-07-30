namespace API.Entities;

public class User
{
    public int ID { get; set; }
    public string Username { get; set; } = String.Empty;
    public string Role { get; set; } = String.Empty;
    public byte[] PasswordSalt { get; set; } = [];
    public byte[] PasswordHash { get; set; } = [];
    public List<ShortenedUrl> ShortenedUrls { get; set; } = new List<ShortenedUrl>();
}
