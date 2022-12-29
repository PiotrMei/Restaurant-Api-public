using FluentValidation;
using nowe_Restaurant_API.Entities;

namespace nowe_Restaurant_API.Models.Validators
{
    public class UserDtoValidator : AbstractValidator<UserDto>
    {
        public UserDtoValidator(RestaurantDbContext dbContect)
        {
            RuleFor(u => u.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(u => u.Password).MinimumLength(6);
            RuleFor(u => u.ConfirmPassword).Equal(p => p.Password);

            RuleFor(e => e.Email)
                .Custom((value, context) =>
                {
                   var isemail= dbContect.Users.Any(u => u.Email == value);
                    if(isemail)
                    {
                        context.AddFailure("Email", "Email is taken");
                    }

                });

        }
    }
}
