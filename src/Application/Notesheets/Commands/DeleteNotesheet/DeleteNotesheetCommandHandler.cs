﻿using Application.Common;
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

namespace Application.Notesheets.Commands.DeleteNotesheet
{
    public class DeleteNotesheetCommandHandler : IRequestHandler<DeleteNotesheetCommand, List<string>>
    {
        private readonly ILogger<DeleteNotesheetCommandHandler> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICurrentUserService _currentUserService;
        private readonly IAppDbContext _context;

        public DeleteNotesheetCommandHandler(ILogger<DeleteNotesheetCommandHandler> logger, IAppDbContext context, ICurrentUserService currentUserService, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _context = context;
            _currentUserService = currentUserService;
            _userManager = userManager;
        }

        public async Task<List<string>> Handle(DeleteNotesheetCommand request, CancellationToken cancellationToken)
        {
            string curUsrId = _currentUserService.UserId;
            ApplicationUser curUsr = await _userManager.FindByIdAsync(curUsrId);
            if (curUsr == null)
            {
                var errorMsg = "User not found for proposal deletion";
                _logger.LogError(errorMsg);
                return new List<string>() { errorMsg };
            }

            // fetch the notesheet for editing
            var notesheet = await _context.Notesheets.Where(ns => ns.Id == request.Id).FirstOrDefaultAsync(cancellationToken);

            if (notesheet == null)
            {
                string errorMsg = $"Notesheet Id {request.Id} not present for deletion";
                return new List<string>() { errorMsg };
            }

            // check if user is authorized for deleting the proposal
            IList<string> usrRoles = await _userManager.GetRolesAsync(curUsr);
            if (curUsr.UserName != notesheet.CreatedBy && !usrRoles.Contains(SecurityConstants.AdminRoleString))
            {
                return new List<string>() { "This user is not authorized for deleting this proposal since this is not his created by this user and he is not in admin role" };
            }

            _context.Notesheets.Remove(notesheet);
            await _context.SaveChangesAsync(cancellationToken);

            return new List<string>();

        }
    }
}
