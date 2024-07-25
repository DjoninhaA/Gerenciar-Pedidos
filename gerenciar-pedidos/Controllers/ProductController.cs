
using gerenciar_pedidos.Models;
using Microsoft.AspNetCore.Mvc;

namespace gerenciar_pedidos.Controllers
{
    [ApiController]
    [Route("api/products")]

    public class ProductController : ControllerBase
    {

        private readonly IProductRepository _productRepository;


        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        ///////////////////////////////////////
        

        [HttpGet]
        public async Task<IActionResult> Get(){
            var products = await _productRepository.GetAllProduct();
            return Ok(products);
        }


        [HttpPost("create")]
        public async Task<IActionResult> Post([FromBody] Product product){
            if (product == null){
                return BadRequest("Produto não pode ser nulo.");
            }

            try{
                var createdProduct = await _productRepository.Create(product);
                return CreatedAtAction(nameof(Get), new { id = createdProduct.ProductId }, createdProduct);
            }
            catch(Exception){
                return StatusCode(500, "Erro na Criação do Produto");
            }
        }



    }
    
}