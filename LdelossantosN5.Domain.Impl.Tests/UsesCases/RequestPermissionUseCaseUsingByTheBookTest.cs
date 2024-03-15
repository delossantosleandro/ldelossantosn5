using LdelossantosN5.Domain.Impl.DbEntities;
using LdelossantosN5.Domain.Impl.UseCases;
using LdelossantosN5.Domain.Notification;
using LdelossantosN5.Domain.Notifications;
using LdelossantosN5.Domain.Patterns;
using LdelossantosN5.Domain.Repositories;
using LdelossantosN5.Domain.UseCases;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LdelossantosN5.Domain.Impl.Tests.UsesCases
{
    public class RequestPermissionUseCaseUsingByTheBookTest
    {
        public RequestPermissionUseCaseUsingByTheBookTest()
        {
            this.LogFactory = LoggerFactory.Create(builder => builder.AddDebug());
        }

        public ILoggerFactory LogFactory { get; }

        class TestValues
        {
            public Mock<IEmployeeSecurityRepository> moqEmployeeSecurityRepo = new Mock<IEmployeeSecurityRepository>();
            public Mock<IPermissionTypeRepository> moqPermissionTypeRepository = new Mock<IPermissionTypeRepository>();
            public Mock<IUnitOfWork> moqUOF = new Mock<IUnitOfWork>();
            public Mock<INotificationSender> moqMediator = new Mock<INotificationSender>();
            public RequestPermissionUseCaseUsingByTheBook GetUseCase(ILoggerFactory logFactory)
            {
                return new RequestPermissionUseCaseUsingByTheBook(
                    moqMediator.Object,
                    moqUOF.Object,
                    logFactory.CreateLogger<RequestPermissionUseCaseUsingByTheBook>(),
                    moqEmployeeSecurityRepo.Object,
                    moqPermissionTypeRepository.Object
                );
            }
        }

        [Fact]
        public void CreateRequestPermisionUseCase()
        {
            var testValues = new TestValues();
            Assert.NotNull(testValues.GetUseCase(LogFactory));
        }

        [Fact]
        public async Task HappyPath()
        {
            var testValues = new TestValues();

            testValues.moqEmployeeSecurityRepo.Setup(repo => repo.FindAsync(It.IsAny<int>())).ReturnsAsync(new DbEntities.EmployeeSecurityEntity());
            testValues.moqPermissionTypeRepository.Setup(repo => repo.FindAsync(It.IsAny<int>())).ReturnsAsync(new DbEntities.PermissionTypeEntity());

            var result = await testValues.GetUseCase(LogFactory).ExecuteAsync(new RequestPermissionUseCaseParams() { EmployeeId = 1, PermissionTypeId = 1 });
            Assert.True(result.Success);
        }

        [Fact]
        public async Task EmployeeNotFound()
        {
            var testValues = new TestValues();

            testValues.moqEmployeeSecurityRepo.Setup(repo => repo.FindAsync(It.IsAny<int>())).Throws(new NotFoundException(typeof(EmployeeSecurityEntity), 1));

            var result = await testValues.GetUseCase(LogFactory).ExecuteAsync(
                new RequestPermissionUseCaseParams() { EmployeeId = 1, PermissionTypeId = 1 }
            );
            Assert.True(result.IsNotFound());
        }

        [Fact]
        public async Task ExceptionsDuringTransaction()
        {
            var testValues = new TestValues();
            testValues.moqEmployeeSecurityRepo.Setup(repo => repo.FindAsync(It.IsAny<int>())).Throws(new Exception());

            var result = await testValues.GetUseCase(LogFactory).ExecuteAsync(
                new RequestPermissionUseCaseParams() { EmployeeId = 1, PermissionTypeId = 1 }
            );
            Assert.True(result.IsException());
        }

        [Fact]
        public async Task CantInsertExistingPermissions()
        {
            var testValues = new TestValues();
            var permissionType = new PermissionTypeEntity() { Id = 1 };
            var newEmployee = new EmployeeSecurityEntity()
            {
                Id = 1,
                Name = "Jhon",
                Permissions = new List<EmployeePermissionEntity>()
                {
                    new() { Id = 1, PermissionType = permissionType }
                }
            };

            testValues.moqEmployeeSecurityRepo.Setup(repo => repo.FindAsync(It.IsAny<int>())).ReturnsAsync(newEmployee);
            testValues.moqPermissionTypeRepository.Setup(repo => repo.FindAsync(It.IsAny<int>())).ReturnsAsync(permissionType);
            var result = await testValues.GetUseCase(LogFactory).ExecuteAsync(
                new RequestPermissionUseCaseParams() { EmployeeId = 1, PermissionTypeId = 1 }
            );
            Assert.False(result.Success);
        }

        [Fact]
        public async Task OnSuccessPublishTheMessage()
        {
            var testValues = new TestValues();
            var permissionType = new PermissionTypeEntity() { Id = 1 };
            var newEmployee = new EmployeeSecurityEntity()
            {
                Id = 1,
                Name = "Jhon",
                Permissions = new List<EmployeePermissionEntity>()
            };

            testValues.moqEmployeeSecurityRepo.Setup(repo => repo.FindAsync(It.IsAny<int>())).ReturnsAsync(newEmployee);
            testValues.moqPermissionTypeRepository.Setup(repo => repo.FindAsync(It.IsAny<int>())).ReturnsAsync(permissionType);

            testValues.moqMediator.Setup(mediator => mediator.Publish(It.IsAny<PermissionRequestNotification>()))
                .Callback<PermissionRequestNotification>((perms) =>
                {
                    Assert.Equal(1, perms.EmployeeId);
                    Assert.Equal(1, perms.PermissionTypeId);
                });

            var result = await testValues.GetUseCase(LogFactory).ExecuteAsync(new RequestPermissionUseCaseParams() { EmployeeId = 1, PermissionTypeId = 1 });
            Assert.True(result.Success);
        }
    }
}
