using System.Net;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Forwarding.Management.Api.Middlewares;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate request;

    public ExceptionHandlerMiddleware(RequestDelegate request)
    {
        this.request = request;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await request(context);
        }
        catch (Exception exception)
        {
            context.Response.ContentType = "application/problem+json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            var problemDetails = new ProblemDetails()
            {
                Type = exception.GetType().ToString()
            };

            if (exception is ValidationException validationException)
            {
                var status = validationException.Errors.Any(x => x.ErrorCode == HttpStatusCode.NotFound.ToString())
                    ? (int)HttpStatusCode.NotFound
                    : (int)HttpStatusCode.BadRequest;

                problemDetails.Detail = string.Join(
                    ';',
                    validationException.Errors.Select(e => e.ErrorMessage).ToArray());

                problemDetails.Status = status;

                context.Response.StatusCode = status;
            }
            else
            {
                problemDetails.Detail = exception.Message;
            }

            await context.Response.WriteAsJsonAsync(problemDetails);
        }
    }
}
