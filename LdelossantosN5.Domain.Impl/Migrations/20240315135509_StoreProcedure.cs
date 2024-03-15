using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LdelossantosN5.Domain.Impl.Migrations
{
    /// <inheritdoc />
    public partial class StoreProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //When permissionStatus = denied, deletes the records and returns a negative value
            //When permissionStatus = confirmed, update the records and returns the recordType (i need this value to upsert the Elastic index)
            //If the record it's not found because the Id doesn't exist or don't belong to the employee, returns zero.
            migrationBuilder.Sql(
@"
CREATE OR ALTER PROCEDURE UpdateOrDeleteEmployeePermission
    @Id INT,
    @EmployeeId INT,
    @RequestStatus INT,
    @ReturnValue INT OUTPUT 
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @AffectedRows INT;
    DECLARE @PermissionTypeId INT = 0;

    IF @RequestStatus = 1
    BEGIN
        DELETE FROM SEC_EmployeePermissions
        WHERE Id = @Id AND EmployeeId = @EmployeeId;
        SET @AffectedRows = @@ROWCOUNT;
        IF @AffectedRows > 0
        BEGIN
            SET @ReturnValue = -1;
        END
        ELSE
        BEGIN
            SET @ReturnValue = 0;
        END
    END
    ELSE
    BEGIN
        UPDATE SEC_EmployeePermissions
        SET RequestStatus = 2
        WHERE Id = @Id AND EmployeeId = @EmployeeId;
        SET @AffectedRows = @@ROWCOUNT;
        IF @AffectedRows > 0
        BEGIN
            SELECT @PermissionTypeId = PermissionTypeId FROM SEC_EmployeePermissions WHERE Id = @Id AND EmployeeId = @EmployeeId;
            SET @ReturnValue = @PermissionTypeId;
        END
        ELSE
        BEGIN
            SET @ReturnValue = 0;
        END
    END
END
");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS UpdateOrDeleteEmployeePermission");

        }
    }
}
