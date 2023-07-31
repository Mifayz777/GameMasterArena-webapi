using FluentValidation;
using GameMasterArena.Service.Dtos.Persons;

namespace GameMasterArena.Service.Validators.Dtos.Students;

public class PersonLoginValidator : AbstractValidator<PersonLoginDto>
{
    public PersonLoginValidator()
    {
        RuleFor(dto => dto.Email).EmailAddress().WithMessage("Write correct email address");

        RuleFor(dto => dto.Password).Must(password => PasswordValidator.IsStrongPassword(password).IsValid)
             .WithMessage("Password is not strong password!");
    }
}
