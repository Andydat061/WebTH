using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;

namespace DemoDB2.Models
{
    public class CartItem
    {
        public Product _product { get; set; }
        public int _quantity { get; set; }
    }
    public class Cart
    {
        List<CartItem> items = new List<CartItem>();
        public List<CartItem> Items { get { return items; } }

        public void Add_Product_Cart(Product _pro, int _quan = 1)
        {
            var item = items.FirstOrDefault(s => s._product.ProductID == _pro.ProductID);
            if (item == null)
            {
                items.Add(new CartItem
                {
                    _product = _pro,
                    _quantity = _quan
                });
            }
            else
            {
                item._quantity += _quan;
            }
        }
        public int Total_Quantity()
        {
            return items.Sum(s => s._quantity);
        }
        public decimal Total_Money()
        {
            var total = items.Sum(s => s._quantity * s._product.Price);
            return (decimal)total;
        }
        public void Update_Quantity(int id, int _new_quan)
        {
            var item = items.Find(s => s._product.ProductID == id);
            if (item != null)
            {
                item._quantity = _new_quan;
            }
        }
        public void Remove_Cart(int id)
        {
            items.RemoveAll(s => s._product.ProductID == id);
        }
        public void Clear_Cart()
        {
            items.Clear();
        }
    }
}

    

    