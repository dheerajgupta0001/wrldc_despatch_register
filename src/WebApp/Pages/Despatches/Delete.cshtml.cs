using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Core.Entities;
using Infra.Persistence;
using MediatR;
using Application.Despatches.Commands.DeleteDespatch;
using WebApp.Extensions;
using Microsoft.Extensions.Logging;
using Application.Despatches.Queries.GetDespatchById;

namespace WebApp.Pages.Despatches
{
    public class DeleteModel : PageModel
    {
        private readonly IMediator _mediator;
        private readonly ILogger<EditModel> _logger;

        public DeleteModel(IMediator mediator, ILogger<EditModel> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [BindProperty]
        public Despatch Despatch { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Despatch = await _mediator.Send(new GetDespatchByIdQuery() { Id = id.Value });

            if (Despatch == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            List<string> errors = await _mediator.Send(new DeleteDespatchCommand() { Id = id.Value });

            // check if we have any errors and redirect if successful
            if (errors.Count == 0)
            {
                _logger.LogInformation("Despatch delete operation successful");
                return RedirectToPage($"./{nameof(Index)}").WithSuccess("Despatch Deletion done");
            }

            return RedirectToPage($"./{nameof(Index)}").WithDanger("Unable to delete Despatch...");
        }
    }
}
