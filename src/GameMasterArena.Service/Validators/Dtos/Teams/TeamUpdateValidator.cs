using FluentValidation;
using GameMasterArena.Service.Common.Helpers;
using GameMasterArena.Service.Dtos.Teams;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GameMasterArena.Service.Validators.Dtos.Teams;

public class TeamUpdateValidator : AbstractValidator<TeamUpdateDto>
{

    public TeamUpdateValidator()
    {
        RuleFor(dto => dto.Name).NotEmpty().NotNull().WithMessage("Team name is required!")
            .MinimumLength(3).WithMessage("Team name must be more than 3 characters!")
            .MaximumLength(50).WithMessage("Team name must be less than 50 characters!");

        When(dto => dto.Image is not null, () =>
        {
            int maxImageSizeMB = 5;
            RuleFor(dto => dto.Image.Length).LessThan(maxImageSizeMB * 1024 * 1024).WithMessage($"Image size must be less than {maxImageSizeMB} MB");
            RuleFor(dto => dto.Image.FileName).Must(predicate =>
            {
                FileInfo fileInfo = new FileInfo(predicate);
                return MediaHelper.GetImageExtensions().Contains(fileInfo.Extension);
            }).WithMessage("This file type is not image file");
        });
    }
        

}
