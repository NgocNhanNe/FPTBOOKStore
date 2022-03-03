using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FPTBook.Models
{
        public class CartItem
        {
            public Book _cart_book { get; set; }
            public int _cart_quantity { get; set; }
        }

        public class MyCart
        {
            List<CartItem> items = new List<CartItem>();
            public IEnumerable<CartItem> Items
            {
                get { return items; }
            }

            public void Add(Book _book, int _quantity = 1)
            {
                var item = items.FirstOrDefault(s => s._cart_book.Book_ID == _book.Book_ID);
                if (item == null)
                {
                    items.Add(new CartItem
                    {
                        _cart_book = _book,
                        _cart_quantity = _quantity
                    });
                }
                else
                {
                    item._cart_quantity += _quantity;
                }
            }

            public void Update_Quantity_Book(int id, int _quantity)
            {
                var item = items.Find(s => s._cart_book.Book_ID == id);
                if (item != null)
                {
                    item._cart_quantity = _quantity;
                }
            }

            public double Amount()
            {
                var total = items.Sum(s => s._cart_book.Price * s._cart_quantity);

                return total;
            }

            public int Total()
            {
                return items.Sum(s => s._cart_quantity);
            }

            public void DeleteCart(int id)
            {
                items.RemoveAll(s => s._cart_book.Book_ID == id);
            }

            public void ClearCart()
            {
                items.Clear();
            }
        }
    }
