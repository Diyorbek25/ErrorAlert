using ErrorAlert.Api.Models;
using ErrorAlert.Api.SendErrorMessageService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using System.Text.Json;

namespace ErrorAlert.Api.Filters
{
    public class LogAttribute : Attribute, IAsyncActionFilter
    {
        private ActionLog actionLog;
        private ISendErrorMessageService sendErrorMessageService;

        public LogAttribute(ISendErrorMessageService sendErrorMessageService)
        {
            this.sendErrorMessageService = sendErrorMessageService;
            actionLog = new ActionLog();
        }
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var httpContect = context.HttpContext;
            var request = httpContect?.Request;

            actionLog.IpAddress = httpContect?.Connection?.RemoteIpAddress?.ToString();
            actionLog.Port = httpContect?.Connection?.RemotePort ?? default;
            actionLog.Controller = httpContect?.GetRouteValue("controller")?.ToString();
            actionLog.Action = httpContect?.GetRouteValue("action")?.ToString();
            actionLog.MethodType = request?.Method;
            actionLog.RequestData = JsonSerializer.Serialize(context.ActionArguments);
            actionLog.RequestHeader = JsonSerializer.Serialize(request.Headers);
        }

        public async Task OnActionExecutedAsync(ActionExecutedContext context)
        {
            var response = context?.HttpContext?.Response;
            Exception? exception = context?.Exception;

            try
            {
                var result = (ObjectResult?)context?.Result;
                var responseResult = (Response?)result?.Value;
                actionLog.ResponseData = JsonSerializer.Serialize(result);
                actionLog.StatusCode = (HttpStatusCode?)response?.StatusCode;

                if ((int?)actionLog.StatusCode >= 500)
                {
                    this.sendErrorMessageService.SendErrorMessageAsync(actionLog).Wait();
                }
            }
            catch (Exception ex)
            {
                actionLog.Exception = $"{actionLog.Exception} - ActionFilterError: {ex.Message}";
                this.sendErrorMessageService.SendErrorMessageAsync(actionLog).Wait();
            }
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            OnActionExecuting(context);
            await OnActionExecutedAsync(await next());
        }
    }
}
