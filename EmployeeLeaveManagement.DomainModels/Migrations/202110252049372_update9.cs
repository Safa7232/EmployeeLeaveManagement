namespace EmployeeLeaveManagement.DomainModels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update9 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Leaves", "FirstName", c => c.String());
            AddColumn("dbo.Leaves", "LastName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Leaves", "LastName");
            DropColumn("dbo.Leaves", "FirstName");
        }
    }
}
