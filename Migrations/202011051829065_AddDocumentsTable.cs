namespace CodeTheCloud.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDocumentsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Documents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ApplicantId = c.Int(nullable: false),
                        Description = c.String(nullable: false),
                        FileName = c.String(nullable: false),
                        UploadDate = c.DateTime(nullable: false),
                        FilePath = c.String(),
                        AzureFileName = c.String(),
                        FileExtension = c.String(),
                        UserId = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Applicants", t => t.ApplicantId, cascadeDelete: true)
                .Index(t => t.ApplicantId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Documents", "ApplicantId", "dbo.Applicants");
            DropIndex("dbo.Documents", new[] { "ApplicantId" });
            DropTable("dbo.Documents");
        }
    }
}
