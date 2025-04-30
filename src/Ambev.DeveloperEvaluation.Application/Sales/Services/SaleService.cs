using Ambev.DeveloperEvaluation.Application.Sales.Dtos;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.ORM.Context;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.Application.Sales.Services;

public class SaleService(SqlContext context, IMapper mapper):ISaleService
{

    public async Task<OperationResult<SaleResponse>> CreateSaleAsync(SaleCreateRequest request)
    {
        try
        {
            var customer = await context.Users.FindAsync(request.CustomerId);
            var branch = await context.Branches.FindAsync(request.BranchId);

            if (customer == null)
                return OperationResult<SaleResponse>.Failure("Client not found.");

            if (branch == null)
                return OperationResult<SaleResponse>.Failure("Branch not found.");

            var sale = new Sale
            {
                Id = Guid.NewGuid(),
                SaleNumber = Guid.NewGuid().ToString().Substring(0, 8).ToUpper(),
                SaleDate = DateTime.UtcNow,
                CustomerId = request.CustomerId,
                BranchId = request.BranchId,
                TotalAmount = 0,
                Items = new List<SaleItem>()
            };

            foreach (var itemRequest in request.Items)
            {
                var product = await context.Products.FindAsync(itemRequest.ProductId);
                if (product == null)
                    return OperationResult<SaleResponse>.Failure($"Product {itemRequest.ProductId} not found.");

                var saleItem = new SaleItem
                {
                    Id = Guid.NewGuid(),
                    ProductId = itemRequest.ProductId,
                    Quantity = itemRequest.Quantity,
                    UnitPrice = itemRequest.UnitPrice,
                    Discount = itemRequest.Discount
                };

                sale.TotalAmount += saleItem.Total;
                sale.Items.Add(saleItem);
            }

            context.Sales.Add(sale);
            await context.SaveChangesAsync();

            var response = mapper.Map<SaleResponse>(sale);
            return OperationResult<SaleResponse>.Success(response, "Sales recorded with success.");
        }
        catch (Exception ex)
        {
            return OperationResult<SaleResponse>.Failure("Error during record sale", ex.Message);
        }
    }

    public async Task<OperationResult<IEnumerable<SaleResponse>>> GetAllSalesAsync()
    {
        try
        {
            var sales = await context.Sales
                .Include(s => s.Customer)
                .Include(s => s.Branch)
                .Include(s => s.Items)
                    .ThenInclude(i => i.Product)
                .ToListAsync();

            var result = mapper.Map<IEnumerable<SaleResponse>>(sales);
            return OperationResult<IEnumerable<SaleResponse>>.Success(result);
        }
        catch (Exception ex)
        {
            return OperationResult<IEnumerable<SaleResponse>>.Failure("Erro ao listar vendas.", ex.Message);
        }
    }

    public async Task<OperationResult<SaleResponse>> GetSaleByIdAsync(Guid id)
    {
        try
        {
            var sale = await context.Sales
                .Include(s => s.Customer)
                .Include(s => s.Branch)
                .Include(s => s.Items)
                    .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (sale == null)
                return OperationResult<SaleResponse>.Failure("Venda não encontrada.");

            var result = mapper.Map<SaleResponse>(sale);
            return OperationResult<SaleResponse>.Success(result);
        }
        catch (Exception ex)
        {
            return OperationResult<SaleResponse>.Failure("Erro ao buscar venda.", ex.Message);
        }
    }

    public async Task<OperationResult> CancelSaleAsync(Guid id)
    {
        try
        {
            var sale = await context.Sales.FindAsync(id);
            if (sale == null)
                return OperationResult.Failure("Venda não encontrada.");

            sale.IsCancelled = true;
            await context.SaveChangesAsync();

            return OperationResult.Success("Venda cancelada com sucesso.");
        }
        catch (Exception ex)
        {
            return OperationResult.Failure("Erro ao cancelar venda.", ex.Message);
        }
    }
}

