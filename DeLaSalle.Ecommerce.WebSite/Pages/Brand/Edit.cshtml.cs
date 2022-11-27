using DeLaSalle.Ecommerce.Core.Dto;
using DeLaSalle.Ecommerce.Core.Http;
using DeLaSalle.Ecommerce.WebSite.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DeLaSalle.Ecommerce.WebSite.Pages.Brand;

public class Edit : PageModel
{
    
    [BindProperty] public BrandDto BrandDto { get; set; }
    public List<string> Errors { get; set; } = new List<string>();
    private readonly IBrandService _service;

    public Edit(IBrandService service)
    {
        BrandDto = new BrandDto();
        _service = service;
    }

    public async Task<ActionResult> OnGet(int? id)
    {
        BrandDto = new BrandDto();
        
        if (id.HasValue)
        {
            // Get information from the service (API)
            var response = await _service.GetById(id.Value);
            
            BrandDto = response.Data;

            if (BrandDto == null)
            {
                return RedirectToPage("/Error");
            }
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        Response<BrandDto> response;

        if (BrandDto.Id > 0)
        {
            // Update
            response = await _service.UpdateAsync(BrandDto);
            
        }
        else
        {
            // Insert
            response = await _service.SaveAsync(BrandDto);
        }

        
        Errors = response.Errors;

        if (Errors.Count > 0)
        {
            return Page();
        }
        
        BrandDto = response.Data;
        return RedirectToPage("./List");
    }
}