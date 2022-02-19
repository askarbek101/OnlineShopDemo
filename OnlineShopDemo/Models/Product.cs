using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopDemo.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string name { get; set; }
        public string slug { get; set; }
        public int price { get; set; }
        public string image { get; set; }
        public string brand { get; set; }
        public string category { get; set; }
        public string description { get; set; }
        public int countInStock { get; set; }
        public int rating { get; set; }
        public int numReviews { get; set; }
    }
}

