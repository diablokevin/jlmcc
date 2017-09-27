namespace JLMCC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class delFlightInterval : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.FlightIntervals", "NextFlightId", "dbo.Flights");
            DropForeignKey("dbo.FlightIntervals", "PreFlightId", "dbo.Flights");
            DropIndex("dbo.FlightIntervals", new[] { "PreFlightId" });
            DropIndex("dbo.FlightIntervals", new[] { "NextFlightId" });
            DropTable("dbo.FlightIntervals");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.FlightIntervals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PlaneNO = c.String(),
                        PlaneType = c.String(),
                        Type = c.Int(nullable: false),
                        PreFlightId = c.Int(),
                        NextFlightId = c.Int(),
                        Station = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.FlightIntervals", "NextFlightId");
            CreateIndex("dbo.FlightIntervals", "PreFlightId");
            AddForeignKey("dbo.FlightIntervals", "PreFlightId", "dbo.Flights", "FlightId");
            AddForeignKey("dbo.FlightIntervals", "NextFlightId", "dbo.Flights", "FlightId");
        }
    }
}
