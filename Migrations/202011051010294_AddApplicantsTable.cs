namespace CodeTheCloud.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddApplicantsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Applicants",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        FirstName = c.String(nullable: false),
                        Surname = c.String(nullable: false),
                        Gender = c.String(nullable: false),
                        Race = c.String(nullable: false),
                        BirthDate = c.String(nullable: false),
                        IdNumber = c.String(nullable: false, maxLength: 13),
                        ContactNumber = c.String(nullable: false, maxLength: 10),
                        Qualification = c.String(nullable: false),
                        ResidentialAddress = c.String(maxLength: 70),
                        Acknowledgement = c.Boolean(nullable: false),
                        RegistrationDate = c.String(),
                        Availability = c.String(),
                        AdminComments = c.String(),
                        Excluded = c.Boolean(nullable: false),
                        ExclusionReason = c.String(maxLength: 30),
                        ResetDate = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Applicants", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Applicants", new[] { "UserId" });
            DropTable("dbo.Applicants");
        }
    }
}
