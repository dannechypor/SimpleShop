using Model.DTO;
using Model.Entities;
using Shop.BLL.Infrastructure;
using Shop.BLL.Interfaces;
using Shop.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.BLL.Services
{
    
    public class ReviewService : IReviewService
    {
        private readonly IUnitOfWork Database;

        public ReviewService(IUnitOfWork db)
        {
            Database = db;
        }


        public async Task<OperationDetails> Create(ReviewDTO reviewDTO , string userId)
        {

            Database.ReviewRepository.Add(new Review {Text= reviewDTO.Text, Rating = reviewDTO.Rating , Date = DateTime.UtcNow , ProductId = reviewDTO.ProductId , UserId = userId });
            await Database.SaveAsync();
            return new OperationDetails(true, "", "");
        }


    }
}
