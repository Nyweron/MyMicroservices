using Catalog.API.Entities;
using Catalog.API.Repositories.interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;


namespace Catalog.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger _logger;

        public CatalogController(IProductRepository productRepository, ILogger<CatalogController> logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return Ok(await _productRepository.GetProducts());
        }

        [HttpGet("[action]/{id:length(24)}", Name = "GetProduct") ]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> GetProduct(string id)
        {
            return Ok(await _productRepository.GetProduct(id));
        }

        [HttpGet("[action]/{productName}")]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> GetProductByName(string productName)
        {
            return Ok(await _productRepository.GetProductByName(productName));
        }

        [HttpGet("[action]/{category}")]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> GetProductByCategory(string category)
        {
            return Ok(await _productRepository.GetProductByCategory(category));
        }

        [HttpPost()]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> Create([FromBody] Product product)
        {
            await _productRepository.Create(product);
            
            return CreatedAtRoute("GetProduct", new { id = product.Id }, product);
        }



    }
}
