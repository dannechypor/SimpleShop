using SimpleShop.DAL.EF;
using SimpleShop.DAL.Interfaces;
using SimpleShop.Model.Entities;

namespace SimpleShop.DAL.Repositories;

public class WishListRepository(ApplicationDbContext db) : Repository<WishListItem>(db), IWishListRepository;