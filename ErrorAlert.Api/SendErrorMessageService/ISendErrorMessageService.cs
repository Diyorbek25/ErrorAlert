using ErrorAlert.Api.Models;

namespace ErrorAlert.Api.SendErrorMessageService
{
    public interface ISendErrorMessageService
    {
        Task SendErrorMessageAsync(ActionLog log);
    }
}
