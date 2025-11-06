using Model.Entities;
using Shop.DAL.Interfaces;
using SimpleShop.DAL.EF;
using SimpleShop.DAL.Interfaces;
using SimpleShop.Model.Entities;

namespace Shop.DAL.Repositories;

public class WishListRepository(ApplicationDbContext db) : Repository<WishListItem>(db), IWishListRepository;