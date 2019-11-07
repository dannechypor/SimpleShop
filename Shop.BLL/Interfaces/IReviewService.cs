using Model.DTO;
using Shop.BLL.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shop.BLL.Interfaces
{
    public interface IReviewService
    {
        Task<OperationDetails> Create(ReviewDTO reviewDTO, string userId);
    }
}
