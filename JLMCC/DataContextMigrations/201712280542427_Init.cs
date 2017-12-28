namespace JLMCC.DataContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
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
                "dbo.FlightInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SoflSeqNr = c.Int(nullable: false),
                        AlnCd = c.String(),
                        FltDt = c.DateTime(),
                        FltNr = c.String(),
                        LegSeqNr = c.String(),
                        OpSuffix = c.String(),
                        SvcType = c.String(),
                        SvcChnDesc = c.String(),
                        LatestTailNr = c.String(),
                        LatestEqpCd = c.String(),
                        ScheduledEqpCd = c.String(),
                        LegStsCd = c.String(),
                        LegStsChn = c.String(),
                        AcfOper = c.String(),
                        DopsTmst = c.DateTime(),
                        DeleteInd = c.String(),
                        CnclCd = c.String(),
                        BranchCode = c.String(),
                        DepArpCd = c.String(),
                        SchDepCityName = c.String(),
                        DepStsCd = c.String(),
                        LatestDepArpCd = c.String(),
                        ArcDepCityName = c.String(),
                        DepStandNo = c.String(),
                        DepGateNo = c.String(),
                        SchDepDt = c.DateTime(),
                        LocSchDepDt = c.DateTime(),
                        LatestDepDt = c.DateTime(),
                        LocLatestDepDt = c.DateTime(),
                        ActualOffblocks = c.DateTime(),
                        TaxiOutTm = c.Int(),
                        ActualAirborne = c.DateTime(),
                        AirTm = c.DateTime(),
                        LocAirTm = c.DateTime(),
                        ArvArpCd = c.String(),
                        SchArvCityName = c.String(),
                        LatestArvArpCd = c.String(),
                        ArcArvCityName = c.String(),
                        SchArvDt = c.DateTime(),
                        LocSchArvDt = c.DateTime(),
                        latestArvDt = c.DateTime(),
                        locLatestArvDt = c.DateTime(),
                        ArvStandNo = c.String(),
                        ArvGateNo = c.String(),
                        ArvStsCd = c.String(),
                        DwnTm = c.DateTime(),
                        LocDwnTm = c.DateTime(),
                        ActualLanding = c.DateTime(),
                        TaxiInTm = c.Int(),
                        ActualOnblocks = c.DateTime(),
                        LatestDlyCd = c.String(),
                        DelayTime = c.Int(),
                        DlyCd1 = c.String(),
                        DlyTm1 = c.Int(),
                        DlyReasonDetail01 = c.String(),
                        DlyReasonPublish01 = c.String(),
                        DlyCd2 = c.String(),
                        DlyTm2 = c.Int(),
                        DlyReasonDetail02 = c.String(),
                        DlyReasonPublish02 = c.String(),
                        TailCompany = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.FltDt);
            
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
                .ForeignKey("dbo.FlightInfoes", t => t.FlightId, cascadeDelete: true)
                .Index(t => t.FlightId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Services", "FlightId", "dbo.FlightInfoes");
            DropForeignKey("dbo.SubDepartments", "DepartmentId", "dbo.Departments");
            DropIndex("dbo.Services", new[] { "FlightId" });
            DropIndex("dbo.FlightInfoes", new[] { "FltDt" });
            DropIndex("dbo.SubDepartments", new[] { "DepartmentId" });
            DropTable("dbo.Services");
            DropTable("dbo.FlightInfoes");
            DropTable("dbo.SubDepartments");
            DropTable("dbo.Departments");
        }
    }
}
