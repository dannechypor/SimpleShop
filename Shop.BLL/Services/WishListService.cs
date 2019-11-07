using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model.DTO;
using Model.Entities;
using Model.Models;
using Shop.BLL.Infrastructure;
using Shop.BLL.Interfaces;
using Shop.DAL;
using Shop.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Shop.BLL.Services
{
    public class WishListService : IWishListService
    {
        private readonly IUnitOfWork Database;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        public WishListService(IMapper mapper, IUnitOfWork db , ApplicationDbContext context)

        {
            _mapper = mapper;
            Database = db;
            _context = context;
        }


        public async Task AddToWhishList([FromForm]WishListDTO wishListDto)
        {
            var whishList = _mapper.Map<WishListDTO, WhishListItem>(wishListDto);

            whishList = new WhishListItem
            {
                ProductId = wishListDto.ProductId,
                UserId = wishListDto.UserId

            };

            Database.WishListPrepository.Add(whishList);
            await Database.SaveAsync();

        }

        public List<Product> GetAllProductIdByUser(string userId )
        {
             List<Product> myWishLists = new List<Product>();

            var products = Database.WishListPrepository.All().Where(e => e.UserId == userId).ToList();
                  //.Include(e => e.ProductId)
                  //.Select(e => new Product
                  //{
                  //    Id = e.ProductId
                  //})
                  //.ToList();

           
                foreach (var item in products)
                {
                    var tmp  = Database.ProductRepository.GetAll().Where(e=> e.Id == item.ProductId);
                    myWishLists.Add(tmp.First());
                }


            return myWishLists;
        }

        public int GetCountOfWishList(string userId)
        {
            var products = Database.WishListPrepository.All().Where(e => e.UserId == userId)
                 .Include(e => e.ProductId)
                 .Select(e => new Product
                 {
                     Id = e.ProductId
                 }).ToList();

            return (products.Count());
        }

        public async Task<OperationDetails> DeleteRelationship(int productID, string userId)
        {
            
               
                var product = _context.WhishListItems.FirstOrDefault(p => p.ProductId == productID);
                var supplier = _context.WhishListItems.FirstOrDefault(s => s.UserId == userId);

                
                _context.WhishListItems.Remove(supplier);
                _context.WhishListItems.Remove(product);

              
               await _context.SaveChangesAsync();

            return new OperationDetails(true, "", "");

        }


    }
}
