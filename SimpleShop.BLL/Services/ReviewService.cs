using System;
using System.Threading.Tasks;
using Model.DTO;
using Model.Entities;
using Shop.BLL.Infrastructure;
using Shop.BLL.Interfaces;
using Shop.DAL.Interfaces;

namespace Shop.BLL.Services;

public class ReviewService(IUnitOfWork db) : IReviewService
{
    public async Task<OperationDetails> Create(ReviewDTO reviewDto, string userId)
    {
        db.ReviewRepository.Add(new Review
        {
            Text = reviewDto.Text, Rating = reviewDto.Rating, Date = DateTime.UtcNow, ProductId = reviewDto.ProductId,
            UserId = userId
        });
        await db.SaveAsync();
        return new OperationDetails(true, "", "");
    }
}