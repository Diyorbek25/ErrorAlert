using ErrorAlert.Api.Models;
using System.Text;

namespace ErrorAlert.Api.Helper
{
    public class MessageFactory
    {
        public static string TableBuilder(ActionLog error)
        {
            var builder = new StringBuilder();

            builder.AppendLine($"StatusCode: {(int?)error?.StatusCode} ❗❗❗\n");
            builder.AppendLine($"<pre>|{new string('-', 18)}|{new string('-', 30)}|");

            //builder.AppendLine(String.Format("| {0, -16} | {1, -29}|", "Id", $"{error.Id}"));
            builder.AppendLine(string.Format("| {0, -16} | {1, -29}|", "CreatedTime", $"{error?.CreatedDate}"));
            builder.AppendLine(string.Format("| {0, -16} | {1, -29}|", $"StatusCode", $"{(int?)error?.StatusCode}"));
            builder.AppendLine($"|{new string('-', 18)}|{new string('-', 30)}|</pre>");

            builder.AppendLine($"Path:\t<pre>{error?.Controller}/{error?.Action}</pre>\n");
            builder.AppendLine($"Exception:\t{error?.Exception}\n");

            return builder.ToString();
        }
    }
}
