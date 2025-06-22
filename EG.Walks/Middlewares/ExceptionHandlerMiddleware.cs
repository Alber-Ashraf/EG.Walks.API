using System.Net;

namespace EG.Walks.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var id = Guid.NewGuid();

                // Return a generic error response
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                var errorResponse = new
                {
                    Id = id,
                    ErrorMessage = "An unexpected error occurred.",
                };

                await context.Response.WriteAsJsonAsync(errorResponse);
            }
        }
    }
}
