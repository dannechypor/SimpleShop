using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Model.DTO;
using Model.Entities;
using Shop.BLL.Infrastructure;
using Shop.BLL.Interfaces;
using SimpleShop.DAL.Interfaces;
using SimpleShop.Model.Entities;

namespace SimpleShop.BLL.Services;

public class ReviewService(IUnitOfWork db, IMapper mapper, ILogger<ReviewService> logger) : IReviewService
{
    public async Task<OperationDetails> Create(ReviewDto reviewDto, string userId)
    {
        if (reviewDto == null)
            return new OperationDetails(false, "Review data is required", "");
            
        if (string.IsNullOrWhiteSpace(userId))
            return new OperationDetails(false, "User ID is required", "");
            
        if (reviewDto.ProductId <= 0)
            return new OperationDetails(false, "Valid product ID is required", "");
            
        if (reviewDto.Rating < 1 || reviewDto.Rating > 5)
            return new OperationDetails(false, "Rating must be between 1 and 5", "");
            
        if (string.IsNullOrWhiteSpace(reviewDto.Text))
            return new OperationDetails(false, "Review text is required", "");

        // Check if user already reviewed this product
        var existingReview = db.ReviewRepository.Get()
            .FirstOrDefault(r => r.ProductId == reviewDto.ProductId && r.UserId == userId);
            
        if (existingReview != null)
            return new OperationDetails(false, "You have already reviewed this product", "");

        db.ReviewRepository.Add(new Review
        {
            Text = reviewDto.Text,
            Rating = reviewDto.Rating,
            Date = DateTime.UtcNow,
            ProductId = reviewDto.ProductId,
            UserId = userId
        });
        await db.SaveAsync();
        return new OperationDetails(true, "Review added successfully", "");
    }

    public List<ReviewDto> GetReviewsByProductId(int productId)
    {
        if (productId <= 0)
        {
            logger.LogWarning("GetReviewsByProductId: Invalid product ID. ProductId: {ProductId}", productId);
            return new List<ReviewDto>();
        }

        try
        {
            var reviews = db.ReviewRepository.All()
                .Where(r => r.ProductId == productId)
                .Include(r => r.User)
                .OrderByDescending(r => r.Date)
                .ToList();

            // Use AutoMapper for mapping
            var reviewDtos = mapper.Map<List<Review>, List<ReviewDto>>(reviews);
            
            // Set UserName manually since it's not in the entity
            for (int i = 0; i < reviews.Count; i++)
            {
                if (reviews[i].User != null)
                {
                    reviewDtos[i].UserName = $"{reviews[i].User.FirstName} {reviews[i].User.LastName}";
                }
                else
                {
                    reviewDtos[i].UserName = "Anonymous";
                }
            }

            logger.LogDebug("GetReviewsByProductId: Retrieved {Count} reviews for ProductId: {ProductId}", 
                reviewDtos.Count, productId);
            return reviewDtos;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error retrieving reviews. ProductId: {ProductId}", productId);
            return new List<ReviewDto>();
        }
    }

    public double GetAverageRatingByProductId(int productId)
    {
        if (productId <= 0)
            return 0;

        return db.ReviewRepository.Get()
            .Where(r => r.ProductId == productId)
            .Select(r => (double)r.Rating)
            .DefaultIfEmpty(0)
            .Average();
    }
}