using LdelossantosN5.Domain.Notifications;
using LdelossantosN5.Domain.Patterns;
using LdelossantosN5.Domain.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LdelossantosN5.Domain.Impl.Tests.UsesCases
{
    public class UseCaseTestValues
    {
        public Mock<IUnitOfWork> moqUOF = new Mock<IUnitOfWork>();
        public Mock<INotificationSender> moqMediator = new Mock<INotificationSender>();
    }
}
