using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.ORM.Context;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

public class SaleItemRepository(SqlContext context):SqlBaseRepository<SaleItem>(context),ISaleItemRepository
{
}
