using System.Threading.Tasks;
using Company.PostsAndCommentsModels.CustomExceptions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Company.PostsAndComments.Filters
{
    public class ModelValidatorFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                throw new PacInvalidModelException();
            }

            base.OnActionExecuting(context);
        }

        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            return context.ModelState.IsValid
                ? base.OnActionExecutionAsync(context, next)
                : throw new PacInvalidModelException();
        }
    }
}
