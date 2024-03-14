using LdelossantosN5.Domain.UseCases;

namespace LdelossantosN5.WebApi.Helpers
{
    public static class ResultExtensions
    {
        public static IResult ToHttpResponse(this UseCaseResultModel result)
        {
            if (result.Success)
                return Results.Ok(result);

            if (result.IsNotFound())
                return Results.NotFound(result);

            if (result.IsException())
                return Results.Problem(string.Join(";", result.Errors.Select(x => $"{x.ErrCode} {x.Description}")));

            //Model validations
            return Results.BadRequest(result);
        }
    }
}