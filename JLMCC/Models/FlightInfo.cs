using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Serialization;
using Newtonsoft.Json;
using System.Reflection;
using System.ComponentModel.DataAnnotations.Schema;

namespace JLMCC.Models
{
    
    public class FlightInfo
    {
        public int Id { get; set; }

        /// <summary>
        /// 航班唯一ID
        /// </summary>
        [Display(Name = "航班唯一ID")]
        [JsonProperty("soflSeqNr")]        
        public int SoflSeqNr { get; set; }

        /// <summary>
        /// 航空公司代码
        /// </summary>
        [Display(Name = "航空公司代码")]
        [JsonProperty("alnCd")]
        public string AlnCd { get; set; }
       
        /// <summary>
        /// 航班日期
        /// </summary>
        [Display(Name = "航班日期")]
        [Index]
        [XmlElement(DataType = "dateTime")]
        public DateTime? FltDt { get; set; }
        
        /// <summary>
        /// 航班号
        /// </summary>
        [Display(Name = "航班号")]
        public string FltNr { get; set; }
        
        /// <summary>
        /// 航段序号
        /// </summary>
        [Display(Name = "航段序号")]
        public string LegSeqNr { get; set; }
        
        /// <summary>
        /// 航班号后缀
        /// </summary>
        [Display(Name = "航班号后缀")]
        public string OpSuffix { get; set; }

        /// <summary>
        /// 航班性质
        ///<para> J：正班；G：加班；H：货机 等</para>
        /// </summary>
        [Display(Name = "航班性质")]
        public string SvcType { get; set; }

        /// <summary>
        /// 航班性质中文
        /// </summary>
        [Display(Name = "航班性质中文")]
        public string SvcChnDesc { get; set; }

        /// <summary>
        /// 机尾号
        /// </summary>
        [Display(Name = "机尾号")]
        public string LatestTailNr { get; set; }

        /// <summary>
        /// 机型
        /// </summary>
        [Display(Name = "机型")]
        public string LatestEqpCd { get; set; }

        /// <summary>
        /// 实际机型
        /// </summary>
        [Display(Name = "实际机型")]
        public string ScheduledEqpCd { get; set; }

        /// <summary>
        /// 航班状态代码（C:取消或者删除、DEL:改直飞、R:取消后恢复）
        /// </summary>
        [Display(Name = "航班状态代码")]
        public string LegStsCd { get; set; }

        /// <summary>
        /// 航班状态代码中文释义（C:取消或者删除、DEL:改直飞、R:取消后恢复）
        /// </summary>
        [Display(Name = "航班状态代码中文释义")]
        public string LegStsChn { get; set; }

        /// <summary>
        /// 执行航班的航空公司代码
        /// </summary>
        [Display(Name = "执行航班的航空公司代码")]
        //（C、DEL、R）
        public string AcfOper { get; set; }

        /// <summary>
        /// 最新更改时间
        /// </summary>
        [Display(Name = "最新更改时间")]
         [XmlElement(DataType = "dateTime")]
        public DateTime? DopsTmst { get; set; }

        /// <summary>
        /// 删除标记
        /// </summary>
        [Display(Name = "删除标记")]
        //Y/N
        public string DeleteInd { get; set; }

        /// <summary>
        /// 航班取消代码,表示具体的取消原因
        /// </summary>
        [Display(Name = "航班取消代码")]    
        public string CnclCd { get; set; }

        /// <summary>
        /// 分公司代码
        /// </summary>
        [Display(Name = "分公司代码")]
        public string BranchCode { get; set; }

        /// <summary>
        /// 计划起飞站代码
        /// </summary>
        [Display(Name = "计划起飞站代码")]
        public string DepArpCd { get; set; }

        /// <summary>
        /// 计划起飞机场中文
        /// </summary>
        [Display(Name = "计划起飞机场中文")]
        public string SchDepCityName { get; set; }

        /// <summary>
        /// 起飞状态代码
        /// </summary>
        [Display(Name = "起飞状态代码")]
        //SCH计划、ETD预计、OFF滑出、AIR离地
        public string DepStsCd { get; set; }

        /// <summary>
        /// 实际起飞站代码
        /// </summary>
        [Display(Name = "实际起飞站代码")]
        public string LatestDepArpCd { get; set; }

        /// <summary>
        /// 实际起飞机场中文
        /// </summary>
        [Display(Name = "实际起飞机场中文")]
        public string ArcDepCityName { get; set; }

        /// <summary>
        /// 起飞机场停机位
        /// </summary>
        [Display(Name = "起飞机场停机位")]
        public string DepStandNo { get; set; }

        /// <summary>
        /// 起飞机场登机口
        /// </summary>
        [Display(Name = "起飞机场登机口")]
        public string DepGateNo { get; set; }

        /// <summary>
        /// 计划起飞时间
        /// </summary>
        [Display(Name = "计划起飞")]
        [XmlElement(DataType = "dateTime")]
        public DateTime? SchDepDt { get; set; }

        /// <summary>
        /// 计划起飞时间(local)
        /// </summary>
        [Display(Name = "计划起飞(local)")]
        [XmlElement(DataType = "dateTime")]
        public DateTime? LocSchDepDt { get; set; }

        /// <summary>
        /// 预计起飞时间
        /// </summary>
        [Display(Name = "预计起飞")]
        [XmlElement(DataType = "dateTime")]
        public DateTime? LatestDepDt { get; set; }

        /// <summary>
        /// 预计起飞时间(local)
        /// </summary>
        [Display(Name = "预计起飞(local)")]
        [XmlElement(DataType = "dateTime")]
        public DateTime? LocLatestDepDt { get; set; }

        /// <summary>
        /// 撤轮挡时间
        /// </summary>
        [Display(Name = "撤轮挡时间")]
        [XmlElement(DataType = "dateTime")]
        public DateTime? ActualOffblocks { get; set; }

        /// <summary>
        /// 滑出用时(分)
        /// </summary>
        [Display(Name = "滑出用时(分)")]
        public int? TaxiOutTm { get; set; }

        /// <summary>
        /// 实际离地时间
        /// </summary>
        [Display(Name = "实际离地时间")]
        [XmlElement(DataType = "dateTime")]
        public DateTime? ActualAirborne { get; set; }

        /// <summary>
        /// 离地时间
        /// </summary>
        [Display(Name = "离地时间")]
        [XmlElement(DataType = "dateTime")]
        //Airborne time
        public DateTime? AirTm { get; set; }

        /// <summary>
        /// 离地时间(local)
        /// </summary>
        [Display(Name = "离地时间(local)")]
        [XmlElement(DataType = "dateTime")]
        public DateTime? LocAirTm { get; set; }

        /// <summary>
        /// 计划到达站代码
        /// </summary>
        [Display(Name = "计划到达站代码")]
        public string ArvArpCd { get; set; }

        /// <summary>
        /// 计划到达机场中文
        /// </summary>
        [Display(Name = "计划到达机场中文")]
        public string SchArvCityName { get; set; }

        /// <summary>
        /// 实际到达站代码
        /// </summary>
        [Display(Name = "实际到达站代码")]
        public string LatestArvArpCd { get; set; }

        /// <summary>
        /// 实际到达机场中文
        /// </summary>
        [Display(Name = "实际到达机场中文")]
        public string ArcArvCityName { get; set; }

        /// <summary>
        /// 计划到达时间
        /// </summary>
        [Display(Name = "计划到达")]
        [XmlElement(DataType = "dateTime")]
        public DateTime? SchArvDt { get; set; }

        /// <summary>
        /// 计划到达时间(local)
        /// </summary>
        [Display(Name = "计划到达(local)")]
        [XmlElement(DataType = "dateTime")]
        public DateTime? LocSchArvDt { get; set; }

        /// <summary>
        /// 预计到达时间
        /// </summary>
        [Display(Name = "预计到达")]
        [XmlElement(DataType = "dateTime")]
        public DateTime? latestArvDt { get; set; }

        /// <summary>
        /// 预计到达时间(local)
        /// </summary>
        [Display(Name = "预计到达(local)")]
        [XmlElement(DataType = "dateTime")]
        public DateTime? locLatestArvDt { get; set; }

        /// <summary>
        /// 落地机场停机位
        /// </summary>
        [Display(Name = "落地机场停机位")]
        public string ArvStandNo { get; set; }

        /// <summary>
        /// 落地机场登机口
        /// </summary>
        [Display(Name = "落地机场登机口")]
        public string ArvGateNo { get; set; }

        /// <summary>
        /// 到达状态代码:SCH计划、ETA预计、DWN落地、ON滑入
        /// </summary>
        [Display(Name = "到达状态代码")]       
        public string ArvStsCd { get; set; }

        /// <summary>
        /// 落地时间
        /// </summary>
        [Display(Name = "落地时间")]
        [XmlElement(DataType = "dateTime")]
        //Touch down time
        public DateTime? DwnTm { get; set; }

        /// <summary>
        /// 落地时间(local)
        /// </summary>
        [Display(Name = "落地时间(local)")]
        [XmlElement(DataType = "dateTime")]
        public DateTime? LocDwnTm { get; set; }

        /// <summary>
        /// 实际着陆时间
        /// </summary>
        [Display(Name = "实际着陆时间")]
        [XmlElement(DataType = "dateTime")]
        public DateTime? ActualLanding { get; set; }

        /// <summary>
        /// 滑入用时(分)
        /// </summary>
        [Display(Name = "滑入用时(分)")]
        public int? TaxiInTm { get; set; }

        /// <summary>
        /// 上轮挡时间
        /// </summary>
        [Display(Name = "上轮挡时间")]
           [XmlElement(DataType = "dateTime")]
        public DateTime? ActualOnblocks { get; set; }

        /// <summary>
        /// 延误代码
        /// </summary>
        [Display(Name = "延误代码")]
        public string LatestDlyCd { get; set; }

        /// <summary>
        /// 延误时间
        /// </summary>
        [Display(Name = "延误时间")]
        //延误时间，实际起飞时间与计划起飞时间的差值(分钟)
        public int? DelayTime { get; set; }

        /// <summary>
        /// 延误代码1
        /// </summary>
        [Display(Name = "延误代码1")]
        public string DlyCd1 { get; set; }

        /// <summary>
        /// 延误时间1
        /// </summary>
        [Display(Name = "延误时间1")]       
        public int? DlyTm1 { get; set; }

        /// <summary>
        /// 延误代码1的对内延误原因
        /// </summary>
        [Display(Name = "延误1对内原因")]
        public string DlyReasonDetail01 { get; set; }

        /// <summary>
        /// 延误代码1的对外延误原因
        /// </summary>
        [Display(Name = "延误1对外原因")]
        public string DlyReasonPublish01 { get; set; }

        /// <summary>
        /// 延误代码2
        /// </summary>
        [Display(Name = "延误代码2")]
        public string DlyCd2 { get; set; }

        /// <summary>
        /// 延误时间1
        /// </summary>
        [Display(Name = "延误时间1")]
        public int? DlyTm2 { get; set; }

        /// <summary>
        /// 延误代码2的对内延误原因
        /// </summary>
        [Display(Name = "延误2对内原因")]
        public string DlyReasonDetail02 { get; set; }

        /// <summary>
        /// 延误代码2的对外延误原因
        /// </summary>
        [Display(Name = "延误2对外原因")]
        public string DlyReasonPublish02 { get; set; }


        /// <summary>
        /// 飞机所属公司
        /// </summary>
        [Display(Name = "飞机所属公司")]
        public string TailCompany { get; set; }

        // override object.Equals
        public override bool Equals(object obj)
        {
            //       
            // See the full list of guidelines at
            //   http://go.microsoft.com/fwlink/?LinkID=85237  
            // and also the guidance for operator== at
            //   http://go.microsoft.com/fwlink/?LinkId=85238
            //

            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            FlightInfo b = (FlightInfo)obj;

            foreach (PropertyInfo p in this.GetType().GetProperties())
            {
                object m = p.GetValue(this);
                object n = p.GetValue(b);
                if (m != null && n != null)
                {
                    if (!m.Equals(n))
                    {
                        return false;
                    }
                }
                else if (m == null ^ n == null)
                {
                    return false;
                }
            }
            return true;

        }




    }

    public class Section
    {
        public int key { get; set; }
        public string label { get; set; }

    }

    public class FlightTimeLine
    {
        public string text { get; set; }
        public string start_date { get; set; }
        public string end_date { get; set; }

        public int section_id { get; set; }
        public string PlaneNO { get; set; }
        public int FlightId { get; set; }
    }


    public class FlightsJson
    {
        public List<Section> Sections { get; set; }
        public List<FlightTimeLine> Flights { get; set; }

    }
}