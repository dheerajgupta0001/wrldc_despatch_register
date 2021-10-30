using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Core.Entities;
using Infra.Persistence;
using Microsoft.Extensions.Logging;
using MediatR;
using Application.Despatches.Commands.EditDespatch;
using Application.Despatches.Queries.GetDespatchById;
using AutoMapper;
using WebApp.Extensions;
using Application.Despatches;

namespace WebApp.Pages.Despatches
{
    public class EditModel : PageModel
    {
        private readonly ILogger<EditModel> _logger;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public EditModel(ILogger<EditModel> logger,
                         IMediator mediator, IMapper mapper)
        {
            _logger = logger;
            _mediator = mediator;
            _mapper = mapper;
        }

        [BindProperty]
        public EditDespatchCommand Despatch { get; set; }
        public SelectList IndentingDeptOptions { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            Despatch despatch = await _mediator.Send(new GetDespatchByIdQuery() { Id = id.Value });

            if (despatch == null)
            {
                return NotFound();
            }

            InitSelectListItems(despatch);

            Despatch = _mapper.Map<EditDespatchCommand>(despatch);

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            Despatch despatch = await _mediator.Send(new GetDespatchByIdQuery() { Id = Despatch.Id });

            if (despatch == null)
            {
                return NotFound();
            }

            InitSelectListItems(despatch);

            if (!ModelState.IsValid)
            {
                return Page();
            }


            List<string> errors = await _mediator.Send(Despatch);

            foreach (var error in errors)
            {
                ModelState.AddModelError(string.Empty, error);
            }

            // check if we have any errors and redirect if successful
            if (errors.Count == 0)
            {
                _logger.LogInformation("Despatch edit operation successful");
                return RedirectToPage($"./{nameof(Index)}").WithSuccess("Despatch Editing done");
            }

            return Page();
        }

        public void InitSelectListItems(Despatch despatch)
        {
            IndentingDeptOptions = new SelectList(IndentingDeptConstants.GetIndentingDeptOptions());
        }
    }
}
