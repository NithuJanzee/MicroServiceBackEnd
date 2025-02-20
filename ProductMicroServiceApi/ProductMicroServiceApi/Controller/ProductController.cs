using System.ComponentModel.DataAnnotations;
using AutoMapper;
using BusinessLogicLayer.DTO;
using BusinessLogicLayer.ServiceContracts;
using BusinessLogicLayer.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI.Common;

namespace ProductMicroServiceApi.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IproductService _ProductService;
        private readonly IValidator<ProductAddRequest> _AddRequestValidator;
        private readonly IValidator<ProductUpdateRequest> _UpdateValidator;
        private readonly IMapper _Mapper;

        public ProductController(IproductService productService,
             IValidator<ProductAddRequest> addRequestValidator,
             IValidator<ProductUpdateRequest> updateValidator,
             IMapper mapper)
        {
            _ProductService = productService;
            _AddRequestValidator = addRequestValidator;
            _UpdateValidator = updateValidator;
            _Mapper = mapper;
        }

        //Add Products
        [HttpPost("AddProducts")]
        public async Task<IActionResult> AddProducts(ProductAddRequest product)
        {
            FluentValidation.Results.ValidationResult validationResult = _AddRequestValidator.Validate(product);
            if (!validationResult.IsValid)
            {
                Dictionary<string, string[]> errors = validationResult.Errors
                  .GroupBy(temp => temp.PropertyName)
                  .ToDictionary(grp => grp.Key,
                    grp => grp.Select(err => err.ErrorMessage).ToArray());
                var problemDetails = new ValidationProblemDetails(errors);
                return ValidationProblem(problemDetails);
            }

            var AddProductResponse = await _ProductService.AddProduct(product);
            if (AddProductResponse != null) return Ok(AddProductResponse);
            return BadRequest(new { Message = "Failed to Add Product" });
        }

        //Delete Products
        [HttpDelete("DeleteProduct")]
        public async Task<IActionResult> DeleteProduct(Guid ProductID)
        {
            bool IsDeleted = await _ProductService.DeleteProduct(ProductID);
            if (IsDeleted) return Ok(IsDeleted);
            return BadRequest(new { Message = "Failed to Delete Product" });
        }
        //Get Products By conditon
        [HttpGet("GetProductByCondition")]
        public async Task<IActionResult> GetProductByCondition(Guid ProdutID)
        {
            ProductResponse? Response = await _ProductService.GetProductByCondition(temp => temp.ProductID == ProdutID);
            return Ok(Response);
        }

        //Get Products
        [HttpGet("GetAllProducts")]
        public async Task<IActionResult> GetAllProducts()
        {
            List<ProductResponse> products = await _ProductService.GetProducts();
            return Ok(products);
        }

        //Get Products By Condtion
        [HttpGet("GetProductsByCondition")]
        public async Task<IActionResult> GetProductsByCondition(string SearchString)
        {
            List<ProductResponse?> ProductsByName = await _ProductService.GetProductsByCondition(temp => temp.ProductName != null && temp.ProductName.Contains(SearchString, StringComparison.OrdinalIgnoreCase));
            List<ProductResponse?> ProductByCategory = await _ProductService.GetProductsByCondition(temp => temp.Category != null && temp.Category.Contains(SearchString, StringComparison.OrdinalIgnoreCase));
            var products = ProductsByName.Union(ProductByCategory);
            return Ok(products);
        }
        //Update Products
    }
}
