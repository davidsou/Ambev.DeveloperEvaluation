using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.ORM.Context;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

public class ProductRepository(SqlContext context):SqlBaseRepository<Product>(context), IProductRepository
{
    public async Task<IEnumerable<Product>> GetAllWithRatingsAsync()
    {
        return await context.Products
            .Where(p => p.Active)
            .Include(p => p.Ratings)
            .ToListAsync();
    }
}
