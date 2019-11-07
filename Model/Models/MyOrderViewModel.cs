using Model.DTO;
using Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Models
{
    public class MyOrderViewModel
    {
       
        public IEnumerable<MyOrderInfoDTO> Orders { get; set; }
        public OrderDTO Statust { get; set; }
           
    }
}
