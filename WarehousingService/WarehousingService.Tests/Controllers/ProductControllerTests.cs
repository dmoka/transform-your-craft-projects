using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using FluentAssertions;
using MutationTestingMeetup.Tests;
using MutationTestingMeetup.Tests.Asserters;
using NUnit.Framework;
using WarehousingService.Controllers;
using WarehousingService.Domain;

namespace WarehousingService.Tests.Controllers
{
    public class ProductControllerTests
    {
        [Test]
        public async Task CreateEndpointShouldStoreProductInWarehouse()
        {
            //Arrange
            using var server = new InMemoryTestServer();

            var payload = JsonPayloadBuilder.Build(new ProductPayload()
            {
                Name = "Aqua WakeBoard",
                Price = 120.5m
            });

            //Act
            var response = await server.Client.PostAsync("/product", payload);

            //Assert
            await HttpResponseAsserter.AssertThat(response).HasStatusCode(HttpStatusCode.Created);
            var product = server.ProductStore.Products.SingleOrDefault();
            product.Should().NotBeNull();
            product.Id.Should().NotBeEmpty();
            product.Name.Value.Should().Be("Aqua WakeBoard");
            product.Price.Should().Be(120.5m);
        }


        [TestCase(null)]
        [TestCase("")]
        public async Task ShouldReturnBadRequest_whenProductNameIsNotSpecified(string productName)
        {
            //Arrange
            using var server = new InMemoryTestServer();

            var payload = JsonPayloadBuilder.Build(new ProductPayload()
            {
                Name = productName,
                Price = 120.5m
            });

            //Act
            var response = await server.Client.PostAsync("/product", payload);

            //Assert
            await HttpResponseAsserter.AssertThat(response).HasStatusCode(HttpStatusCode.BadRequest);
        }

        [Test]
        public async Task ShouldReturnConflict_whenProductWithNameAlreadyExists()
        {
            //Arrange
            using var server = new InMemoryTestServer();

            var productName = "Aqua WakeBoard";
            server.ProductStore.Products.Add(new Product(productName, 250.5m));

            var payload = JsonPayloadBuilder.Build(new ProductPayload()
            {
                Name = productName,
                Price = 120.5m
            });

            //Act
            var response = await server.Client.PostAsync("/product", payload);

            //Assert
            await HttpResponseAsserter.AssertThat(response)
                .HasStatusCode(HttpStatusCode.Conflict);
        }
    }
}