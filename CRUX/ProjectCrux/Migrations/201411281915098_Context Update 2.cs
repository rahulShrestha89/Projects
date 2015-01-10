namespace ProjectCrux.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ContextUpdate2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Questions", "rating", c => c.Int(nullable: false));
            AddColumn("dbo.Questions", "visible", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Questions", "visible");
            DropColumn("dbo.Questions", "rating");
        }
    }
}
