using Data.Models;
using FluentValidation;
using Microsoft.Extensions.Localization;
using Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace LOgic.UserManager.ViewModels.Validators
{
    public class RegistValidator : AbstractValidator<RegistModel>
    {
        public RegistValidator(IStringLocalizer<LanguageResource> localizer)
        {
            RuleFor(u => u.UserName).NotNull().MinimumLength(10).WithMessage(localizer["UserNameMinLength"].Value)
                .MaximumLength(50).WithMessage(localizer["UserNameMaxLength"].Value);
            //  .WithMessage(localizer["ValidUserName"].Value)

            RuleFor(u => u.Password).NotNull().NotEmpty()
                 .MaximumLength(16).WithMessage(localizer["PasswordMinLength"].Value)
                .MinimumLength(8).WithMessage(localizer["PasswordMaxLength"].Value)
                .Matches(@"^(?=.*[0-9])(?=.*\d)(?=.*?[A-Z])(?=.*[a-z])(?=.*[a-zA-Z])(?=.*?[#?!@$%^&*-]).*$").WithMessage(localizer["PasswordPattern"].Value);

            RuleFor(u => u.MobileNumber).NotNull().NotEmpty();


        }
    }
}
