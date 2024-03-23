namespace HTTP5226_Collab.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Assignment : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Assignments",
                c => new
                    {
                        AssignmentId = c.Int(nullable: false, identity: true),
                        AssignmentName = c.String(),
                        AssignmentStatus = c.String(),
                        AssignmentValue = c.Int(nullable: false),
                        EmployeeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AssignmentId)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .Index(t => t.EmployeeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Assignments", "EmployeeId", "dbo.Employees");
            DropIndex("dbo.Assignments", new[] { "EmployeeId" });
            DropTable("dbo.Assignments");
        }
    }
}
