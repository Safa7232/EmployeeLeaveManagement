

    namespace EmployeeLeaveManagement.DomainModels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Leaves", "StartDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Leaves", "EndDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Leaves", "LeaveReason", c => c.String());
            DropColumn("dbo.Leaves", "TotalLeave");
            DropColumn("dbo.Leaves", "LeaveTaken");
            DropColumn("dbo.Leaves", "AvailableLeave");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Leaves", "AvailableLeave", c => c.Int(nullable: false));
            AddColumn("dbo.Leaves", "LeaveTaken", c => c.Int(nullable: false));
            AddColumn("dbo.Leaves", "TotalLeave", c => c.Int(nullable: false));
            DropColumn("dbo.Leaves", "LeaveReason");
            DropColumn("dbo.Leaves", "EndDate");
            DropColumn("dbo.Leaves", "StartDate");
        }
    }
}
