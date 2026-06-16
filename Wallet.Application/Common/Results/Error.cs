namespace Wallet.Application.Common.Results
{
    public class Error
    {
        public Error(string code, string message, ErrorType errorType)
        {
            Code = code;
            Message = message;
            ErrorType = errorType;
        }
        public string Code { get; init; }
        public string Message { get; init; }
        public ErrorType ErrorType { get; init; }
    }
}
