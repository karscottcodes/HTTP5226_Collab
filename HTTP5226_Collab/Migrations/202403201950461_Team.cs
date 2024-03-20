namespace HTTP5226_Collab.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Team : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Teams",
                c => new
                    {
                        TeamId = c.Int(nullable: false, identity: true),
                        TeamName = c.String(),
                        DepartmentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TeamId)
                .ForeignKey("dbo.Departments", t => t.DepartmentId, cascadeDelete: true)
                .Index(t => t.DepartmentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Teams", "DepartmentId", "dbo.Departments");
            DropIndex("dbo.Teams", new[] { "DepartmentId" });
            DropTable("dbo.Teams");
        }
    }
}
