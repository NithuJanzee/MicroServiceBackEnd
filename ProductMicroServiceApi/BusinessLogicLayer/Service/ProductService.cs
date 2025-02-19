

using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using AutoMapper;
using BusinessLogicLayer.DTO;
using BusinessLogicLayer.ServiceContracts;
using DataAccessLayer.Entities;
using DataAccessLayer.RepositoryContracts;
using FluentValidation;

namespace BusinessLogicLayer.Service;

public class ProductService : IproductService
{
    private readonly IValidator<ProductAddRequest> _AddRequestValidator;
    private readonly IValidator<ProductUpdateRequest> _UpdateRequestValidator;
    private readonly IMapper _Mapper;
    private readonly IproductRepository _Repository;

    public ProductService(IValidator<ProductAddRequest> addRequestValidator,
        IValidator<ProductUpdateRequest> updateRequestValidator,
        IMapper mapper, IproductRepository repository)
    {
        _AddRequestValidator = addRequestValidator;
        _UpdateRequestValidator = updateRequestValidator;
        _Mapper = mapper;
        _Repository = repository;
    }

    public async Task<ProductResponse?> AddProduct(ProductAddRequest product)
    {
        if (product == null) throw new ArgumentNullException(nameof(product));

        //validate the product Using FluentValidation
        FluentValidation.Results.ValidationResult validationResult = _AddRequestValidator.Validate(product);
        //Check Validation Result
        if (!validationResult.IsValid)
        {
            string errors = string.Join(",", validationResult.Errors.Select(temp => temp.ErrorMessage));
            throw new ArgumentException(errors);
        }

        Products ProductMap = _Mapper.Map<Products>(product);
        Products? AddProducts = await _Repository.AddProduct(ProductMap);

        if(AddProducts == null) return null;
        ProductResponse response = _Mapper.Map<ProductResponse>(AddProducts);
        return response;
    }

    public async Task<bool> DeleteProduct(Guid ProductID)
    {
        throw new NotImplementedException();
    }

    public async Task<ProductResponse?> GetProductByCondition(Expression<Func<Products, bool>> connditionExpression)
    {
        throw new NotImplementedException();
    }

    public async Task<List<ProductResponse>> GetProducts()
    {
        throw new NotImplementedException();
    }

    public async Task<List<ProductResponse?>> GetProductsByCondition(Expression<Func<Products, bool>> connditionExpression)
    {
        throw new NotImplementedException();
    }

    public async Task<ProductResponse?> UpdateProduct(ProductUpdateRequest product)
    {
        throw new NotImplementedException();
    }
}
