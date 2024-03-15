using LdelossantosN5.Domain.Notifications;
using LdelossantosN5.Domain.Patterns;
using LdelossantosN5.Domain.UseCases;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LdelossantosN5.Domain.Impl.UseCases
{

    //TODO: La clase debe tener un parametro de tipo con el mensaje que va a generar
    //El cual deberia ser de tipo IBaseRequest
    //un método abstracto es responsable de que el mismo se genere
    //y con eso invocamos a mediator, aunque lo hacemos fuera de la sección donde hubo fallas

    public abstract class UseCaseBase<TParam, TMessage>
        : IUseCaseBase<TParam>
        where TParam : class
        where TMessage : class, INotification

    {
        private INotificationSender NotificationSender { get; }
        private IUnitOfWork UOF { get; }
        protected ILogger Logger { get; }
        public UseCaseBase(INotificationSender notificationSender, IUnitOfWork uOF, ILogger logger)
        {
            this.NotificationSender = notificationSender;
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
                { 
                    await this.UOF.SaveAndCommitAsync();
                    TMessage theMessage = CreateNotificationMessage();
                    this.NotificationSender.Publish(theMessage);
                }
                else
                {
                    await this.UOF.ForceRollbackAsync();
                }
            }

            catch (NotFoundException ex)
            {
                Logger.LogError(ex, "NotFound");
                result.NotFoundError(ex);
                await this.UOF.ForceRollbackAsync();
            }
            catch (Exception ex)
            {
                var uidError = Guid.NewGuid().ToString();
                Logger.LogCritical(ex, "Cliente Reference: {uidError}", uidError);
                result.GenericException(uidError);
                await this.UOF.ForceRollbackAsync();
            }
            return result;
        }

        protected abstract TMessage CreateNotificationMessage();
        protected abstract Task ExecuteUseCase(TParam useCaseParam, UseCaseResultModel result);
    }
}