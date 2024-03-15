using LdelossantosN5.Domain.Impl.Repositories;
using LdelossantosN5.Domain.Impl.UseCases;
using LdelossantosN5.Domain.Models;
using LdelossantosN5.Domain.Notification;
using Microsoft.Extensions.Logging;
using Moq;
using System.Reflection.Metadata.Ecma335;

namespace LdelossantosN5.Domain.Impl.Tests.UsesCases
{
    public class ModifyPermissionUseCaseStoreProcedureTest
    {
        public ILoggerFactory LogFactory { get; }

        class TestValues
            : UseCaseTestValues
        {
            public Mock<IEmployeePermissionRepository> moqEmployeePermissionRepository = new Mock<IEmployeePermissionRepository>();
            public ModifyPermissionUseCaseStoreProcedure GetUseCase(ILoggerFactory logFactory)
            {
                return new ModifyPermissionUseCaseStoreProcedure(
                    this.moqMediator.Object,
                    this.moqUOF.Object,
                    logFactory.CreateLogger<ModifyPermissionUseCaseStoreProcedure>(),
                    this.moqEmployeePermissionRepository.Object
                );
            }
        }

        public ModifyPermissionUseCaseStoreProcedureTest()
        {
            this.LogFactory = LoggerFactory.Create(builder => builder.AddDebug());
        }

        [Fact]
        public async Task ModifyPermissionUseCaseStoreProcedureNotify()
        {
            var testValues = new TestValues();
            var model = new ModifyPermissionModel()
            {
                EmployeeId = 10,
                NewStatus = PermissionStatus.permissionDenied,
                PermissionId = 50
            };

            testValues.moqEmployeePermissionRepository
                .Setup(x => x.UpdateEmployeePermissionAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<PermissionStatus>()))
                .ReturnsAsync(() => 25);

            testValues.moqMediator.Setup(x => x.Publish(It.IsAny<ModifyPermissionNotification>()))
                .Callback<ModifyPermissionNotification>((msg) =>
                {
                    Assert.Equal(model.EmployeeId, msg.EmployeeId);
                    Assert.Equal(model.PermissionId, msg.PermissionId);
                    Assert.Equal(25, msg.PermissionTypeId);
                    Assert.Equal(PermissionStatus.permissionDenied.ToString(), msg.Status);
                });

            var useCase = testValues.GetUseCase(this.LogFactory);

            var result = await useCase.ExecuteAsync(model);
            Assert.NotNull(result);
            Assert.True(result.Success);
        }

        [Fact]
        public async Task RejectRequestPermission()
        {
            var testValues = new TestValues();
            var model = new ModifyPermissionModel()
            {
                EmployeeId = 10,
                NewStatus = PermissionStatus.permissionRequested,
                PermissionId = 50
            };

            var useCase = testValues.GetUseCase(this.LogFactory);
            var result = await useCase.ExecuteAsync(model);
            Assert.NotNull(result);
            Assert.False(result.Success);
        }
    }
}
