using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace GBM_Stocks.Filters
{
    public class HttpResponseExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            string message;
            if (context.Exception is UnauthorizedAccessException)
            {
                context.HttpContext.Response.StatusCode = 401;
                message = context.Exception.Message;
            }
            else if (context.Exception is SqlException)
            {
                var sqlError = context.Exception as SqlException;
                message = "Error in : " + sqlError.Procedure + " " + sqlError.Message;

                context.HttpContext.Response.StatusCode = 400;
            }
            else
            {
                message = context.Exception.Message;
                context.HttpContext.Response.StatusCode = 500;
            }

            context.Result = new JsonResult(message);
            base.OnException(context);
        }
    }
}
