using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.User.Command.Insert
{
    public class UserInsertCommandValidator : AbstractValidator<UserInsertCommand>
    {
        public UserInsertCommandValidator()
        {
            RuleFor(x => x.FirstName)
               .NotEmpty().WithMessage("نام الزامی است.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("نام خانوادگی الزامی است.");

            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("شماره موبایل الزامی است.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("ایمیل الزامی است.")
                .EmailAddress().WithMessage("فرمت ایمیل نامعتبر است.");

            When(x => string.IsNullOrWhiteSpace(x.Password), () =>
            {
                RuleFor(x => x.Password)
                    .MinimumLength(6).WithMessage("رمز عبور باید حداقل ۶ کاراکتر باشد.")
                    .Matches("[A-Z]").WithMessage("رمز عبور باید شامل حداقل یک حرف بزرگ باشد.")
                    .Matches("[a-z]").WithMessage("رمز عبور باید شامل حداقل یک حرف کوچک باشد.")
                    .Matches("[0-9]").WithMessage("رمز عبور باید شامل عدد باشد.");

                RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.Password)
                .WithMessage("تکرار رمز عبور با رمز عبور مطابقت ندارد.");

            });
        }
    }
}
