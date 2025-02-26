
using BusinessLogicLayer.DTO;
using DataAccessLayer.Entities;
using System.Linq.Expressions;

namespace BusinessLogicLayer.ServiceContracts;

public interface IproductService
{
    Task<List<Products>> GetProducts();
    Task<ProductResponse?> GetProductByCondition(Expression<Func<Products, bool>> connditionExpression);
    Task<List<ProductResponse?>> GetProductsByCondition(Expression<Func<Products, bool>> connditionExpression);
    Task<ProductResponse?> AddProduct(ProductAddRequest product);
    Task<ProductResponse?> UpdateProduct(ProductUpdateRequest product);
    Task<bool> DeleteProduct(Guid ProductID);
}

