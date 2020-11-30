using CodeTheCloud.Models;

namespace CodeTheCloud.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CodeTheCloud.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CodeTheCloud.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            context.Races.AddOrUpdate(p => p.Id,
                new Race { Id = 1, Name = "African" },
                new Race { Id = 2, Name = "Coloured" },
                new Race { Id = 3, Name = "Asian" },
                new Race { Id = 4, Name = "White" }
            );

            context.Qualifications.AddOrUpdate(p => p.Id,
                new Qualification { Id = 1, Name = "None" },
                new Qualification { Id = 2, Name = "Matric"},
                new Qualification { Id = 3, Name = "Certificate"},
                new Qualification { Id = 4, Name = "Diploma" },
                new Qualification { Id = 5, Name = "Degree"},
                new Qualification { Id = 6, Name = "Honours"},
                new Qualification { Id = 7, Name = "Professional Qualification" },
                new Qualification { Id = 8, Name = "Post Graduate"},
                new Qualification { Id = 9, Name = "Masters"},
                new Qualification { Id = 10, Name = "Doctorate"},
                new Qualification { Id = 11, Name = "Other"}
            );

            context.Genders.AddOrUpdate(p => p.Id,
                new Models.Gender { Id = 1, Name = "Male" },
                new Models.Gender { Id = 2, Name = "Female" },
                new Models.Gender { Id = 3, Name = "Intersex" }
            );

        }
    }
}
