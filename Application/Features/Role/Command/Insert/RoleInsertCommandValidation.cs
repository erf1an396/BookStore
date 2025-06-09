using Application.Contracts;
using Application.Contracts.Identity;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Role.Command.Insert
{
    public class RoleInsertCommandValidation : AbstractValidator<RoleInsertCommand>
    {
        private readonly IIdentityRoleManager _identityRoleManager;
        public RoleInsertCommandValidation(IIdentityRoleManager identityRoleManager)
        {
            RuleFor(x => x.Name)

                .NotEmpty().WithMessage("نام نقش الزامی است.")
                .MaximumLength(50).WithMessage("نام نقش نباید بیشتر از ۵۰ کاراکتر باشد.")
                .MustAsync(async (name , CancellationToken) =>
                {
                    return !(await identityRoleManager.RoleExistAsync(name));

                }).WithMessage("نقش با این نام وجود دارد.");
                

           

        }


    }
    
}
