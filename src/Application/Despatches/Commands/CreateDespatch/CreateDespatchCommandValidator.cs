using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Despatches.Commands.CreateDespatch
{
    public class CreateDespatchCommandValidator : AbstractValidator<CreateDespatchCommand>
    {
        public CreateDespatchCommandValidator()
        {
            RuleFor(x => x.ReferenceNo).NotEmpty();
            RuleFor(x => x.IndentingDept).NotEmpty();
            RuleFor(x => x.Purpose).NotEmpty();
        }
    }
}
