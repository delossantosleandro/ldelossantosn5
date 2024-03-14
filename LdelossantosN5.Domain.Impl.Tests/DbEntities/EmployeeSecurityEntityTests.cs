using LdelossantosN5.Domain.Impl.DbEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LdelossantosN5.Domain.Tests.DbEntities
{
    public class EmployeeSecurityEntityTests
        : IClassFixture<SqlLocalDbFixture>
    {
        public SqlLocalDbFixture DbFixture { get; }
        public EmployeeSecurityEntityTests(SqlLocalDbFixture dbFixture)
        {
            this.DbFixture = dbFixture;
        }

        [Fact]
        public async Task CreateEmployee()
        {
            //Create and check
            using (var DbContext = this.DbFixture.CreateContext())
            {
                var newEmployee = new EmployeeSecurityEntity();
                newEmployee.Name = "Some Name";
                newEmployee.StartDate = DateTime.Now.Date.AddYears(-5);
                DbContext.EmployeeSecuritySet.Add(newEmployee);
                await DbContext.SaveChangesAsync();
                Assert.True(newEmployee.Id > 0);
                Assert.True(newEmployee.Timestamp.Length > 0);
            }
        }

        [Fact]
        public async Task UpdateEmployee()
        {
            var newID = 0;
            var newName = DateTime.Now.ToString(); ;
            //Create a new employee
            using (var DbContext = this.DbFixture.CreateContext())
            {
                var newEmployee = new EmployeeSecurityEntity();
                newEmployee.Name = "Some Name";
                newEmployee.StartDate = DateTime.Now.Date.AddYears(-5);
                DbContext.EmployeeSecuritySet.Add(newEmployee);
                await DbContext.SaveChangesAsync();
                newID = newEmployee.Id;
            }

            //Find and update
            using (var DbContext = this.DbFixture.CreateContext())
            {
                var existingEmployee = await DbContext.EmployeeSecuritySet.FindAsync(newID);
                Assert.NotNull(existingEmployee);

                existingEmployee.Name = newName;
                await DbContext.SaveChangesAsync();
            }

            //Check for the changes
            using (var DbContext = this.DbFixture.CreateContext())
            {
                var existingEmployee = await DbContext.EmployeeSecuritySet.FindAsync(newID);
                Assert.NotNull(existingEmployee);
                Assert.Equal(newName, existingEmployee.Name);
            }
        }

        [Fact]
        public async Task RemoveEmployee()
        {
            var newID = 0;
            //Create the new employee
            using (var DbContext = this.DbFixture.CreateContext())
            {
                var newEmployee = new EmployeeSecurityEntity();
                newEmployee.Name = "Some Name";
                newEmployee.StartDate = DateTime.Now.Date.AddYears(-5);
                DbContext.EmployeeSecuritySet.Add(newEmployee);
                await DbContext.SaveChangesAsync();
                newID = newEmployee.Id;
            }

            //find and remove
            using (var dbContext = this.DbFixture.CreateContext())
            {
                var toRemove = await dbContext.EmployeeSecuritySet.FindAsync(newID);
                Assert.NotNull(toRemove);
                dbContext.EmployeeSecuritySet.Remove(toRemove);
                await dbContext.SaveChangesAsync();
            }

            //Check if it's removed
            using (var dbContext = this.DbFixture.CreateContext())
            {
                var removed = await dbContext.EmployeeSecuritySet.FindAsync(newID);
                Assert.Null(removed);
            }
        }
    }
}
