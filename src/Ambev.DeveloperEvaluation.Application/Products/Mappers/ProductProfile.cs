using Ambev.DeveloperEvaluation.Application.Products.Dtos;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Products.Mappers;

public class ProductProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for CreateProduct operation
    /// </summary>
    public ProductProfile()
    {
        CreateMap<CreateProductDto, Product>();
        CreateMap<Product, CreateProductResultDto>();

        CreateMap<Product, ProductDto>()
            .ForMember(dest => dest.AverageRating, opt => opt.MapFrom(src => src.AverageRating))
            .ForMember(dest => dest.TotalRatings, opt => opt.MapFrom(src => src.TotalRatings));

        CreateMap<ProductRating, ProductRatingDto>();

        CreateMap<Product, ProductDetailDto>()
            .ForMember(dest => dest.AverageRating, opt => opt.MapFrom(src => src.AverageRating))
            .ForMember(dest => dest.TotalRatings, opt => opt.MapFrom(src => src.TotalRatings))
            .ForMember(dest => dest.Ratings, opt => opt.MapFrom(src => src.Ratings));
    }
}
