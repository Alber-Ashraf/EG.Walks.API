using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EG.Walks.Filtters
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            // Check if the model state is valid
            if (!context.ModelState.IsValid)
            {
                // If the model state is invalid, return a BadRequest response with the validation errors
                context.Result = new BadRequestResult();
                return;
            }
        }
    }
}
