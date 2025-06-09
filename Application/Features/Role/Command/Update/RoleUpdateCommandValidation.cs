using Application.Contracts.Identity;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Role.Command.Update
{
    public class RoleUpdateCommandValidation : AbstractValidator<RoleUpdateCommand>
    {
        private readonly IIdentityRoleManager _identityRoleManager;
        public RoleUpdateCommandValidation(IIdentityRoleManager identityRoleManager)
        {
            RuleFor(x => x.Name)

               .NotEmpty().WithMessage("نام نقش الزامی است.")
               .MaximumLength(50).WithMessage("نام نقش نباید بیشتر از ۵۰ کاراکتر باشد.");

        }
    }
}
