namespace JLMCC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeService : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Services", "FlightId", "dbo.Flights");
            DropIndex("dbo.Services", new[] { "FlightId" });
            RenameColumn(table: "dbo.Services", name: "FlightId", newName: "Flight_FlightId");
            AddColumn("dbo.Services", "FlightIntervalId", c => c.Int(nullable: false));
            AlterColumn("dbo.Services", "Flight_FlightId", c => c.Int());
            CreateIndex("dbo.Services", "Flight_FlightId");
            AddForeignKey("dbo.Services", "Flight_FlightId", "dbo.Flights", "FlightId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Services", "Flight_FlightId", "dbo.Flights");
            DropIndex("dbo.Services", new[] { "Flight_FlightId" });
            AlterColumn("dbo.Services", "Flight_FlightId", c => c.Int(nullable: false));
            DropColumn("dbo.Services", "FlightIntervalId");
            RenameColumn(table: "dbo.Services", name: "Flight_FlightId", newName: "FlightId");
            CreateIndex("dbo.Services", "FlightId");
            AddForeignKey("dbo.Services", "FlightId", "dbo.Flights", "FlightId", cascadeDelete: true);
        }
    }
}
