using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JLMCC.Models
{
    public class FlightInfo
    {
        public string Id { get; set; }

        [Display(Name = "航班唯一ID")]
        public int SoflSeqNr { get; set; }


        [Display(Name = "航空公司代码")]
        public string AlnCd { get; set; }

        [Display(Name = "航班日期")]
        public DateTime FltDt { get; set; }

        [Display(Name = "航班号")]
        public string FltNr { get; set; }

        [Display(Name = "航段序号")]
        public string LegSeqNr { get; set; }

        [Display(Name = "航班号后缀")]
        public string OpSuffix { get; set; }

        [Display(Name = "航班性质")]
        //J：正班；G：加班；H：货机 等
        public string SvcType { get; set; }

        [Display(Name = "航班性质中文")]
        public string SvcChnDesc { get; set; }

        [Display(Name = "机尾号")]
        public string LatestTailNr { get; set; }

        [Display(Name = "机型")]
        public string LatestEqpCd { get; set; }

        [Display(Name = "实际机型")]
        public string ScheduledEqpCd { get; set; }

        [Display(Name = "航班状态代码")]
        public string LegStsCd { get; set; }

        [Display(Name = "执行航班的航空公司代码")]
        //（C、DEL、R）
        public string AcfOper { get; set; }

        [Display(Name = "最新更改时间")]
        public DateTime DopsTmst { get; set; }

        [Display(Name = "删除标记")]
        //Y/N
        public string DeleteInd { get; set; }

        [Display(Name = "航班取消代码")]
        //表示具体的取消原因
        public string CnclCd { get; set; } 

        [Display(Name = "分公司代码")]
        public string BranchCode { get; set; }

        [Display(Name = "计划起飞站代码")]
        public string DepArpCd { get; set; }

        [Display(Name = "计划起飞机场中文")]
        public string SchDepCityName { get; set; }

        [Display(Name = "起飞状态代码")]
        //SCH计划、ETD预计、OFF滑出、AIR离地
        public string DepStsCd { get; set; }

        [Display(Name = "实际起飞站代码")]
        public string LatestDepArpCd { get; set; }

        [Display(Name = "实际起飞机场中文")]
        public string ArcDepCityName { get; set; }

        [Display(Name = "起飞机场停机位")]
        public string DepStandNo { get; set; }

        [Display(Name = "起飞机场登机口")]
        public string DepGateNo { get; set; }


        [Display(Name = "计划起飞时间")]
        public DateTime SchDepDt { get; set; }

        [Display(Name = "计划起飞时间(local)")]
        public DateTime LocSchDepDt { get; set; }

        [Display(Name = "预计起飞时间")]
        public DateTime LatestDepDt { get; set; }

        [Display(Name = "预计起飞时间(local)")]
        public DateTime LocLatestDepDt { get; set; }

        [Display(Name = "撤轮挡时间")]
        public DateTime ActualOffblocks { get; set; }

        [Display(Name = "滑出用时(分)")]
        public int TaxiOutTm { get; set; }

        [Display(Name = "实际离地时间")]
        public DateTime ActualAirborne { get; set; }


        [Display(Name = "离地时间")]
        //Airborne time
        public DateTime AirTm { get; set; }

        [Display(Name = "离地时间(local)")]
        public DateTime LocAirTm { get; set; }


        [Display(Name = "计划到达站代码")]
        public string ArvArpCd { get; set; }

        [Display(Name = "计划到达机场中文")]
        public string SchArvCityName { get; set; }

        [Display(Name = "实际到达站代码")]
        public string LatestArvArpCd { get; set; }

        [Display(Name = "实际到达机场中文")]
        public string ArcArvCityName { get; set; }

        [Display(Name = "计划到达时间")]
        public DateTime SchArvDt { get; set; }

        [Display(Name = "计划到达时间(local)")]
        public DateTime LocSchArvDt { get; set; }

        [Display(Name = "预计到达时间")]
        public DateTime latestArvDt { get; set; }

        [Display(Name = "预计到达时间(local)")]
        public DateTime locLatestArvDt { get; set; }

        [Display(Name = "落地机场停机位")]
        public string ArvStandNo { get; set; }

        [Display(Name = "落地机场登机口")]
        public string ArvGateNo { get; set; }

        [Display(Name = "到达状态代码")]
        //SCH计划、ETD预计、DWN落地、ON滑入
        public string ArvStsCd { get; set; }

        [Display(Name = "落地时间")]
        //Touch down time
        public DateTime DwnTm { get; set; }

        [Display(Name = "落地时间(local)")]
        public DateTime LocDwnTm { get; set; }


        [Display(Name = "实际着陆时间")]
        public DateTime ActualLanding { get; set; }


        [Display(Name = "滑入用时(分)")]
        public int TaxiInTm { get; set; }
        [Display(Name = "上轮挡时间")]
        public DateTime ActualOnblocks { get; set; }

        [Display(Name = "延误代码")]
        public string LatestDlyCd { get; set; }

        [Display(Name = "延误时间")]
        //延误时间，实际起飞时间与计划起飞时间的差值(分钟)
        public int DelayTime { get; set; }        

        [Display(Name = "飞机所属公司")]
        public string TailCompany { get; set; }

    }
}