using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.ORM.Context;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

public class ProductRepository(SqlContext context):SqlBaseRepository<Product>(context), IProductRepository
{
}
