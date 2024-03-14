using LdelossantosN5.Domain.Patterns;
using LdelossantosN5.Domain.UseCases;
using Microsoft.Extensions.Logging;

namespace LdelossantosN5.Domain.Impl.UseCases
{
    public abstract class UseCaseBase<TParam>
        : IUseCaseBase<TParam>
        where TParam : class

    {
        protected IUnitOfWork UOF { get; }
        protected ILogger Logger { get; }
        public UseCaseBase(IUnitOfWork uOF, ILogger logger)
        {
            this.UOF = uOF;
            this.Logger = logger;
        }
        public async Task<UseCaseResultModel> ExecuteAsync(TParam useCaseParam)
        {
            var result = new UseCaseResultModel();
            try
            {
                await this.UOF.BeginTransaction();
                await ExecuteUseCase(useCaseParam, result);
                if (result.Success)
                    await this.UOF.SaveAndCommitAsync();
                else
                    await this.UOF.ForceRollbackAsync();
            }
            catch (NotFoundException ex)
            {
                Logger.LogError(ex, "NotFound");
                result.NotFoundError(ex);
            }
            catch (Exception ex)
            {
                var uidError = Guid.NewGuid().ToString();
                Logger.LogCritical(ex, "Cliente Reference: {uidError}", uidError);
                result.GenericException(uidError);
            }
            return result;
        }
        protected abstract Task ExecuteUseCase(TParam useCaseParam, UseCaseResultModel result);
    }
}