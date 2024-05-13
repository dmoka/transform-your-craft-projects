using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using WarehousingService.Controllers;
using WarehousingService.Data;
using WarehousingService.Domain;

namespace WarehousingService.Tests
{

    public class InMemoryTestServer : IDisposable
    {
        public HttpClient Client { get; }

        private TestServer TestServer { get; }
        public ProductStoreFake ProductStore { get; set; }

        public InMemoryTestServer()
        {
            ProductStore = new ProductStoreFake();
            TestServer = new TestServer(CreateWebHostBuilder());
;           Client = TestServer.CreateClient();
        }

        public T GetService<T>()
        {
            return TestServer.Host.Services.GetService<T>();
        }


        private IWebHostBuilder CreateWebHostBuilder()
        {
            return new WebHostBuilder()
                .UseContentRoot(Path.GetDirectoryName(Assembly.GetAssembly(typeof(Startup)).Location))
                .ConfigureTestServices(s => s.AddSingleton<IProductRepository>(ProductStore))
                .UseStartup<Startup>();
        }


        //Tear-down
        public void Dispose()
        {
            TestServer?.Dispose();
        }
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
