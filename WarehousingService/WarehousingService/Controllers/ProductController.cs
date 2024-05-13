using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WarehousingService.Data;
using WarehousingService.Domain;

namespace WarehousingService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductPayload payload)
        {
            try
            {
                if (_productRepository.Exist(payload.Name))
                {
                    return StatusCode(409);
                }

                await _productRepository.Create(new Product(payload.Name, payload.Price));
            }
            catch (ApplicationException)
            {
                return StatusCode(400);
            }

            return StatusCode(201);
        }
    }


}