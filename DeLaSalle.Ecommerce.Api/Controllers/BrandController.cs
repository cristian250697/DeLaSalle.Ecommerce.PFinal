using DeLaSalle.Ecommerce.Api.Repositories.Interfaces;
using DeLaSalle.Ecommerce.Api.Services.Interfaces;
using DeLaSalle.Ecommerce.Core.Dto;
using DeLaSalle.Ecommerce.Core.Entities;
using DeLaSalle.Ecommerce.Core.Http;
using Microsoft.AspNetCore.Mvc;

namespace DeLaSalle.Ecommerce.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BrandController : Controller
{
    private readonly IBrandRepository _brandRepository;
    private readonly IBrandService _brandService;

    public BrandController(IBrandService service)
    {
        _brandService = service;
    }
    
    [HttpGet]
    public async Task<ActionResult<Response<List<BrandDto>>>> GetAll()
    {
        var response = new Response<List<BrandDto>>
        {
            Data = await _brandService.GetAllAsync()
        };

        return Ok(response);
    }
    
    [HttpPost]
    public async Task<ActionResult<Response<BrandDto>>> Post([FromBody] BrandDto brandDto)
    {
        var response = new Response<BrandDto>
        {
            Data = await _brandService.SaveAsync(brandDto)
        };
        
        return Created($"/api/[controller]/{response.Data.Id}",response);
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<ActionResult<Response<BrandDto>>> GetById( int id )
    {

        var response = new Response<BrandDto>();

        bool exist = await _brandService.BrandExist(id);

        if (!exist)
        {
            response.Errors.Add("Brand not found");
            return NotFound(response);
        }

        response.Data = await _brandService.GetById(id);
        return Ok(response);

    }

    [HttpPut]
    public async Task<ActionResult<Response<BrandDto>>> Update([FromBody] BrandDto brandDto)
    {
        var response = new Response<BrandDto>();
        bool exist = await _brandService.BrandExist(brandDto.Id);

        if (!exist)
        {
            response.Errors.Add("Brand not found");
            return NotFound(response);
        }

        response.Data = await _brandService.UpdateAsync(brandDto);
        return Ok(response);
        
    }

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<ActionResult<Response<bool>>> Delete(int id)
    {
        var response = new Response<bool>();
        bool exist = await _brandService.BrandExist(id);

        if (!exist)
        {
            response.Errors.Add("Brand not found");
            return NotFound(response);
        }

        var result = await _brandService.DeleteAsync(id);
        response.Data = result;

        return Ok(response);
    }
    
    
}