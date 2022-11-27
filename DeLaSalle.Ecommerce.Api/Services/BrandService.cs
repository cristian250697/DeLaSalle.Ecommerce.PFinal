using DeLaSalle.Ecommerce.Api.Repositories.Interfaces;
using DeLaSalle.Ecommerce.Api.Services.Interfaces;
using DeLaSalle.Ecommerce.Core.Dto;
using DeLaSalle.Ecommerce.Core.Entities;

namespace DeLaSalle.Ecommerce.Api.Services;

public class BrandService : IBrandService
{

    private readonly IBrandRepository _brandRepository;

    public BrandService(IBrandRepository repository)
    {
        _brandRepository = repository;
    }
    
    public async Task<bool> BrandExist(int id)
    {
        var brand = await _brandRepository.GetById(id);
        return (brand != null);
    }
        
    public async Task<BrandDto> SaveAsync(BrandDto brandDto)
    {
        var newBrand = new Brand
        {
            Name = brandDto.Name,
            Description = brandDto.Description,
            CreatedBy = "",
            CreatedDate = DateTime.Now,
            UpdatedBy = "",
            UpdatedDate = DateTime.Now
        };

        newBrand = await _brandRepository.SaveAsync(newBrand);

        return new BrandDto(newBrand);
    }

    public async Task<BrandDto> UpdateAsync(BrandDto brandDto)
    {
        var brandDB = await _brandRepository.GetById(brandDto.Id);

        if (brandDB == null)
            throw new Exception("Brand not found");
        
        brandDB.Name = brandDto.Name;
        brandDB.Description = brandDto.Description;
        brandDB.UpdatedBy = "";
        brandDB.UpdatedDate = DateTime.Now;
        
        await _brandRepository.UpdateAsync(brandDB);

        return brandDto;
    }

    public async Task<List<BrandDto>> GetAllAsync()
    {
        var brands = await _brandRepository.GetAllAsync();
        var brandsDto = brands.Select(c => new BrandDto(c)).ToList();

        return brandsDto;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var brand = await _brandRepository.GetById(id);
        
        if(brand == null)
            throw new Exception("Brand not found");
        
        return await _brandRepository.DeleteAsync(id);
    }

    public async Task<BrandDto> GetById(int id)
    {
        var brand = await _brandRepository.GetById(id);
        
        if(brand == null)
            throw new Exception("Brand not found");

        var brandDto = new BrandDto(brand);
           
        return brandDto;
    }
}