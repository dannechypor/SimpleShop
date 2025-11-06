using System.Collections.Generic;
using System.Threading.Tasks;
using Model.DTO;
using Shop.BLL.Infrastructure;

namespace Shop.BLL.Interfaces;

public interface IReviewService
{
    Task<OperationDetails> Create(ReviewDto reviewDTO, string userId);
    List<ReviewDto> GetReviewsByProductId(int productId);
    double GetAverageRatingByProductId(int productId);
}