using Catalog.Api.Entities;
using Catalog.Api.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Catalog.Api.Controllers {
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CotalogController : ControllerBase {
        #region constractor
        private readonly IProductsRepository productsRepository;
        private readonly ILogger<CotalogController> logger;
        public CotalogController(IProductsRepository productsRepository, ILogger<CotalogController> logger) {
            this.productsRepository = productsRepository;
            this.logger = logger;
        }
        #endregion
        #region GetProducts
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts() => Ok(await productsRepository.GetProducts());
        #endregion
        #region Get Product By Id
        [HttpGet("{id:length(24)}", Name = "GetProduct")]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]

        public async Task<ActionResult<IEnumerable<Product>>> GetProducts(string id) {
            var products = await productsRepository.GetProduct(id);
            if (products == null) {
                logger.LogError($"product with id {id} is not found");
                return NotFound();
            }
            return Ok(products);
        }
        #endregion
        #region Get Product By Name
        [HttpGet("[action]/{category}")]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsByCategory(string category) => Ok(await productsRepository.GetProductsByCategory(category));
        #endregion
        #region create Product
        [HttpPost]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> CreateProducts([FromBody] Product product) {
            await productsRepository.CreateProduct(product);
            return CreatedAtRoute("GetProduct", new { id = product.Id }, product);
        }
        #endregion
        #region update product
        [HttpPut]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateProduct([FromBody] Product product)=>Ok(await productsRepository.UpdateProduct(product));
        #endregion
        #region delete product
        [HttpDelete("{id:length(24)}",Name ="DeleteProduct")]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteProduct(string id) => Ok(await productsRepository.DeleteProduct(id));
        #endregion
    }
}
