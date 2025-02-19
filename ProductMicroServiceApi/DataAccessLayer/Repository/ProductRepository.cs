using System.Linq.Expressions;
using DataAccessLayer.Entities;
using DataAccessLayer.RepositoryContracts;
using eCommerce.DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repository;

public class ProductRepository : IproductRepository
{
    private readonly ApplicationDbContext _context;

    public ProductRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Products?> AddProduct(Products product)
    {
        _context.products.Add(product);
        await _context.SaveChangesAsync();
        return product;
    }

    public async Task<bool> DeleteProduct(Guid ProductID)
    {
        Products? ExitingProducts = await _context.products.FirstOrDefaultAsync(temp => temp.ProductID == ProductID);
        if (ExitingProducts == null) return false;
        _context.products.Remove(ExitingProducts);
        int affectedRow = await _context.SaveChangesAsync();
        return affectedRow > 0;
    }

    public async Task<Products?> GetProductByCondition(Expression<Func<Products, bool>> connditionExpression)
    {
        return await _context.products.FirstOrDefaultAsync(connditionExpression);
    }

    public async Task<IEnumerable<Products>> GetProducts()
    {
        return await _context.products.ToListAsync();
    }

    public async Task<IEnumerable<Products?>> GetProductsByCondition(Expression<Func<Products, bool>> connditionExpression)
    {
        return await _context.products.Where(connditionExpression).ToListAsync();
    }

    public async Task<Products?> UpdateProduct(Products product)
    {
        Products? exitingProducts = await _context.products.FirstOrDefaultAsync(x => x.ProductID == product.ProductID);
        if (exitingProducts == null) return null;
        exitingProducts.ProductName = product.ProductName;
        exitingProducts.Category = product.Category;
        exitingProducts.UnitPrice = product.UnitPrice;
        exitingProducts.QuantityInStock = product.QuantityInStock;
        await _context.SaveChangesAsync();
        return product;
    }
}

