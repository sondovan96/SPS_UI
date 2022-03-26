using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace SPS.UI.Filters
{
    public class UnhandleExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override Task OnExceptionAsync(ExceptionContext context)
        {
            var exceptionType = context.Exception.GetType();
            var methodInfo = typeof(ExceptionHandler).GetMethod("Handle" + exceptionType.Name);

            ExceptionHandler exceptionHandler = new ExceptionHandler();

            context.Result = new ViewResult
            {
                ViewName = methodInfo != null ? 
                methodInfo.Invoke(exceptionHandler, new object[] { context.Exception }) as string : 
                exceptionHandler.HandleUnhandleException(context.Exception) as string
            };

            return base.OnExceptionAsync(context);
        }
    }
}
