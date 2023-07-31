using FluentValidation;
using GameMasterArena.Service.Common.Helpers;
using GameMasterArena.Service.Dtos.Tournaments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameMasterArena.Service.Validators.Dtos.Tournaments;

public class TournamentUpdateValidator: AbstractValidator<TournamentUpdateDto>
{

    public TournamentUpdateValidator()
    {
        RuleFor(dto => dto.Name).NotEmpty().NotNull().WithMessage("Tournament name is required!")
            .MinimumLength(3).WithMessage("Tournament name must be more than 3 characters!")
            .MaximumLength(50).WithMessage("Tournament name must be less than 50 characters!");

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
