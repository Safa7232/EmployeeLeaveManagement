namespace EmployeeLeaveManagement.DomainModels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update11 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Leaves", "FirstName");
            DropColumn("dbo.Leaves", "LastName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Leaves", "LastName", c => c.String());
            AddColumn("dbo.Leaves", "FirstName", c => c.String());
        }
    }
}
