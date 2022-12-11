using FakeCommerce.Entities.ErrorModel;
using FakeCommerce.Entities.Exceptions.BadRequestExceptions;
using FakeCommerce.Entities.Exceptions.NotFoundExceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FakeCommerce.Api.Extensions.Exceptions
{
    public static class ExceptionExtensions
    {
        public static void ConfigureExceptionHandler(this WebApplication app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

                    if (contextFeature != null)
                    {
                        context.Response.StatusCode = contextFeature.Error switch
                        {
                            NotFoundException => StatusCodes.Status404NotFound,
                            BadRequestException => StatusCodes.Status400BadRequest,
                            _ => StatusCodes.Status500InternalServerError
                        };

                        var errorDetail = new ErrorDetail
                        {
                            Message = contextFeature.Error.Message,
                            StatusCode = context.Response.StatusCode
                        }.ToString();

                        await context.Response.WriteAsync(errorDetail!);
                    }

                });
            });
        }
    }
}
