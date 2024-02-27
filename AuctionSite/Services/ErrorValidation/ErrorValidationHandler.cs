using AuctionSite.API.Contracts;
using Microsoft.AspNetCore.Mvc.ModelBinding;
namespace AuctionSite.API.Services.ErrorValidation
{
    internal class ErrorValidationHandler : IErrorValidationHandler<List<ErrorModel>, ModelStateDictionary>
    {
        public List<ErrorModel> HandleError(ModelStateDictionary modelState)
        {
            List<ErrorModel> errors = new List<ErrorModel>();

            foreach (var state in modelState)
            {
                if (state.Value.ValidationState != ModelValidationState.Invalid) continue;

                var errorModel = new ErrorModel()
                {
                    PropertyName = $"Помилка для поля: {state.Key}"
                };

                foreach (var error in state.Value.Errors)
                {
                    errorModel.Descriptions.Add(error.ErrorMessage);
                }

            }
            return errors;
        }
    }
}
