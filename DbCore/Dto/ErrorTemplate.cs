using System.Text.Json;

namespace DbCore.Dto
{
    public class ErrorTemplate
    {
        public ErrorTemplate(string message, string stackTrace) 
        {
            this.Message = message;
            this.StackTrace = stackTrace;
        }
        public string Message { get; set; }
        public string StackTrace { get; set; }

        public string Serialize()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
