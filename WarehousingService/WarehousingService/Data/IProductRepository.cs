using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WarehousingService.Domain;

namespace WarehousingService.Data
{
    public interface IProductRepository
    {
        Task Create(Product product);
        bool Exist(string name);
    }


    public class ProductStoreFake : IProductRepository
    {
        public IList<Product> Products { get; set; } = new List<Product>();

        public Task Create(Product product)
        {
            Products.Add(product);
            return Task.CompletedTask;
        }

        public bool Exist(string name)
        {
            return Products.Any(p => p.Name.Value == name);
        }
    }
}
