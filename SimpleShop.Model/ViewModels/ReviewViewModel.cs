using System;
using Microsoft.AspNetCore.Mvc;

namespace Model.ViewModels;

public class ReviewViewModel
{
    public int Id { get; set; }
    public string Text { get; set; }
    [HiddenInput] public int Rating { get; set; }
    public DateTime Date { get; set; }
    public string UserId { get; set; }
    public int ProductId { get; set; }
}