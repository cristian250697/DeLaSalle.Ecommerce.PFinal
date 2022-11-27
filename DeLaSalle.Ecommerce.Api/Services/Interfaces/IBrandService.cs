using DeLaSalle.Ecommerce.Core.Dto;
using DeLaSalle.Ecommerce.Core.Entities;

namespace DeLaSalle.Ecommerce.Api.Services.Interfaces;

public interface IBrandService
{
    Task<bool> BrandExist(int id);
    Task<BrandDto> SaveAsync(BrandDto brand);
    Task<BrandDto> UpdateAsync(BrandDto brand);
    Task<List<BrandDto>> GetAllAsync();
    Task<bool> DeleteAsync(int id);
    Task<BrandDto> GetById(int id);
}