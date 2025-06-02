using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace AdventOfCode.Application.Extensions;
public static class ErrorHandlingExtensions
{
    public static void UseCustomErrorHandling(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(
            errorInApp =>
            {
                errorInApp.Run(async context =>
                {
                    var ex = context.Features.Get<IExceptionHandlerPathFeature>()?.Error;
                    if(ex != null)
                    {
                        //TODO: add logging
                        //TODO: fetch needed services
                        //TODO: decorate exeption with additional data
                        //TODO: extraxc data from exception
                        //var response = "";
                        context.Response.StatusCode = 200;
                        context.Response.ContentType = "application/json";

                        var response = new
                        {
                            Type = ex.GetType().FullName,
                            Error = ex.Message.ToString(),
                            Inner = ex.InnerException?.ToString(),
                            Message = ex.Message,
                            InnerMessage = ex?.InnerException?.Message
                        };

                        var error = JsonSerializer.Serialize(response);
                        //TODO: write better response based on decorated exception
                        await context.Response.WriteAsync(error);
                    }
                });
            }
        );
    }
}
