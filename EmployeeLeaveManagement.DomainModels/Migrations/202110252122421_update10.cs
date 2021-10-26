namespace EmployeeLeaveManagement.DomainModels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update10 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Leaves", "TimeSpan");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Leaves", "TimeSpan", c => c.DateTime(nullable: false));
        }
    }
}
