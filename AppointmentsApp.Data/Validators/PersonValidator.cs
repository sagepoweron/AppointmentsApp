using AppointmentsApp.Data.Models;
using FluentValidation;

namespace AppointmentsApp.Data.Validators
{
    public class PersonValidator : AbstractValidator<Person>
    {
        public PersonValidator()
        {
            RuleFor(person => person.Name).NotEmpty().WithMessage("The name can't be blank");
      
        }
    }
}
