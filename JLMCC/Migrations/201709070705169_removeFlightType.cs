namespace JLMCC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeFlightType : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Flights", "Type");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Flights", "Type", c => c.String());
        }
    }
}
