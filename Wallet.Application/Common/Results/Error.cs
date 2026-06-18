namespace Wallet.Application.Common.Results
{
    public class Error
    {
        private Error(string code, string message, ErrorType errorType)
        {
            Code = code;
            Message = message;
            Type = errorType;
        }
        public string Code { get; init; }
        public string Message { get; init; }
        public ErrorType Type { get; init; }

        public static Error Unexpected(string message)
        {
            return new Error("Unexpected", message, ErrorType.Unexpected);
        }

        public static Error Validation(string code, string message)
        {
            return new Error(code, message, ErrorType.Validation);
        }

        public static Error Unauthorized(string code, string message)
        {
            return new Error(code, message, ErrorType.Unauthorized);
        }

        public static Error Forbidden(string code, string message)
        {
            return new Error(code, message, ErrorType.Forbidden);
        }

        public static Error NotFound(string code, string message)
        {
            return new Error(code, message, ErrorType.NotFound);
        }

        public static Error Conflict(string code, string message)
        {
            return new Error(code, message, ErrorType.Conflict);
        }
    }
}
