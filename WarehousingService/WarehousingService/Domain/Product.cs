using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WarehousingService.Domain
{
    public class Product
    {
        public Guid Id { get; }
        public ProductName Name { get; }
        public decimal Price { get;}

        public Product(string name, decimal price)
        {
            Id = Guid.NewGuid();

            Name = new ProductName(name);
            Price = price;
        }
    }
}
