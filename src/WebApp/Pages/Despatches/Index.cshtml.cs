using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Core.Entities;
using Infra.Persistence;
using Microsoft.Extensions.Logging;
using MediatR;
using Application.Despatches.Queries.GetDespatches;

namespace WebApp.Pages.Despatches
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IMediator _mediator;

        public IndexModel(ILogger<IndexModel> logger,IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        public IList<Despatch> Despatch { get;set; }

        public async Task OnGetAsync()
        {
            Despatch = (await _mediator.Send(new GetDespatchesQuery()));
        }
    }
}
