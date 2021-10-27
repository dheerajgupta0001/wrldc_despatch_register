using Application.Common.Interfaces;
using Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Despatches.Commands.CreateDespatch
{
    class CreateDespatchCommandHandler : IRequestHandler<CreateDespatchCommand, List<string>>
    {
        private readonly IAppDbContext _context;

        public CreateDespatchCommandHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<List<string>> Handle(CreateDespatchCommand request, CancellationToken cancellationToken)
        {
            Despatch despatch = new()
            {
                IndentingDept = request.IndentingDept,
                ReferenceNo = request.ReferenceNo,
                Purpose = request.Purpose
            };

            _context.Despatches.Add(despatch);
            _ = await _context.SaveChangesAsync(cancellationToken);

            return new List<string>();
        }
    }
}
