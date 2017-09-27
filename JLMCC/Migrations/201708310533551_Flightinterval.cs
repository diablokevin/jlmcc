namespace JLMCC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Flightinterval : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FlightIntervals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PlaneNO = c.String(),
                        Type = c.Int(nullable: false),
                        PreFlightId = c.Int(),
                        NextFlightId = c.Int(),
                        Station = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Flights", t => t.NextFlightId)
                .ForeignKey("dbo.Flights", t => t.PreFlightId)
                .Index(t => t.PreFlightId)
                .Index(t => t.NextFlightId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FlightIntervals", "PreFlightId", "dbo.Flights");
            DropForeignKey("dbo.FlightIntervals", "NextFlightId", "dbo.Flights");
            DropIndex("dbo.FlightIntervals", new[] { "NextFlightId" });
            DropIndex("dbo.FlightIntervals", new[] { "PreFlightId" });
            DropTable("dbo.FlightIntervals");
        }
    }
}
