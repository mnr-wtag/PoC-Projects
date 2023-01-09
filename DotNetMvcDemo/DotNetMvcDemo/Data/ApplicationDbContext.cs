using DotNetMvcDemo.Models;
using System.Data.Entity;

namespace DotNetMvcDemo.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("name=DotNetMvcDb")
        {

        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Student> Students { get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<Enrollment> Enrollments { get; set; }

        public DbSet<UserProfile> UserProfiles { get; set; }

        public DbSet<AuthUser> AuthUsers { get; set; }

        public DbSet<MobileNumber> MobileNumbers { get; set; }
        public DbSet<EmailAddress> EmailAddresses { get; set; }

        //public DbSet<PermenantAddress> PermenantAddresses { get; set; }
        //public DbSet<CurrentAddress> CurrentAddresses { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<ApplicationDbContext>());
            // Database.SetInitializer(new ApplicationDbInitializer());

            #region AuthUser 1 - 1 Student/teacher/admin

            //modelBuilder.Entity<Student>()
            //    .HasRequired<AuthUser>(s => s.User).WithRequiredDependent(x=>x.Student);

            //          //   .Map(x => x.MapKey("UserId"));

            //          modelBuilder.Entity<Teacher>()
            //              .HasRequired<AuthUser>(s => s.User).WithRequiredDependent(x=>x.Teacher);

            //            //.Map(x => x.MapKey("UserId"));

            //            modelBuilder.Entity<Admin>()
            //                .HasRequired<AuthUser>(s => s.User).WithRequiredDependent(x=>x.Admin);

            //           // .Map(x => x.MapKey("UserId"));

            #endregion


            #region Department * - * Teacher

            modelBuilder.Entity<Department>()
                .HasMany<Teacher>(s => s.Teachers)
                .WithMany(c => c.Departments)
                .Map(cs =>
                {
                    cs.MapLeftKey("DepartmentRefId");
                    cs.MapRightKey("TeacherRefId");
                    cs.ToTable("DepartmentTeacher");
                });

            #endregion

            #region Department * - * Admin

            modelBuilder.Entity<Department>()
                .HasMany<Admin>(s => s.Admins)
                .WithMany(c => c.Departments)
                .Map(cs =>
                {
                    cs.MapLeftKey("DepartmentRefId");
                    cs.MapRightKey("AdminsRefId");
                    cs.ToTable("DepartmentAdmins");
                });

            #endregion

            #region AuthUser 1-0..1 UserProfile

            modelBuilder.Entity<AuthUser>()
                        .HasOptional(p => p.UserProfile)
                        .WithRequired(u => u.AuthUser);

            #endregion


            #region Enrollment

            modelBuilder.Entity<Enrollment>()
                        .HasKey(bc => new { bc.CourseId, bc.StudentId });

            modelBuilder.Entity<Enrollment>()
                        .HasRequired(bc => bc.Course)
                        .WithMany(b => b.Enrollments)
                        .HasForeignKey(bc => bc.CourseId)
                        .WillCascadeOnDelete(false);

            modelBuilder.Entity<Enrollment>()
                        .HasRequired(bc => bc.Student)
                        .WithMany(c => c.Enrollments)
                        .HasForeignKey(bc => bc.StudentId)
                        .WillCascadeOnDelete(false);

            #endregion


            #region Student auditable

            modelBuilder.Entity<Student>()
                        .HasRequired<AuthUser>(s => s.UpdaterUser)
                        .WithMany(ad => ad.StudentsUpdated)
                        .HasForeignKey(ad => ad.UpdatedBy)
                        .WillCascadeOnDelete(false);

            modelBuilder.Entity<Student>()
                       .HasRequired<AuthUser>(s => s.CreatorUser)
                       .WithMany(ad => ad.StudentsCreated)
                       .HasForeignKey(ad => ad.CreatedBy)
                       .WillCascadeOnDelete(false);

            #endregion


            #region Teacher auditable

            modelBuilder.Entity<Teacher>()
                        .HasRequired<AuthUser>(s => s.UpdaterUser)
                        .WithMany(ad => ad.TeachersUpdated)
                        .HasForeignKey(ad => ad.UpdatedBy)
                        .WillCascadeOnDelete(false);

            modelBuilder.Entity<Teacher>()
                       .HasRequired<AuthUser>(s => s.CreatorUser)
                       .WithMany(ad => ad.TeachersCreated)
                       .HasForeignKey(ad => ad.CreatedBy)
                       .WillCascadeOnDelete(false);

            #endregion


            #region Course auditable

            modelBuilder.Entity<Course>()
                       .HasRequired<AuthUser>(s => s.UpdaterUser)
                       .WithMany(ad => ad.CoursesUpdated)
                       .HasForeignKey(ad => ad.UpdatedBy)
                       .WillCascadeOnDelete(false);

            modelBuilder.Entity<Course>()
                       .HasRequired<AuthUser>(s => s.CreatorUser)
                       .WithMany(ad => ad.CoursesCreated)
                       .HasForeignKey(ad => ad.CreatedBy)
                       .WillCascadeOnDelete(false);

            #endregion


            #region Admin auditable

            modelBuilder.Entity<Admin>()
                        .HasRequired<AuthUser>(s => s.UpdaterUser)
                        .WithMany(ad => ad.AdminsUpdated)
                        .HasForeignKey(ad => ad.UpdatedBy)
                        .WillCascadeOnDelete(false);

            modelBuilder.Entity<Admin>()
                       .HasRequired<AuthUser>(s => s.CreatorUser)
                       .WithMany(ad => ad.AdminsCreated)
                       .HasForeignKey(ad => ad.CreatedBy)
                       .WillCascadeOnDelete(false);

            #endregion


            #region Enrollment auditable

            modelBuilder.Entity<Enrollment>()
                       .HasRequired<AuthUser>(s => s.UpdaterUser)
                       .WithMany(ad => ad.EnrollmentsUpdated)
                       .HasForeignKey(ad => ad.UpdatedBy)
                       .WillCascadeOnDelete(false);

            modelBuilder.Entity<Enrollment>()
                        .HasRequired<AuthUser>(s => s.CreatorUser)
                        .WithMany(ad => ad.EnrollmentsCreated)
                        .HasForeignKey(ad => ad.CreatedBy)
                        .WillCascadeOnDelete(false);

            #endregion


            #region Department auditable

            modelBuilder.Entity<Department>()
                        .HasRequired<AuthUser>(s => s.UpdaterUser)
                        .WithMany(ad => ad.DepartmentsUpdated)
                        .HasForeignKey(ad => ad.UpdatedBy)
                        .WillCascadeOnDelete(false);

            modelBuilder.Entity<Department>()
                        .HasRequired<AuthUser>(s => s.CreatorUser)
                        .WithMany(ad => ad.DepartmentsCreated)
                        .HasForeignKey(ad => ad.CreatedBy)
                        .WillCascadeOnDelete(false);

            #endregion

        }


    }
}