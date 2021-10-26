﻿namespace EmployeeLeaveManagement.DomainModels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update6 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Leaves", "EmployeeName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Leaves", "EmployeeName");
        }
    }
}
