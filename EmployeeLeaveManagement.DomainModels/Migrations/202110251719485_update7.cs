namespace EmployeeLeaveManagement.DomainModels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update7 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Leaves", "EmployeeName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Leaves", "EmployeeName", c => c.String());
        }
    }
}
