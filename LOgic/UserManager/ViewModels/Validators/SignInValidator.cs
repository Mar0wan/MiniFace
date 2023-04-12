using Data.Models;
using FluentValidation;
using Microsoft.Extensions.Localization;
using Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace LOgic.UserManager.ViewModels.Validators
{
    public class SignInValidator : AbstractValidator<SignInModel>
    {
        public SignInValidator(IStringLocalizer<LanguageResource> localizer)
        {
            RuleFor(u => u.UserName).NotNull().MinimumLength(10)
                 .MaximumLength(100)
                 .WithMessage(localizer["InvalidUsernameOrPassword"].Value);

            RuleFor(u => u.Password).NotNull().NotEmpty()
                .MaximumLength(16)
                .MinimumLength(8).WithMessage(localizer["InvalidUsernameOrPassword"].Value);

        }
    }
}
