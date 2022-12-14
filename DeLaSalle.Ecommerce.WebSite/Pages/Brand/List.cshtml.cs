using DeLaSalle.Ecommerce.Core.Dto;
using DeLaSalle.Ecommerce.WebSite.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DeLaSalle.Ecommerce.WebSite.Pages.Brand;

public class ListModel : PageModel
{

    private readonly IBrandService _service;
    public List<BrandDto> Brands { get; set; }

    public ListModel(IBrandService service)
    {
        Brands = new List<BrandDto>();
        _service = service;
    }

    public async Task<IActionResult> OnGet()
    {
        // Llamada al servicio
        var response = await _service.GetAllAsync();

        Brands = response.Data;
        
        return Page();
    }
}