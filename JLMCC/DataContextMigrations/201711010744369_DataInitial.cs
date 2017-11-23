namespace JLMCC.DataContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DataInitial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SubDepartments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        DepartmentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Departments", t => t.DepartmentId, cascadeDelete: true)
                .Index(t => t.DepartmentId);
            
            CreateTable(
                "dbo.Flights",
                c => new
                    {
                        FlightId = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        FlightNO = c.String(),
                        PlaneNO = c.String(),
                        PlaneType = c.String(),
                        DepartureCity = c.String(),
                        DepartureStandNO = c.String(),
                        ScheduleDeparture = c.DateTime(nullable: false),
                        ArriveCity = c.String(),
                        ArriveStandNO = c.String(),
                        ScheduleArrive = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.FlightId);
            
            CreateTable(
                "dbo.Services",
                c => new
                    {
                        ServiceId = c.Int(nullable: false, identity: true),
                        Content = c.String(),
                        StaffId = c.String(),
                        Name = c.String(),
                        ServiceTime = c.DateTime(nullable: false),
                        Type = c.Int(nullable: false),
                        FlightId = c.Int(nullable: false),
                        FlightIntervalId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ServiceId)
                .ForeignKey("dbo.Flights", t => t.FlightId, cascadeDelete: true)
                .Index(t => t.FlightId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Services", "FlightId", "dbo.Flights");
            DropForeignKey("dbo.SubDepartments", "DepartmentId", "dbo.Departments");
            DropIndex("dbo.Services", new[] { "FlightId" });
            DropIndex("dbo.SubDepartments", new[] { "DepartmentId" });
            DropTable("dbo.Services");
            DropTable("dbo.Flights");
            DropTable("dbo.SubDepartments");
            DropTable("dbo.Departments");
        }
    }
}
