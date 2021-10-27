using Application.Common.Interfaces;
using Core.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Despatches.Queries.GetDespatches
{
    public class GetDespatchesQuery : IRequest<List<Despatch>>
    {
        public class GetDespatchesQueryHandler : IRequestHandler<GetDespatchesQuery, List<Despatch>>
        {
            private readonly IAppDbContext _context;

            public GetDespatchesQueryHandler(IAppDbContext context)
            {
                _context = context;
            }

            public async Task<List<Despatch>> Handle(GetDespatchesQuery request, CancellationToken cancellationToken)
            {
                List<Despatch> res = await _context.Despatches.ToListAsync();
                return res;
            }
        }
    }
}
