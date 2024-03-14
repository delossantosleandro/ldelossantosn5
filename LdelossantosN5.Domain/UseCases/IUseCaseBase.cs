namespace LdelossantosN5.Domain.UseCases
{
    public interface IUseCaseBase<TParam>
        where TParam : class
    {
        Task<UseCaseResultModel> ExecuteAsync(TParam param);
    }
}