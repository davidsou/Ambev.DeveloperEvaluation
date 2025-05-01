using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.ORM.Context;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

public class BranchRepository(SqlContext context) : SqlBaseRepository<Branch>(context), IBranchRepository
{
}
