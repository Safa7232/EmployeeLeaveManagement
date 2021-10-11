namespace EmployeeLeaveManagement.DomainModels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "ImageURL", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employees", "ImageURL");
        }
    }
}
