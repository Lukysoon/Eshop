using AutoMapper;
using ProductManager.Entities;
using ProductManager.Models.Dto;

namespace ProductManager.Mapping;

public class ProductProfile: Profile
{
	public ProductProfile()
	{
		CreateMap<Product, ProductDto>();
	}
}
