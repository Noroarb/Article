namespace Article.Data.Migrations
{
    using Article.Common;
    using Article.Domain.Entities;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            // USERNAME : Admin
            // PASSWORD : 1q2w!Q@W
            SeedAdminUserAndRole(context);

        }

        private Guid ADMIN_ROLE_ID = Guid.Parse("aaaaaaaa-555d-40ff-85d5-8342ebc5f32c");
        private Guid SupervisorRole_ID = Guid.Parse("cccccccc-555d-40ff-85d5-8342ebc5f32c");
        private Guid WriterRole_ID = Guid.Parse("eddddddd-555d-40ff-85d5-8342ebc5f32c");
        private Guid UserRole_ID = Guid.Parse("fddddddd-555d-40ff-85d5-8342ebc5f32c");
        private Guid ADMIN_USER_ID = Guid.Parse("00000000-555d-40ff-85d5-8342ebc5f32c");
       
        private void SeedAdminUserAndRole(ApplicationDbContext context)
        {
            
            if (!context.Roles.Where(r=>r.RoleId==ADMIN_ROLE_ID).Any())
            {
               

                var AdminRole=new Role { RoleId = ADMIN_ROLE_ID, Name = Roles.AdminRole };
                context.Roles.AddOrUpdate(
                        r => r.Name,
                        AdminRole
                    );
              
                var SupervisorRole = new Role { RoleId = SupervisorRole_ID, Name = Roles.SupervisorRole };
                context.Roles.AddOrUpdate(
                        r => r.Name,
                        SupervisorRole
                    );
                var WriterRole = new Role { RoleId = WriterRole_ID, Name = Roles.WriterRole };
                context.Roles.AddOrUpdate(
                        r => r.Name,
                        WriterRole
                    );
                var UserRole = new Role { RoleId = UserRole_ID, Name = Roles.UserRole };
                context.Roles.AddOrUpdate(
                        r => r.Name,
                        UserRole
                    );

                context.SaveChanges();

            context.Users.AddOrUpdate(
                u => u.UserName,
                new User { UserId = ADMIN_USER_ID, UserName = "Admin", Email = "admin@core.com", CreationDate = Utils.ServerNow, FullName = "Administrator", PasswordHash = "AAhJLnBd0DCF7ZEABUCeih2bWJNRSM3eJ+kVCSdEcjM7TpwibUTfc4Ukssv9uxKrdw==", SecurityStamp = "75a7bfb7-9db7-44dd-a2c0-c1857ebc9f67" }
            );
            context.SaveChanges();

            context.Users.Where(u => u.UserId.ToString() == ADMIN_USER_ID.ToString()).SingleOrDefault().Roles.Add(AdminRole);
                context.SaveChanges();

              
            }

        }
    }
}
