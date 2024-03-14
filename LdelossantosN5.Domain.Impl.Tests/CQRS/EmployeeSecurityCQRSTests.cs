using LdelossantosN5.Domain.CQRS;
using LdelossantosN5.Domain.Impl.DbEntities;
using LdelossantosN5.Domain.Models;
using LdelossantosN5.Domain.Repositories;
using Moq;

namespace LdelossantosN5.Domain.Tests.CQRS
{
    /// <summary>
    /// Test clasess, this cases using only mocks
    /// </summary>
    public class EmployeeSecurityCQRSTests
    {
        [Fact]
        public async Task GetResultFromRepository()
        {
            var employeeRepo = new Mock<IEmployeeSecurityRepository>();
            var cqrsRepo = new Mock<IEmployeeSecurityCQRSRepository>();
            var resultModel = new EmployeeSecurityModel() { Id = 1 };

            cqrsRepo.Setup(repo => repo.GetEmployeeSecurityAsync(It.IsAny<int>()))
                .ReturnsAsync(resultModel);

            IEmployeeSecurityCQRS cqrs = new EmployeeSecurityCQRS(employeeRepo.Object, cqrsRepo.Object);
            var result = await cqrs.GetEmployeeSecurityAsync(1);
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public async Task OptimizeQueryAndSave()
        {
            var employeeRepo = new Mock<IEmployeeSecurityRepository>();
            var cqrsRepo = new Mock<IEmployeeSecurityCQRSRepository>();

            //Create a sample entity
            var employeeToOptimize = new EmployeeSecurityEntity() { Id = 1, Name = "Test", StartDate = DateTime.Now, Timestamp = [1, 2, 3, 4, 4, 5, 6, 7, 8] };
            for (int i = 0; i < 10; i++)
                employeeToOptimize.Permissions.Add(new EmployeePermissionEntity()
                {
                    Id = i,
                    EmployeeId = 1,
                    PermissionType = new PermissionTypeEntity()
                    {
                        Id = i,
                        ShortName = i.ToString(),
                        Description = $"Desc:{i}"
                    }
                });

            EmployeeSecurityModel? theMappedValue = null;

            //setup the mock
            employeeRepo.Setup(repo => repo.GetEmployeeOptimizedAsync(It.Is<int>(val => val == 1)))
                .ReturnsAsync(employeeToOptimize);

            cqrsRepo.Setup(repo => repo.UpsertEmployeeSecurityAsync(It.IsAny<EmployeeSecurityModel>()))
                .Callback<EmployeeSecurityModel>(model => theMappedValue = model)
                .Returns(Task.CompletedTask);

            IEmployeeSecurityCQRS cqrs = new EmployeeSecurityCQRS(employeeRepo.Object, cqrsRepo.Object);
            await cqrs.OptimizeReadingAsync(1);
            Assert.NotNull(theMappedValue);
            Assert.Equal(employeeToOptimize.Id, theMappedValue.Id);
            Assert.Equal(employeeToOptimize.Name, theMappedValue.Name);
            Assert.Equal(employeeToOptimize.Permissions.Count, theMappedValue.Permissions.Count());
            for (int i = 0; i < 10; i++)
            {
                var curr = theMappedValue.Permissions[i];
                Assert.Equal(i, curr.Id);
                Assert.Equal(i, curr.PermissionTypeId);
                Assert.Equal(i.ToString(), curr.ShortName);
                Assert.Equal($"Desc:{i}", curr.Description);
            }
        }
    }
}