﻿namespace API.Entities;

public class ShortenedUrl
{
    public Guid Id { get; set; }
    public string LongUrl { get; set; } = string.Empty;
    public string ShortUrl { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; }
    public int CreatedBy { get; set; }
    public User User { get; set; } = null!;
}
