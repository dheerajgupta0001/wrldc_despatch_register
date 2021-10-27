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

namespace Application.Despatches.Queries.GetDespatchById
{
    public class GetDespatchByIdQuery : IRequest<Despatch>
    {
        public int Id { get; set; }
    }

    public class GetDespatchByIdQueryHandler : IRequestHandler<GetDespatchByIdQuery, Despatch>
    {
        private readonly IAppDbContext _context;

        public GetDespatchByIdQueryHandler(IAppDbContext context)
        {
            _context = context;
        }

        async Task<Despatch> IRequestHandler<GetDespatchByIdQuery, Despatch>.Handle(GetDespatchByIdQuery request, CancellationToken cancellationToken)
        {
            Despatch res = await _context.Despatches.Where(co => co.Id == request.Id)
                                            .FirstOrDefaultAsync(cancellationToken: cancellationToken);
            return res;
        }
    }
}
