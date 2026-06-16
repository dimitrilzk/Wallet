namespace Wallet.Application.Common.Results
{
    public enum ErrorType
    {
        Validation, //400
        Unauthorized, //401
        Forbidden, //403
        NotFound, //404
        Conflict, //409
        Unexpected //500
    }
}
