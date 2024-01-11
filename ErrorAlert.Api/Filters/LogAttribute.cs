using Microsoft.AspNetCore.Mvc.Filters;
using System.Text.Json;

namespace ErrorAlert.Api.Filters
{
    public class LogAttribute : IActionFilter
    {
        private string ipAddress;
        private int port;
        private HttpRequest? request;
        private HttpResponse response;
        private string? methodType;
        private int? companyId;
        private int? userId;
        private bool isCoorporateOrg = false;
        private string? controllerName;
        private string? actionName;
        private string? requestBody;
        private string? requestHeader;

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var httpContect = context.HttpContext;
            var user = httpContect?.User;

            controllerName = httpContect?.GetRouteValue("controller")?.ToString();
            actionName = httpContect?.GetRouteValue("action")?.ToString();

            request = httpContect?.Request;
            methodType = request?.Method;

            requestBody = JsonSerializer.Serialize(context.ActionArguments);
            requestHeader = JsonSerializer.Serialize(request.Headers);
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            var response = context.HttpContext.Response;
            Exception? exception = context?.Exception;

            try
            {
                var result = context?.Result;


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }
}
