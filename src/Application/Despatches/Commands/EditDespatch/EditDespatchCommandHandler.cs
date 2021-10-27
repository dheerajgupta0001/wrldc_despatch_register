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

namespace Application.Despatches.Commands.EditDespatch
{
    public class EditDespatchCommandHandler : IRequestHandler<EditDespatchCommand, List<string>>
    {
        private readonly ILogger<EditDespatchCommandHandler> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICurrentUserService _currentUserService;
        private readonly IAppDbContext _context;

        public EditDespatchCommandHandler(ILogger<EditDespatchCommandHandler> logger, IAppDbContext context, ICurrentUserService currentUserService, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _context = context;
            _currentUserService = currentUserService;
            _userManager = userManager;
        }

        public async Task<List<string>> Handle(EditDespatchCommand request, CancellationToken cancellationToken)
        {
            string curUsrId = _currentUserService.UserId;
            ApplicationUser curUsr = await _userManager.FindByIdAsync(curUsrId);
            if (curUsr == null)
            {
                var errorMsg = "User not found for editing proposal";
                _logger.LogError(errorMsg);
                return new List<string>() { errorMsg };
            }

            // fetch the notesheet for editing
            var notesheet = await _context.Despatches.Where(ns => ns.Id == request.Id).FirstOrDefaultAsync(cancellationToken);

            if (notesheet == null)
            {
                string errorMsg = $"Proposal Id {request.Id} not present for editing";
                return new List<string>() { errorMsg };
            }

            // check if user is authorized for editing proposal
            IList<string> usrRoles = await _userManager.GetRolesAsync(curUsr);
            if (curUsr.UserName != notesheet.CreatedBy && !usrRoles.Contains(SecurityConstants.AdminRoleString))
            {
                return new List<string>() { "This user is not authorized for updating this proposal since this is not his created by this user and he is not in admin role" };
            }

            if (notesheet.IndentingDept != request.IndentingDept) //new field
            {
                notesheet.IndentingDept = request.IndentingDept;
            }
            if (notesheet.ReferenceNo != request.ReferenceNo)
            {
                notesheet.ReferenceNo = request.ReferenceNo;
            }
            if (notesheet.Purpose != request.Purpose)
            {
                notesheet.Purpose = request.Purpose;
            }
            
            try
            {
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Despatches.Any(e => e.Id == request.Id))
                {
                    return new List<string>() { $"Order Id {request.Id} not present for editing" };
                }
                else
                {
                    throw;
                }
            }

            return new List<string>();

        }
    }
}
