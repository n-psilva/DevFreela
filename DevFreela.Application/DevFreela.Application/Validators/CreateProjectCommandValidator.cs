using DevFreela.Application.Commands.CreateProject;
using FluentValidation;

namespace DevFreela.Application.Validators
{
    public class CreateProjectCommandValidator : AbstractValidator<CreateProjectCommand>
    {
        public CreateProjectCommandValidator() 
        {
            RuleFor(proj => proj.Description)
                .MaximumLength(255)
                .WithMessage("Tamanho máximo da Descrição é de 255 caracteres.");

            RuleFor(proj => proj.Title)
                .MaximumLength(30)
                .WithMessage("Tamanho máximo do Título é de 30 caracteres.");
        }
    }
}
