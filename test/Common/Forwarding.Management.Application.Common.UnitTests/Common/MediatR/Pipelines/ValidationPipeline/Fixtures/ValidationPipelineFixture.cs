using FluentValidation;
using MediatR;
using Moq;

namespace Forwarding.Management.Application.Common.UnitTests.Common.MediatR.Pipelines.ValidationPipeline.Fixtures;

public class ValidationPipelineFixture
{
    public virtual SampleCommand GetSampleCommand(string message) => new(message);
    public virtual SampleCommandValidator GetSampleCommandValidator() => new();

    public virtual RequestHandlerDelegate<string> GetHandlerDelegate(string message)
    {
        var nextDelegateMoq = new Mock<RequestHandlerDelegate<string>>();

        nextDelegateMoq.Setup(n => n.Invoke()).ReturnsAsync(message);

        return nextDelegateMoq.Object;
    }
}

public class SampleCommand : IRequest<string>
{
    public string Message { get; set; }

    public SampleCommand(string message)
    {
        Message = message;
    }
}

public class SampleCommandValidator : AbstractValidator<SampleCommand>
{
    public SampleCommandValidator()
    {
        RuleFor(x => x.Message).MinimumLength(5)
            .WithMessage("The message cannot be shorter than 5 characters");
        RuleFor(x => x.Message).MaximumLength(25)
            .WithMessage("The message cannot be longer than 25 characters");
    }
}