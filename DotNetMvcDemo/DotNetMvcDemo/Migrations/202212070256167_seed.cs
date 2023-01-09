using System.Data.Entity.Migrations;

namespace DotNetMvcDemo.Migrations
{
    public partial class seed : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admins",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    AdminCardNumber = c.String(),
                    FirstName = c.String(nullable: false, maxLength: 50),
                    LastName = c.String(nullable: false, maxLength: 50),
                    Salary = c.Int(nullable: false),
                    CreatedAt = c.DateTime(nullable: false),
                    UpdatedAt = c.DateTime(nullable: false),
                    CreatedBy = c.Int(nullable: false),
                    UpdatedBy = c.Int(nullable: false),
                    UserId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AuthUsers", t => t.UserId)
                .ForeignKey("dbo.AuthUsers", t => t.CreatedBy)
                .ForeignKey("dbo.AuthUsers", t => t.UpdatedBy)
                .Index(t => t.CreatedBy)
                .Index(t => t.UpdatedBy)
                .Index(t => t.UserId);

            CreateTable(
                "dbo.AuthUsers",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    UserName = c.String(),
                    Password = c.String(),
                    IsAdmin = c.Boolean(nullable: false),
                    CreatedAt = c.DateTime(nullable: false),
                    UpdatedAt = c.DateTime(nullable: false),
                    CreatedBy = c.Int(),
                    UpdatedBy = c.Int(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Courses",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 50),
                    Credit = c.Int(nullable: false),
                    DepartmentId = c.Int(nullable: false),
                    CreatedAt = c.DateTime(nullable: false),
                    UpdatedAt = c.DateTime(nullable: false),
                    CreatedBy = c.Int(nullable: false),
                    UpdatedBy = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AuthUsers", t => t.CreatedBy)
                .ForeignKey("dbo.Departments", t => t.DepartmentId, cascadeDelete: true)
                .ForeignKey("dbo.AuthUsers", t => t.UpdatedBy)
                .Index(t => t.DepartmentId)
                .Index(t => t.CreatedBy)
                .Index(t => t.UpdatedBy);

            CreateTable(
                "dbo.Departments",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 50),
                    Description = c.String(nullable: false, maxLength: 100),
                    CreatedAt = c.DateTime(nullable: false),
                    UpdatedAt = c.DateTime(nullable: false),
                    CreatedBy = c.Int(nullable: false),
                    UpdatedBy = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AuthUsers", t => t.CreatedBy)
                .ForeignKey("dbo.AuthUsers", t => t.UpdatedBy)
                .Index(t => t.CreatedBy)
                .Index(t => t.UpdatedBy);

            CreateTable(
                "dbo.Students",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    FirstName = c.String(nullable: false, maxLength: 50),
                    MiddleName = c.String(maxLength: 50),
                    LastName = c.String(nullable: false, maxLength: 50),
                    StudentCardNumber = c.String(),
                    EnrollmentDate = c.DateTime(nullable: false),
                    DepartmentId = c.Int(nullable: false),
                    CreatedAt = c.DateTime(nullable: false),
                    UpdatedAt = c.DateTime(nullable: false),
                    CreatedBy = c.Int(nullable: false),
                    UpdatedBy = c.Int(nullable: false),
                    UserId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AuthUsers", t => t.CreatedBy)
                .ForeignKey("dbo.Departments", t => t.DepartmentId, cascadeDelete: true)
                .ForeignKey("dbo.AuthUsers", t => t.UpdatedBy)
                .ForeignKey("dbo.AuthUsers", t => t.UserId)
                .Index(t => t.DepartmentId)
                .Index(t => t.CreatedBy)
                .Index(t => t.UpdatedBy)
                .Index(t => t.UserId);

            CreateTable(
                "dbo.Enrollments",
                c => new
                {
                    CourseId = c.Int(nullable: false),
                    StudentId = c.Int(nullable: false),
                    CourseEnrollDate = c.DateTime(nullable: false),
                    CreatedAt = c.DateTime(nullable: false),
                    UpdatedAt = c.DateTime(nullable: false),
                    CreatedBy = c.Int(nullable: false),
                    UpdatedBy = c.Int(nullable: false),
                })
                .PrimaryKey(t => new { t.CourseId, t.StudentId })
                .ForeignKey("dbo.Courses", t => t.CourseId)
                .ForeignKey("dbo.AuthUsers", t => t.CreatedBy)
                .ForeignKey("dbo.Students", t => t.StudentId)
                .ForeignKey("dbo.AuthUsers", t => t.UpdatedBy)
                .Index(t => t.CourseId)
                .Index(t => t.StudentId)
                .Index(t => t.CreatedBy)
                .Index(t => t.UpdatedBy);

            CreateTable(
                "dbo.Teachers",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    TeacherCardNumber = c.String(),
                    IsInProbation = c.Boolean(nullable: false),
                    FirstName = c.String(nullable: false, maxLength: 50),
                    LastName = c.String(nullable: false, maxLength: 50),
                    Salary = c.Int(nullable: false),
                    CreatedAt = c.DateTime(nullable: false),
                    UpdatedAt = c.DateTime(nullable: false),
                    CreatedBy = c.Int(nullable: false),
                    UpdatedBy = c.Int(nullable: false),
                    UserId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AuthUsers", t => t.CreatedBy)
                .ForeignKey("dbo.AuthUsers", t => t.UpdatedBy)
                .ForeignKey("dbo.AuthUsers", t => t.UserId)
                .Index(t => t.CreatedBy)
                .Index(t => t.UpdatedBy)
                .Index(t => t.UserId);

            CreateTable(
                "dbo.UserProfiles",
                c => new
                {
                    Id = c.Int(nullable: false),
                    UserPhoto = c.Binary(),
                    Gender = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AuthUsers", t => t.Id)
                .Index(t => t.Id);

            CreateTable(
                "dbo.EmailAddresses",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Email = c.String(),
                    IsPrimary = c.Boolean(nullable: false),
                    UserProfile_Id = c.Int(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserProfiles", t => t.UserProfile_Id)
                .Index(t => t.UserProfile_Id);

            CreateTable(
                "dbo.MobileNumbers",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Number = c.String(),
                    IsPrimary = c.Boolean(nullable: false),
                    UserProfile_Id = c.Int(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserProfiles", t => t.UserProfile_Id)
                .Index(t => t.UserProfile_Id);

            CreateTable(
                "dbo.DepartmentAdmins",
                c => new
                {
                    DepartmentRefId = c.Int(nullable: false),
                    AdminsRefId = c.Int(nullable: false),
                })
                .PrimaryKey(t => new { t.DepartmentRefId, t.AdminsRefId })
                .ForeignKey("dbo.Departments", t => t.DepartmentRefId, cascadeDelete: true)
                .ForeignKey("dbo.Admins", t => t.AdminsRefId, cascadeDelete: true)
                .Index(t => t.DepartmentRefId)
                .Index(t => t.AdminsRefId);

            CreateTable(
                "dbo.DepartmentTeacher",
                c => new
                {
                    DepartmentRefId = c.Int(nullable: false),
                    TeacherRefId = c.Int(nullable: false),
                })
                .PrimaryKey(t => new { t.DepartmentRefId, t.TeacherRefId })
                .ForeignKey("dbo.Departments", t => t.DepartmentRefId, cascadeDelete: true)
                .ForeignKey("dbo.Teachers", t => t.TeacherRefId, cascadeDelete: true)
                .Index(t => t.DepartmentRefId)
                .Index(t => t.TeacherRefId);

        }

        public override void Down()
        {
            DropForeignKey("dbo.Admins", "UpdatedBy", "dbo.AuthUsers");
            DropForeignKey("dbo.Admins", "CreatedBy", "dbo.AuthUsers");
            DropForeignKey("dbo.UserProfiles", "Id", "dbo.AuthUsers");
            DropForeignKey("dbo.MobileNumbers", "UserProfile_Id", "dbo.UserProfiles");
            DropForeignKey("dbo.EmailAddresses", "UserProfile_Id", "dbo.UserProfiles");
            DropForeignKey("dbo.Teachers", "UserId", "dbo.AuthUsers");
            DropForeignKey("dbo.Students", "UserId", "dbo.AuthUsers");
            DropForeignKey("dbo.Courses", "UpdatedBy", "dbo.AuthUsers");
            DropForeignKey("dbo.Departments", "UpdatedBy", "dbo.AuthUsers");
            DropForeignKey("dbo.DepartmentTeacher", "TeacherRefId", "dbo.Teachers");
            DropForeignKey("dbo.DepartmentTeacher", "DepartmentRefId", "dbo.Departments");
            DropForeignKey("dbo.Teachers", "UpdatedBy", "dbo.AuthUsers");
            DropForeignKey("dbo.Teachers", "CreatedBy", "dbo.AuthUsers");
            DropForeignKey("dbo.Students", "UpdatedBy", "dbo.AuthUsers");
            DropForeignKey("dbo.Enrollments", "UpdatedBy", "dbo.AuthUsers");
            DropForeignKey("dbo.Enrollments", "StudentId", "dbo.Students");
            DropForeignKey("dbo.Enrollments", "CreatedBy", "dbo.AuthUsers");
            DropForeignKey("dbo.Enrollments", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.Students", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.Students", "CreatedBy", "dbo.AuthUsers");
            DropForeignKey("dbo.Departments", "CreatedBy", "dbo.AuthUsers");
            DropForeignKey("dbo.Courses", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.DepartmentAdmins", "AdminsRefId", "dbo.Admins");
            DropForeignKey("dbo.DepartmentAdmins", "DepartmentRefId", "dbo.Departments");
            DropForeignKey("dbo.Courses", "CreatedBy", "dbo.AuthUsers");
            DropForeignKey("dbo.Admins", "UserId", "dbo.AuthUsers");
            DropIndex("dbo.DepartmentTeacher", new[] { "TeacherRefId" });
            DropIndex("dbo.DepartmentTeacher", new[] { "DepartmentRefId" });
            DropIndex("dbo.DepartmentAdmins", new[] { "AdminsRefId" });
            DropIndex("dbo.DepartmentAdmins", new[] { "DepartmentRefId" });
            DropIndex("dbo.MobileNumbers", new[] { "UserProfile_Id" });
            DropIndex("dbo.EmailAddresses", new[] { "UserProfile_Id" });
            DropIndex("dbo.UserProfiles", new[] { "Id" });
            DropIndex("dbo.Teachers", new[] { "UserId" });
            DropIndex("dbo.Teachers", new[] { "UpdatedBy" });
            DropIndex("dbo.Teachers", new[] { "CreatedBy" });
            DropIndex("dbo.Enrollments", new[] { "UpdatedBy" });
            DropIndex("dbo.Enrollments", new[] { "CreatedBy" });
            DropIndex("dbo.Enrollments", new[] { "StudentId" });
            DropIndex("dbo.Enrollments", new[] { "CourseId" });
            DropIndex("dbo.Students", new[] { "UserId" });
            DropIndex("dbo.Students", new[] { "UpdatedBy" });
            DropIndex("dbo.Students", new[] { "CreatedBy" });
            DropIndex("dbo.Students", new[] { "DepartmentId" });
            DropIndex("dbo.Departments", new[] { "UpdatedBy" });
            DropIndex("dbo.Departments", new[] { "CreatedBy" });
            DropIndex("dbo.Courses", new[] { "UpdatedBy" });
            DropIndex("dbo.Courses", new[] { "CreatedBy" });
            DropIndex("dbo.Courses", new[] { "DepartmentId" });
            DropIndex("dbo.Admins", new[] { "UserId" });
            DropIndex("dbo.Admins", new[] { "UpdatedBy" });
            DropIndex("dbo.Admins", new[] { "CreatedBy" });
            DropTable("dbo.DepartmentTeacher");
            DropTable("dbo.DepartmentAdmins");
            DropTable("dbo.MobileNumbers");
            DropTable("dbo.EmailAddresses");
            DropTable("dbo.UserProfiles");
            DropTable("dbo.Teachers");
            DropTable("dbo.Enrollments");
            DropTable("dbo.Students");
            DropTable("dbo.Departments");
            DropTable("dbo.Courses");
            DropTable("dbo.AuthUsers");
            DropTable("dbo.Admins");
        }
    }
}
