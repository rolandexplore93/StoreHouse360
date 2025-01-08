using FluentValidation;

namespace StoreHouse360.Application.Commands.Invoicing
{
    public class CreateInvoiceCommandValidator : AbstractValidator<CreateInvoiceCommand>
    {
        public CreateInvoiceCommandValidator()
        {
            RuleFor(command => command.Items).NotEmpty().WithMessage("Invoice cannot be empty!");
        }
    }
}
