using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Core.Entities;
using Infra.Persistence;

namespace WebApp.Pages.Despatches
{
    public class DetailsModel : PageModel
    {
        private readonly Infra.Persistence.AppDbContext _context;

        public DetailsModel(Infra.Persistence.AppDbContext context)
        {
            _context = context;
        }
        public Despatch Despatch { get; set; }
        public string Date { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // create child entities for each proposal options

            Despatch = await _context.Despatches.Where(m => m.Id == id)
                                        .FirstOrDefaultAsync();
            
            Date = Despatch.Created.ToString("dd-MM-yyyy");

            if (Despatch == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
