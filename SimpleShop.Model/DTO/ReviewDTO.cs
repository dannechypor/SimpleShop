using System;

namespace Model.DTO;

public abstract class ReviewDto
{
    public int Id { get; set; }
    public string Text { get; set; }
    public int Rating { get; set; }
    public DateTime Date { get; set; }
    public string UserId { get; set; }
    public int ProductId { get; set; }
}