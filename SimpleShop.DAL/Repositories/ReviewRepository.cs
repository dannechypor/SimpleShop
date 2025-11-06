using Model.Entities;
using Shop.DAL.EF;
using Shop.DAL.Interfaces;

namespace Shop.DAL.Repositories;

public class ReviewRepository(ApplicationDbContext db) : Repository<Review>(db), IReviewRepository;