using LdelossantosN5.Domain.Notifications;
using LdelossantosN5.Domain.Patterns;
using MediatR;

namespace LdelossantosN5.WebApi.BackgroundProcess
{
    public class MediatRBackgroundNotificationSender
        : INotificationSender
    {
        private IServiceProvider ServiceProvider { get; }
        public MediatRBackgroundNotificationSender(IServiceProvider serviceProvider)
        {
            this.ServiceProvider = serviceProvider;
        }
        void INotificationSender.Publish<TMessage>(TMessage theMessage)
        {
            Task.Run(() => ProcessMessage(theMessage));
        }

        private async Task ProcessMessage<TMessage>(TMessage theMessage) where TMessage : class, INotification
        {
            using (var scope = this.ServiceProvider.CreateScope())
            {
                var uof = scope.ServiceProvider.GetService<IUnitOfWork>()!;
                await uof.BeginTransaction();
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>()!;
                await mediator.Publish(theMessage);
                await uof.SaveAndCommitAsync();
            }
        }
    }
}