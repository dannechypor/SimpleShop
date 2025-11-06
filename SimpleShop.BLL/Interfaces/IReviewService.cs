using System.Collections.Generic;
using System.Threading.Tasks;
using SimpleShop.BLL.Infrastructure;
using SimpleShop.Model.DTO;

namespace SimpleShop.BLL.Interfaces;

public interface IReviewService
{
    Task<OperationDetails> Create(ReviewDto reviewDTO, string userId);
    List<ReviewDto> GetReviewsByProductId(int productId);
    double GetAverageRatingByProductId(int productId);
}