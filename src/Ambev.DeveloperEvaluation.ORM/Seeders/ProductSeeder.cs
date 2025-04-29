using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.ORM.Context;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.ORM.Seeders;

public static class ProductSeeder
{
    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<SqlContext>(); // ajuste aqui o seu DbContext real
        var logger = scope.ServiceProvider.GetRequiredService<ILoggerFactory>().CreateLogger("ProductSeeder");

        if (context.Products.Any())
        {
            logger.LogInformation("Products already exist. Skipping seeding.");
            await ProductRatingSeeder.SeedProductRatingsAsync(context, logger);
            return;
        }

        logger.LogInformation("Seeding initial Products...");

        var products = new List<Product>
{
    // Bebidas
    new() {
        Title = "Cerveja Skol",
        Description = "Latão 473ml",
        Price = 4.50m,
        Category = "Bebidas",
        Image = "https://example.com/skol.jpg"
    },
    new() {
        Title = "Cerveja Brahma",
        Description = "Garrafa 600ml",
        Price = 6.00m,
        Category = "Bebidas",
        Image = "https://example.com/brahma.jpg"
    },
    new() {
        Title = "Refrigerante Guaraná Antarctica",
        Description = "Pet 2L",
        Price = 8.00m,
        Category = "Bebidas",
        Image = "https://example.com/guarana.jpg"
    },
    new() {
        Title = "Água Mineral sem Gás",
        Description = "Garrafa 500ml",
        Price = 2.00m,
        Category = "Bebidas",
        Image = "https://example.com/agua.jpg"
    },

    // Alimentos
    new() {
        Title = "Arroz Tipo 1",
        Description = "Pacote 5kg",
        Price = 18.90m,
        Category = "Alimentos",
        Image = "https://example.com/arroz.jpg"
    },
    new() {
        Title = "Feijão Carioca",
        Description = "Pacote 1kg",
        Price = 7.50m,
        Category = "Alimentos",
        Image = "https://example.com/feijao.jpg"
    },
    new() {
        Title = "Macarrão Espaguete",
        Description = "Pacote 500g",
        Price = 3.80m,
        Category = "Alimentos",
        Image = "https://example.com/macarrao.jpg"
    },

    // Eletrônicos
    new() {
        Title = "Smartphone Android",
        Description = "64GB, Tela 6.5''",
        Price = 1299.99m,
        Category = "Eletrônicos",
        Image = "https://example.com/smartphone.jpg"
    },
    new() {
        Title = "Notebook Gamer",
        Description = "16GB RAM, RTX 3060",
        Price = 7499.00m,
        Category = "Eletrônicos",
        Image = "https://example.com/notebook.jpg"
    },
    new() {
        Title = "Fone de Ouvido Bluetooth",
        Description = "Noise Cancelling",
        Price = 499.90m,
        Category = "Eletrônicos",
        Image = "https://example.com/fone.jpg"
    },

    // Moda
    new() {
        Title = "Camiseta Básica",
        Description = "100% Algodão",
        Price = 39.90m,
        Category = "Moda",
        Image = "https://example.com/camiseta.jpg"
    },
    new() {
        Title = "Tênis Esportivo",
        Description = "Corrida/Academia",
        Price = 249.90m,
        Category = "Moda",
        Image = "https://example.com/tenis.jpg"
    },

    // Casa e Cozinha
    new() {
        Title = "Liquidificador",
        Description = "700W, 12 Velocidades",
        Price = 159.90m,
        Category = "Casa e Cozinha",
        Image = "https://example.com/liquidificador.jpg"
    },
    new() {
        Title = "Conjunto de Panelas",
        Description = "5 peças, Antiaderente",
        Price = 399.90m,
        Category = "Casa e Cozinha",
        Image = "https://example.com/panelas.jpg"
    },

    // Esportes
    new() {
        Title = "Bola de Futebol",
        Description = "Tamanho Oficial",
        Price = 89.90m,
        Category = "Esportes",
        Image = "https://example.com/bola.jpg"
    },
    new() {
        Title = "Halteres 10kg",
        Description = "Par de Halteres",
        Price = 120.00m,
        Category = "Esportes",
        Image = "https://example.com/halteres.jpg"
    },

    // Brinquedos
    new() {
        Title = "Carrinho Controle Remoto",
        Description = "Velocidade alta, bateria recarregável",
        Price = 299.90m,
        Category = "Brinquedos",
        Image = "https://example.com/carrinho.jpg"
    },
    new() {
        Title = "Boneca Baby Alive",
        Description = "Faz sons e bebe água",
        Price = 199.90m,
        Category = "Brinquedos",
        Image = "https://example.com/boneca.jpg"
    }
};


        context.Products.AddRange(products);
        await context.SaveChangesAsync();
        await ProductRatingSeeder.SeedProductRatingsAsync(context, logger);



        logger.LogInformation("Products seeded successfully.");
    }
}
