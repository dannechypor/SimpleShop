using Model.Entities;
using Shop.DAL.EF;
using Shop.DAL.Interfaces;

namespace Shop.DAL.Repositories;

public class WishListRepository(ApplicationDbContext db) : Repository<WhishListItem>(db), IWishListRepository;