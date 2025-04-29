using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.ORM.Context;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.ORM.Seeders;

public static class ProductRatingSeeder
{
    public static async Task SeedProductRatingsAsync(SqlContext context, ILogger logger)
    {
        if (context.ProductRatings.Any())
        {
            logger.LogInformation("ProductRatings already exist. Skipping seeding.");
            return;
        }

        var allProducts = context.Products.ToList();
        if (allProducts.Count < 6)
        {
            logger.LogWarning("At least 6 products are required to seed ratings as specified.");
            return;
        }

        var random = new Random();
        var ratings = new List<ProductRating>();

        // Simulando alguns usuários fixos
        var userIds = Enumerable.Range(1, 10).Select(_ => Guid.NewGuid()).ToList();

        var commentsPool = new[]
        {
            "Excelente produto!",
            "Não gostei da qualidade.",
            "Entrega rápida e produto em ótimo estado.",
            "Produto veio com defeito.",
            "Muito bom pelo preço.",
            "Cumpre o que promete.",
            "Deixou a desejar.",
            "Superou minhas expectativas!",
            "Não compraria novamente.",
            "Recomendo!"
        };

        // Garantir produtos com rating 5.0
        foreach (var product in allProducts.Take(2))
        {
            for (int i = 0; i < 3; i++)
            {
                ratings.Add(new ProductRating
                {
                    ProductId = product.Id,
                    UserId = userIds[random.Next(userIds.Count)],
                    Rate = 5.0,
                    Comment = "Produto perfeito!",
                    CreatedAt = DateTime.UtcNow.AddDays(-random.Next(1, 30))
                });
            }
        }

        // Garantir produtos com rating 1.0
        foreach (var product in allProducts.Skip(2).Take(2))
        {
            for (int i = 0; i < 3; i++)
            {
                ratings.Add(new ProductRating
                {
                    ProductId = product.Id,
                    UserId = userIds[random.Next(userIds.Count)],
                    Rate = 1.0,
                    Comment = "Muito ruim, não recomendo.",
                    CreatedAt = DateTime.UtcNow.AddDays(-random.Next(1, 30))
                });
            }
        }

        // Pular 2 produtos para deixá-los sem rating
        var remainingProducts = allProducts.Skip(6).ToList();

        // Preencher os restantes com ratings variados
        foreach (var product in remainingProducts)
        {
            int numberOfRatings = random.Next(1, 5);
            for (int i = 0; i < numberOfRatings; i++)
            {
                ratings.Add(new ProductRating
                {
                    ProductId = product.Id,
                    UserId = userIds[random.Next(userIds.Count)],
                    Rate = Math.Round(random.NextDouble() * 4 + 1, 1), // entre 1.0 e 5.0
                    Comment = commentsPool[random.Next(commentsPool.Length)],
                    CreatedAt = DateTime.UtcNow.AddDays(-random.Next(1, 30))
                });
            }
        }

        context.ProductRatings.AddRange(ratings);
        await context.SaveChangesAsync();

        logger.LogInformation("ProductRatings seeded successfully.");
    }
}
