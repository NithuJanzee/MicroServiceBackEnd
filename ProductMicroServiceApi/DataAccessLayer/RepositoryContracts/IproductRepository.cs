using System.Linq.Expressions;
using DataAccessLayer.Entities;

namespace DataAccessLayer.RepositoryContracts;
/// <summary>
//Represents a repository for managing 'Products' Table
/// </summary>
public interface IproductRepository
{
    /// <summary>
    /// Return All Products From the table 
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<Products>> GetProducts();
    Task<Products?> GetProductByCondition(Expression<Func<Products, bool>> connditionExpression);
    Task<IEnumerable<Products?>> GetProductsByCondition(Expression<Func<Products, bool>> connditionExpression);
    Task<Products?> AddProduct(Products product);
    Task<Products?> UpdateProduct(Products product);
    Task<bool> DeleteProduct(Guid ProductID);

}

