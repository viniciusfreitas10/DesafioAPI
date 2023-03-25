using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Produtos.Persistence;
using Produtos.Domain;
using Produtos.Persistence.Context;
using Produtos.Application.Contratos;
using Microsoft.AspNetCore.Http;
using static System.String;

namespace Produtos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        public readonly IProductService _productService;
        Logger logger = new Logger();

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            try
            {
                logger.Log("GetProducts", "Recuperando produtos", "Info");

                var products = await _productService.GetAllProductAsync();
                if (products == null) return NotFound("Nenhum produto encontrado");

                return Ok(products);
            }
            catch (Exception e)
            {
                logger.Log("GetProducts", e.Message, "Error");
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar os produtos . Erro: {e.Message}");
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddProduct(Produto model)
        {
            try
            {
                logger.Log("AddProduct", $"adicionando produto: ID: {model.Id}", "Info");

                if (IsNullOrEmpty(model.Description))
                    return BadRequest("O campo descrição do produto deve ser preenchido");

                if (DateValidation(model.DataFabricacao, model.DataValidade))
                    return BadRequest($"Erro ao adicionar o produto - A data de fabricação não pode ser maior ou igual a data de validade");

                //Sempre salvar com letra maiúscula
                model.EstReg = Convert.ToChar(Convert.ToString(model.EstReg).ToUpper());

                var product = await _productService.AddProduct(model);
                if (product == null) return BadRequest($"Erro ao atualizar o produto: {product.Id}");

                return Ok(product);
            }
            catch (Exception e)
            {
                logger.Log("AddProduct", e.Message, "Error");
                return this.StatusCode(StatusCodes.Status400BadRequest,
                    $"Erro ao tentar adicionar o produto. Erro: {e.Message}");
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> AttProduct(int id, Produto model)
        {
            try
            {
                logger.Log("AttProduct", $"atualizando produto: ID: {model.Id}", "Info");

                if (IsNullOrEmpty(model.Description))
                    return BadRequest("O campo descrição do produto deve ser preenchido");

                if (DateValidation(model.DataFabricacao, model.DataValidade))
                    return BadRequest($"Erro ao atualizar o produto: {model.Id} - A data de fabricação não pode ser maior ou igual a data de validade");

                //Sempre salvar com letra maiúscula
                model.EstReg = Convert.ToChar(Convert.ToString(model.EstReg).ToUpper());
                var product = await _productService.UpdateProduct(id, model);

                if (product == null)
                    return BadRequest($"Erro ao atualizar o produto: {product.Id}");

                return Ok(product);
            }
            catch (Exception e)
            {
                logger.Log("AttProduct", e.Message, "Error");
                return this.StatusCode(StatusCodes.Status400BadRequest,
                    $"Erro ao tentar atualizar os produtos. Erro: {e.Message}");
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id, Produto model)
        {
            try
            {
                logger.Log("DeleteProduct", $"Deletando o produto - ID: {model.Id} ", "Info");

                return await _productService.DeleteProduct(id) ? Ok("Deletado") : BadRequest($"Erro ao deletar o produto: {id}");
            }
            catch (Exception e)
            {
                logger.Log("DeleteProduct", e.Message, "Error");
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar deletar o produto. Erro: {e.Message}");
            }
        }
        [HttpPost("{id}/historic")]
        public async Task<IActionResult> HistoricProductApi(int id)
        {
            try
            {
                logger.Log("HistoricProduct", $"Historizando o produto: {id}", "Info");

                var product = await _productService.HistoricProduct(id);

                if (product == null) return BadRequest($"Erro ao historizar o produto: {product.Id}");

                return Ok(product);

            }
            catch (Exception e)
            {
                logger.Log("HistoricProduct", e.Message, "Error");

                return this.StatusCode(StatusCodes.Status400BadRequest,
                    $"Erro ao tentar historizar o produto. Erro: {e.Message}");
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            try
            {
                logger.Log("GetProductById", $"Recuperando o produto com o id: {id}", "Info");

                var product = await _productService.GetProductById(id);

                if (product == null) return NotFound("Nenhum produto encontrado com esse Id");

                return Ok(product);
            }
            catch (Exception e)
            {
                logger.Log("GetProductById", e.Message, "Error");
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar produto com Id: {id} - Erro: {e.Message}");
            }
        }
        
        private bool DateValidation(DateTime dateManufacture, DateTime datevalidity)
        {
            return dateManufacture >= datevalidity ? true : false;
        }
        
    }
}
