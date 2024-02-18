namespace AuctionSite.API.Services.ErrorValidation
{
    public interface IErrorValidationHandler<T, R>
    {
        T HandleError(R modelState);
    }
}
