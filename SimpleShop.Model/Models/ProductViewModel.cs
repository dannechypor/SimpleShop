using Model.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model.Models
{
   public class ProductViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter your name of the food")]
        [Display(Name = "Product name*")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter url of the food image")]
        [Display(Name = "Image url*")]
        public string ImageUrl_1 { get; set; }
        [Required(ErrorMessage = "Please enter url of the food image")]
        [Display(Name = "Image url*")]
        public string ImageUrl_2 { get; set; }
        [Required(ErrorMessage = "Please enter url of the food image")]
        [Display(Name = "Image url*")]
        public string ImageUrl_3 { get; set; }

        [Required]
        public string ShortDescription { get; set; }

        [Required]
        public string LongDescription { get; set; }


       

        [Required]
        public string Higth { get; set; }

        [Required]
        public string Width { get; set; }

        [Required]
        public string Deepth { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public bool InStock { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public int ManufacturerId { get; set; }

    }
}
