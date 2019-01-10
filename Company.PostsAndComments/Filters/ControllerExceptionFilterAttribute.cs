using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Company.PostsAndCommentsModels.CustomExceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Company.PostsAndComments.Filters
{
    public class ControllerExceptionFilterAttribute: ExceptionFilterAttribute
    {
        private readonly ILogger _logger;

        public ControllerExceptionFilterAttribute(ILogger logger)
        {
            _logger = logger;
        }

        private readonly Dictionary<Type, IActionResult> _exceptionFilter =
            new Dictionary<Type, IActionResult>()
            {
                {
                    typeof(PacNotFoundException),
                    new StatusCodeResult(404)
                },
                {
                    typeof(PacInvalidModelException),
                    new StatusCodeResult(422)
                },
                {
                    typeof(PacInvalidOperationException),
                    new StatusCodeResult(400)
                },
                {
                    typeof(PacUnauthorizedAccessException),
                    new StatusCodeResult(403)
                }
            };

        public override void OnException(ExceptionContext context)
        {
            context = GetStatusCodeResult(context);

            base.OnException(context);
        }

        public override Task OnExceptionAsync(ExceptionContext context)
        {
            context = GetStatusCodeResult(context);

            return base.OnExceptionAsync(context);
        }

        private ExceptionContext GetStatusCodeResult(ExceptionContext context)
        {
            try
            {
                context.Result =
                    _exceptionFilter[context.Exception.GetType()];

                return context;
            }
            catch
            {
                context.Result =
                    new StatusCodeResult(500);

                return context;
            }
            finally
            {
                _logger.LogError(context.Exception, context.ActionDescriptor.ToString());
            }
        }
    }
}
