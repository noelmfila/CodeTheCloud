namespace CodeTheCloud.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddQualificationsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Qualifications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Qualifications");
        }
    }
}
