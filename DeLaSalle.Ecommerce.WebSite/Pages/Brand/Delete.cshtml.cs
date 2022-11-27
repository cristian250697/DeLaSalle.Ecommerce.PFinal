using DeLaSalle.Ecommerce.Core.Dto;
using DeLaSalle.Ecommerce.Core.Http;
using DeLaSalle.Ecommerce.WebSite.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DeLaSalle.Ecommerce.WebSite.Pages.Brand;

public class Delete : PageModel
{
    [BindProperty] public BrandDto BrandDto { get; set; }
    private readonly IBrandService _service;
    private bool deleteStatus = false;

    public Delete(IBrandService service)
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
        
        Response<bool> response;
        
        // Delete
        response = await _service.DeleteAsync(BrandDto.Id);
        deleteStatus = response.Data;
        
        if (deleteStatus)
        {
            return RedirectToPage("./List");
        }

        return Page();

    }
}