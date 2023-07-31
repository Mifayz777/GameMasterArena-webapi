using FluentValidation;
using GameMasterArena.Service.Common.Helpers;
using GameMasterArena.Service.Dtos.Persons;

namespace GameMasterArena.Service.Validators.Dtos.Students;

public class PersonUpdateValidator : AbstractValidator<PersonUpdateDto>
{
    public PersonUpdateValidator()
    {

        RuleFor(dto => dto.FirstName).NotEmpty().NotNull().WithMessage("Person First_Name is required!")
            .MinimumLength(3).WithMessage("Person First_Name must be more than 3 characters!")
            .MaximumLength(50).WithMessage("Person First_Name must be less than 50 characters!");

        RuleFor(dto => dto.LastName).NotEmpty().NotNull().WithMessage("Person Last_Name is required!")
            .MinimumLength(3).WithMessage("Person Last_Name must be more than 3 characters!")
            .MaximumLength(50).WithMessage("Person Last_Name must be less than 50 characters!");


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
