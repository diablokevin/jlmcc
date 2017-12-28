using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using JLMCC.Models;
using Newtonsoft.Json;
using System.IO;
using System.Xml.Serialization;
using System.Xml;

namespace JLMCC.Controllers
{
    public class FlightInfoesController : Controller
    {
        private JlmccContext db = new JlmccContext();

        // GET: FlightInfoes
        public ActionResult Index(string date)
        {
            DateTime daySelected = new DateTime();

            if (date == null || date == string.Empty)
            {
                daySelected = DateTime.Today;

            }
            else
            {
                daySelected = Convert.ToDateTime(date);
            }
            List<FlightInfo> flightInfoes = GetFlightInfoesByDate(daySelected);
                ViewBag.Date = date;
            return View(flightInfoes);
        }

        public List<FlightInfo> GetFlightInfoesByDate(DateTime daySelected)
        {
            DateTime nextday = daySelected.AddDays(1);
            List<FlightInfo> flightInfoes= db.FlightInfoes.Where(m => m.FltDt.Value >= daySelected && m.FltDt.Value<nextday).OrderBy(m=>m.FltDt).ToList();
            return flightInfoes;

        }
        // GET: FlightInfoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FlightInfo flightInfo = db.FlightInfoes.Find(id);
            if (flightInfo == null)
            {
                return HttpNotFound();
            }
            return View(flightInfo);
        }

        // GET: FlightInfoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FlightInfoes/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,SoflSeqNr,AlnCd,FltDt,FltNr,LegSeqNr,OpSuffix,SvcType,SvcChnDesc,LatestTailNr,LatestEqpCd,ScheduledEqpCd,LegStsCd,LegStsChn,AcfOper,DopsTmst,DeleteInd,CnclCd,BranchCode,DepArpCd,SchDepCityName,DepStsCd,LatestDepArpCd,ArcDepCityName,DepStandNo,DepGateNo,SchDepDt,LocSchDepDt,LatestDepDt,LocLatestDepDt,ActualOffblocks,TaxiOutTm,ActualAirborne,AirTm,LocAirTm,ArvArpCd,SchArvCityName,LatestArvArpCd,ArcArvCityName,SchArvDt,LocSchArvDt,latestArvDt,locLatestArvDt,ArvStandNo,ArvGateNo,ArvStsCd,DwnTm,LocDwnTm,ActualLanding,TaxiInTm,ActualOnblocks,LatestDlyCd,DelayTime,TailCompany,DlyCd1,DlyTm1,DlyReasonDetail01,DlyReasonPublish01,DlyCd2,DlyTm2,DlyReasonDetail02,DlyReasonPublish02")] FlightInfo flightInfo)
        {
            if (ModelState.IsValid)
            {
                db.FlightInfoes.Add(flightInfo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(flightInfo);
        }

        // GET: FlightInfoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FlightInfo flightInfo = db.FlightInfoes.Find(id);
            if (flightInfo == null)
            {
                return HttpNotFound();
            }
            return View(flightInfo);
        }

        // POST: FlightInfoes/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,SoflSeqNr,AlnCd,FltDt,FltNr,LegSeqNr,OpSuffix,SvcType,SvcChnDesc,LatestTailNr,LatestEqpCd,ScheduledEqpCd,LegStsCd,LegStsChn,AcfOper,DopsTmst,DeleteInd,CnclCd,BranchCode,DepArpCd,SchDepCityName,DepStsCd,LatestDepArpCd,ArcDepCityName,DepStandNo,DepGateNo,SchDepDt,LocSchDepDt,LatestDepDt,LocLatestDepDt,ActualOffblocks,TaxiOutTm,ActualAirborne,AirTm,LocAirTm,ArvArpCd,SchArvCityName,LatestArvArpCd,ArcArvCityName,SchArvDt,LocSchArvDt,latestArvDt,locLatestArvDt,ArvStandNo,ArvGateNo,ArvStsCd,DwnTm,LocDwnTm,ActualLanding,TaxiInTm,ActualOnblocks,LatestDlyCd,DelayTime,TailCompany,DlyCd1,DlyTm1,DlyReasonDetail01,DlyReasonPublish01,DlyCd2,DlyTm2,DlyReasonDetail02,DlyReasonPublish02")] FlightInfo flightInfo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(flightInfo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(flightInfo);
        }

        // GET: FlightInfoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FlightInfo flightInfo = db.FlightInfoes.Find(id);
            if (flightInfo == null)
            {
                return HttpNotFound();
            }
            return View(flightInfo);
        }

        // POST: FlightInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FlightInfo flightInfo = db.FlightInfoes.Find(id);
            db.FlightInfoes.Remove(flightInfo);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [HttpPost]
        public string PostFlightInfo()
        {
            //接受post过来的数据，将数据从xml转为json，从json数据中只取出数组部分
            //然后逐条与数据库中的数据比对
            //不存在同样的数据或者发生改变后才进行创建或修改
            //最后返回受影响的数据库项目
            try
            {
                var sr = new StreamReader(Request.InputStream);
                var stream = sr.ReadToEnd();
                string str = stream.ToString();
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(str);
                string jsonstring = JsonConvert.SerializeXmlNode(doc.FirstChild, Newtonsoft.Json.Formatting.None, true);
                int start = jsonstring.IndexOf('[');
                int end = jsonstring.LastIndexOf(']');
                jsonstring = jsonstring.Substring(start, end - start + 1);
                List<FlightInfo> FlightInfos = new List<Models.FlightInfo>();
                FlightInfos = JsonConvert.DeserializeObject<List<FlightInfo>>(jsonstring);

                foreach (FlightInfo info in FlightInfos)
                {
                    FlightInfo infoExisted = db.FlightInfoes.AsNoTracking().Where(m => m.SoflSeqNr == info.SoflSeqNr).FirstOrDefault();
                    if (infoExisted == null)
                    {
                        db.FlightInfoes.Add(info);
                    }
                    else
                    {

                        info.Id = infoExisted.Id;
                        if (!info.Equals(infoExisted))
                        {
                            //infoExisted = info;
                            db.FlightInfoes.Attach(info);
                            db.Entry(info).State = EntityState.Modified;
                        }
                    }
                }

                int result = db.SaveChanges();
                return "已创建或修改项目" + result;
            }
            catch (Exception e)
            {
                return e.Message;
            }

        }
        public JsonResult GetFlightinfoesToJson(string date, string station)
        {
            DateTime daySelected = new DateTime();
            if (date == null || date == "")
            {
                daySelected = DateTime.Now;

            }
            else
            {
                daySelected = Convert.ToDateTime(date);
            }

            if (station == null || station == "")
            {
                station = "长春";
            }
            //筛选选定机场的航班
            List<FlightInfo> flightinfoes = GetFlightInfoesByDate(daySelected).Where(m => m.ArcArvCityName == station || m.ArcDepCityName == station).ToList();
            //去掉航班状态代码为C(取消)和DEL(改直飞)的航班
            flightinfoes = flightinfoes.Where(m => m.LegStsCd != "C" && m.LegStsCd != "DEL").ToList();

            //选出不重复的飞机号
            var planes = flightinfoes.Where(m=>m.LatestTailNr!="").OrderBy(m => m.LatestTailNr).Select(m => m.LatestTailNr).Distinct();

            List<Section> sections = new List<Section>();
            int i = 1;
            foreach (var p in planes)
            {
                Section s = new Section()
                {
                    key = i,
                    label = p.ToString()
                };
                sections.Add(s);
                i = i + 1;
            }

            List<FlightTimeLine> timeLines = new List<FlightTimeLine>();

            foreach (FlightInfo flightinfo in flightinfoes)
            {
                FlightTimeLine timeline = new FlightTimeLine()
                {
                    section_id = sections.Where(m => m.label == flightinfo.LatestTailNr).First().key,
                    text = string.Format("{0}{1}{2}", flightinfo.ArcDepCityName, flightinfo.AlnCd, flightinfo.ArcArvCityName),
                    start_date = flightinfo.SchDepDt.Value.ToString(),
                    end_date = flightinfo.SchArvDt.Value.ToString(),
                    PlaneNO = flightinfo.LatestTailNr,
                    FlightId = flightinfo.Id

                };
                timeLines.Add(timeline);

            }
            FlightsJson result = new FlightsJson
            {
                Sections = sections,
                Flights = timeLines

            };


            return Json(result, JsonRequestBehavior.AllowGet);

        }
        public ActionResult Gantt()
        {

            return View();
        }
    }
}
