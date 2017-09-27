namespace JLMCC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alterflightInterval2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Services", "Type", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Services", "Type");
        }
    }
}
