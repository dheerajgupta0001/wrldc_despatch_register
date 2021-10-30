using Application.Common;
using Application.Common.Interfaces;
using Application.Users;
using Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Despatches.Commands.DeleteDespatch
{
    public class DeleteDespatchCommandHandler : IRequestHandler<DeleteDespatchCommand, List<string>>
    {
        private readonly ILogger<DeleteDespatchCommandHandler> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICurrentUserService _currentUserService;
        private readonly IAppDbContext _context;

        public DeleteDespatchCommandHandler(ILogger<DeleteDespatchCommandHandler> logger, IAppDbContext context, ICurrentUserService currentUserService, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _context = context;
            _currentUserService = currentUserService;
            _userManager = userManager;
        }

        public async Task<List<string>> Handle(DeleteDespatchCommand request, CancellationToken cancellationToken)
        {
            string curUsrId = _currentUserService.UserId;
            ApplicationUser curUsr = await _userManager.FindByIdAsync(curUsrId);
            if (curUsr == null)
            {
                var errorMsg = "User not found for proposal deletion";
                _logger.LogError(errorMsg);
                return new List<string>() { errorMsg };
            }

            // fetch the despatch for editing
            var despatch = await _context.Despatches.Where(ns => ns.Id == request.Id).FirstOrDefaultAsync(cancellationToken);

            if (despatch == null)
            {
                string errorMsg = $"Despatch Id {request.Id} not present for deletion";
                return new List<string>() { errorMsg };
            }

            // check if user is authorized for deleting the proposal
            IList<string> usrRoles = await _userManager.GetRolesAsync(curUsr);
            if (curUsr.UserName != despatch.CreatedBy && !usrRoles.Contains(SecurityConstants.AdminRoleString))
            {
                return new List<string>() { "This user is not authorized for deleting this proposal since this is not his created by this user and he is not in admin role" };
            }

            _context.Despatches.Remove(despatch);
            await _context.SaveChangesAsync(cancellationToken);

            return new List<string>();

        }
    }
}
