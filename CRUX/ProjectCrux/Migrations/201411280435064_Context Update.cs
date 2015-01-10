namespace ProjectCrux.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ContextUpdate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Answers",
                c => new
                    {
                        answerId = c.Int(nullable: false, identity: true),
                        answer = c.String(nullable: false),
                        postDate = c.DateTime(nullable: false),
                        questionID = c.Int(),
                    })
                .PrimaryKey(t => t.answerId)
                .ForeignKey("dbo.Questions", t => t.questionID)
                .Index(t => t.questionID);
            
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        questionId = c.Int(nullable: false, identity: true),
                        question = c.String(nullable: false),
                        studentID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.questionId)
                .ForeignKey("dbo.Students", t => t.studentID, cascadeDelete: true)
                .Index(t => t.studentID);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        studentId = c.Int(nullable: false, identity: true),
                        firstName = c.String(nullable: false),
                        middleName = c.String(),
                        lastName = c.String(nullable: false),
                        dateOfBirth = c.DateTime(nullable: false),
                        countryID = c.Int(nullable: false),
                        emailAddress = c.String(nullable: false),
                        userName = c.String(nullable: false),
                        userPassword = c.String(nullable: false, maxLength: 500),
                        confirmPassword = c.String(),
                        schoolID = c.Int(nullable: false),
                        image = c.String(),
                        RememberMe = c.Boolean(nullable: false),
                        verified = c.Boolean(nullable: false),
                        verificationToken = c.String(),
                        salt = c.String(),
                    })
                .PrimaryKey(t => t.studentId)
                .ForeignKey("dbo.Countries", t => t.countryID, cascadeDelete: true)
                .ForeignKey("dbo.Schools", t => t.schoolID, cascadeDelete: true)
                .Index(t => t.countryID)
                .Index(t => t.schoolID);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        countryID = c.Int(nullable: false, identity: true),
                        countryName = c.String(),
                    })
                .PrimaryKey(t => t.countryID);
            
            CreateTable(
                "dbo.Schools",
                c => new
                    {
                        schoolID = c.Int(nullable: false, identity: true),
                        schoolName = c.String(),
                    })
                .PrimaryKey(t => t.schoolID);
            
            CreateTable(
                "dbo.Hashtags",
                c => new
                    {
                        hashtagId = c.Int(nullable: false, identity: true),
                        hashtag = c.String(),
                    })
                .PrimaryKey(t => t.hashtagId);
            
            CreateTable(
                "dbo.QuestionLinkToHashtags",
                c => new
                    {
                        questionLinkToHashtagId = c.Int(nullable: false, identity: true),
                        questionId = c.Int(nullable: false),
                        hashtagsId = c.Int(nullable: false),
                        Answers_answerId = c.Int(),
                        Hashtags_hashtagId = c.Int(),
                    })
                .PrimaryKey(t => t.questionLinkToHashtagId)
                .ForeignKey("dbo.Answers", t => t.Answers_answerId)
                .ForeignKey("dbo.Hashtags", t => t.Hashtags_hashtagId)
                .ForeignKey("dbo.Questions", t => t.questionId, cascadeDelete: true)
                .Index(t => t.questionId)
                .Index(t => t.Answers_answerId)
                .Index(t => t.Hashtags_hashtagId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.QuestionLinkToHashtags", "questionId", "dbo.Questions");
            DropForeignKey("dbo.QuestionLinkToHashtags", "Hashtags_hashtagId", "dbo.Hashtags");
            DropForeignKey("dbo.QuestionLinkToHashtags", "Answers_answerId", "dbo.Answers");
            DropForeignKey("dbo.Students", "schoolID", "dbo.Schools");
            DropForeignKey("dbo.Questions", "studentID", "dbo.Students");
            DropForeignKey("dbo.Students", "countryID", "dbo.Countries");
            DropForeignKey("dbo.Answers", "questionID", "dbo.Questions");
            DropIndex("dbo.QuestionLinkToHashtags", new[] { "Hashtags_hashtagId" });
            DropIndex("dbo.QuestionLinkToHashtags", new[] { "Answers_answerId" });
            DropIndex("dbo.QuestionLinkToHashtags", new[] { "questionId" });
            DropIndex("dbo.Students", new[] { "schoolID" });
            DropIndex("dbo.Students", new[] { "countryID" });
            DropIndex("dbo.Questions", new[] { "studentID" });
            DropIndex("dbo.Answers", new[] { "questionID" });
            DropTable("dbo.QuestionLinkToHashtags");
            DropTable("dbo.Hashtags");
            DropTable("dbo.Schools");
            DropTable("dbo.Countries");
            DropTable("dbo.Students");
            DropTable("dbo.Questions");
            DropTable("dbo.Answers");
        }
    }
}
