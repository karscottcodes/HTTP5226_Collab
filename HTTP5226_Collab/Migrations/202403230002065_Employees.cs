namespace HTTP5226_Collab.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Employees : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        EmployeeId = c.Int(nullable: false, identity: true),
                        EmployeeName = c.String(),
                        TeamId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EmployeeId)
                .ForeignKey("dbo.Teams", t => t.TeamId, cascadeDelete: true)
                .Index(t => t.TeamId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Employees", "TeamId", "dbo.Teams");
            DropIndex("dbo.Employees", new[] { "TeamId" });
            DropTable("dbo.Employees");
        }
    }
}
