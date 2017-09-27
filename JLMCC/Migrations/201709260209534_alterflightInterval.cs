namespace JLMCC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alterflightInterval : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Services", "Flight_FlightId", "dbo.Flights");
            DropIndex("dbo.Services", new[] { "Flight_FlightId" });
            RenameColumn(table: "dbo.Services", name: "Flight_FlightId", newName: "FlightId");
            AlterColumn("dbo.Services", "FlightId", c => c.Int(nullable: false));
            CreateIndex("dbo.Services", "FlightId");
            AddForeignKey("dbo.Services", "FlightId", "dbo.Flights", "FlightId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Services", "FlightId", "dbo.Flights");
            DropIndex("dbo.Services", new[] { "FlightId" });
            AlterColumn("dbo.Services", "FlightId", c => c.Int());
            RenameColumn(table: "dbo.Services", name: "FlightId", newName: "Flight_FlightId");
            CreateIndex("dbo.Services", "Flight_FlightId");
            AddForeignKey("dbo.Services", "Flight_FlightId", "dbo.Flights", "FlightId");
        }
    }
}
