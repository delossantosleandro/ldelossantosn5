using LdelossantosN5.Domain.CQRS;
using LdelossantosN5.Domain.Impl.UseCases;
using LdelossantosN5.Domain.Models;
using LdelossantosN5.Domain.Notification;
using LdelossantosN5.Domain.Repositories;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LdelossantosN5.Domain.Impl.Tests.UsesCases
{
    public class GetPermissionUseCaseCQRSTests
    {
        public ILoggerFactory LogFactory { get; }

        public GetPermissionUseCaseCQRSTests()
        {
            this.LogFactory = LoggerFactory.Create(builder => builder.AddDebug());
        }

        class TestValues
            : UseCaseTestValues
        {
            public Mock<IEmployeeSecurityCQRS> moqCQRS = new Mock<IEmployeeSecurityCQRS>();
            public GetPermissionUseCaseCQRS GetUseCase(ILoggerFactory logFactory)
            {
                return new GetPermissionUseCaseCQRS(
                    moqMediator.Object,
                    moqUOF.Object,
                    logFactory.CreateLogger<GetPermissionUseCaseCQRS>(),
                    moqCQRS.Object
                );
            }
        }

        [Fact]
        public async Task GetPermissionUseCaseCQRSCallsCQRS()
        {
            var testValues = new TestValues();

            testValues.moqMediator.Setup(x => x.Publish(It.IsAny<GetPermissionNotification>()))
                .Callback<GetPermissionNotification>((theMessage) => Assert.Equal(25, theMessage.EmployeeId));

            testValues.moqCQRS.Setup(x=> x.GetEmployeeSecurityAsync(It.IsAny<int>()))
                .ReturnsAsync(() => new EmployeeSecurityModel() { Id = 25, Name = "Jhon" });

            var useCase = testValues.GetUseCase(this.LogFactory);
            var result = await useCase.ExecuteAsync(new Models.GetPermissionsModel() { EmployeeId = 25 });

            Assert.NotNull(result.ResponseData);
            Assert.IsAssignableFrom<EmployeeSecurityModel>(result.ResponseData);
            Assert.Equal(25, (result.ResponseData as EmployeeSecurityModel)!.Id);
            Assert.Equal("Jhon", (result.ResponseData as EmployeeSecurityModel)!.Name);
        }
    }
}
