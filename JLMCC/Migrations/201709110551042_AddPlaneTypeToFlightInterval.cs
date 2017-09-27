namespace JLMCC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPlaneTypeToFlightInterval : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FlightIntervals", "PlaneType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.FlightIntervals", "PlaneType");
        }
    }
}
