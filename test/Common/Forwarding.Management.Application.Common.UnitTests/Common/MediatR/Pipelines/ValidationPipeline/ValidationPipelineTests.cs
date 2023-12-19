using FluentValidation;
using Forwarding.Management.Application.Common.MediatR.Pipelines;
using Forwarding.Management.Application.Common.UnitTests.Common.MediatR.Pipelines.ValidationPipeline.Fixtures;

namespace Forwarding.Management.Application.Common.UnitTests.Common.MediatR.Pipelines.ValidationPipeline;

public class ValidationPipelineTests : IClassFixture<ValidationPipelineFixture>
{
    private readonly ValidationPipelineFixture fixture;

    public ValidationPipelineTests(ValidationPipelineFixture fixture)
    {
        this.fixture = fixture;
    }

    [Theory]
    [InlineData("1234")]
    [InlineData("Too long message 123456789")]
    public async Task ValidationPipeline_IncorrectCommand_ShouldThrowException(string message)
    {
        IEnumerable<IValidator<SampleCommand>> validators = new IValidator<SampleCommand>[]
        {
            fixture.GetSampleCommandValidator()
        };

        var request = fixture.GetSampleCommand(message);

        var nextHandlerDelegate = fixture.GetHandlerDelegate(message);

        var pipeline = new ValidationPipelineBehavior<SampleCommand, string>(validators);

        await Assert.ThrowsAsync<ValidationException>(async () =>
            await pipeline.Handle(request, nextHandlerDelegate.Invoke, CancellationToken.None));
        }

    [Theory]
    [InlineData("12345")]
    public async Task ValidationPipeline_CorrectCommand_ShouldNotThrowException(string message)
    {
        IEnumerable<IValidator<SampleCommand>> validators = new IValidator<SampleCommand>[]
        {
            fixture.GetSampleCommandValidator()
        };

        var request = fixture.GetSampleCommand(message);

        var nextHandlerDelegate = fixture.GetHandlerDelegate(message);

        var pipeline = new ValidationPipelineBehavior<SampleCommand, string>(validators);

        var response =
            await pipeline.Handle(request, nextHandlerDelegate.Invoke, CancellationToken.None);

        Assert.Equal(typeof(string), response.GetType());
        Assert.Equal(message, response);
    }
}