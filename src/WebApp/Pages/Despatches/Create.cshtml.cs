using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Core.Entities;
using Infra.Persistence;
using MediatR;
using Application.Despatches;
using FluentValidation.Results;
using FluentValidation.AspNetCore;
using Application.Despatches.Commands.CreateDespatch;

namespace WebApp.Pages.Despatches
{
    public class CreateModel : PageModel
    {
        private readonly IMediator _mediator;

        public CreateModel(IMediator mediator)
        {
            _mediator = mediator;
        }
        public SelectList IndentingDeptOptions { get; set; }
        
        [BindProperty]
        public CreateDespatchCommand Despatch { get; set; }

        public void OnGet()
        {
            InitSelectListItems();
        }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            InitSelectListItems();
            //Despatch.ProposalOptions = ProposalOptions;
            ValidationResult validationCheck = new CreateDespatchCommandValidator().Validate(Despatch);
            validationCheck.AddToModelState(ModelState, nameof(Despatch));

            if (ModelState.IsValid)
            {
                List<string> errors = await _mediator.Send(Despatch);

                if (errors.Count == 0)
                {

                    return RedirectToPage("./Index");
                }

                foreach (var err in errors)
                {
                    ModelState.AddModelError(string.Empty, err);
                }
            }
            return Page();



        }
        public void InitSelectListItems()
        {
            IndentingDeptOptions = new SelectList(IndentingDeptConstants.GetIndentingDeptOptions());
        }
    }
}
