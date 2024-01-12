using System.Net;

namespace ErrorAlert.Api.Models;

public class ActionLog
{
    public string IpAddress { get; set; }
    public int Port { get; set; }
    public DateTime CreatedDate { get; set; }
    public string Controller { get; set; }
    public string Action { get; set; }
    public string MethodType { get; set; }
    public HttpStatusCode? StatusCode { get; set; }
    public string RequestHeader { get; set; }
    public string RequestData { get; set; }
    public string? ResponseData { get; set; }
    public string Exception { get; set; }
}