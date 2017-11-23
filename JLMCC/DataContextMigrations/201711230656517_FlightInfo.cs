namespace JLMCC.DataContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FlightInfo : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FlightInfoes",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        SoflSeqNr = c.Int(nullable: false),
                        AlnCd = c.String(),
                        FltDt = c.DateTime(nullable: false),
                        FltNr = c.String(),
                        LegSeqNr = c.String(),
                        OpSuffix = c.String(),
                        SvcType = c.String(),
                        SvcChnDesc = c.String(),
                        LatestTailNr = c.String(),
                        LatestEqpCd = c.String(),
                        ScheduledEqpCd = c.String(),
                        LegStsCd = c.String(),
                        AcfOper = c.String(),
                        DopsTmst = c.DateTime(nullable: false),
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
                        SchDepDt = c.DateTime(nullable: false),
                        LocSchDepDt = c.DateTime(nullable: false),
                        LatestDepDt = c.DateTime(nullable: false),
                        LocLatestDepDt = c.DateTime(nullable: false),
                        ActualOffblocks = c.DateTime(nullable: false),
                        TaxiOutTm = c.Int(nullable: false),
                        ActualAirborne = c.DateTime(nullable: false),
                        AirTm = c.DateTime(nullable: false),
                        LocAirTm = c.DateTime(nullable: false),
                        ArvArpCd = c.String(),
                        SchArvCityName = c.String(),
                        LatestArvArpCd = c.String(),
                        ArcArvCityName = c.String(),
                        SchArvDt = c.DateTime(nullable: false),
                        LocSchArvDt = c.DateTime(nullable: false),
                        latestArvDt = c.DateTime(nullable: false),
                        locLatestArvDt = c.DateTime(nullable: false),
                        ArvStandNo = c.String(),
                        ArvGateNo = c.String(),
                        ArvStsCd = c.String(),
                        DwnTm = c.DateTime(nullable: false),
                        LocDwnTm = c.DateTime(nullable: false),
                        ActualLanding = c.DateTime(nullable: false),
                        TaxiInTm = c.Int(nullable: false),
                        ActualOnblocks = c.DateTime(nullable: false),
                        LatestDlyCd = c.String(),
                        DelayTime = c.Int(nullable: false),
                        TailCompany = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.FlightInfoes");
        }
    }
}
