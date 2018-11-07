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
            List<FlightInfo> flightInfoes = GetFlightInfoes(daySelected, "CZ");
            ViewBag.Date = date;
            return View(flightInfoes);
        }

        public ActionResult SCIndex(string date)
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
            List<FlightInfo> flightInfoes = GetFlightInfoes(daySelected, "3U");
            ViewBag.Date = date;
            return View(flightInfoes);
        }

        public ActionResult MFIndex(string date)
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
            List<FlightInfo> flightInfoes = GetFlightInfoes(daySelected, "MF");
            ViewBag.Date = date;
            return View(flightInfoes);
        }
        //只通过日期筛选
        public List<FlightInfo> GetFlightInfoes(DateTime daySelected)
        {
            DateTime nextday = daySelected.AddDays(1);
            List<FlightInfo> flightInfoes = db.FlightInfoes.Where(m => m.FltDt.Value >= daySelected && m.FltDt.Value < nextday).OrderBy(m => m.FltDt).ToList();
            return flightInfoes;

        }
        //通过日期和航空公司筛选
        public List<FlightInfo> GetFlightInfoes(DateTime daySelected, string company)
        {
            DateTime nextday = daySelected.AddDays(1);
            List<FlightInfo> flightInfoes = db.FlightInfoes.Where(m => m.FltDt.Value >= daySelected && m.FltDt.Value < nextday && m.AlnCd == company).OrderBy(m => m.FltDt).ToList();
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
            List<FlightInfo> flightinfoes = GetFlightInfoes(daySelected).Where(m => m.ArcArvCityName == station || m.ArcDepCityName == station).ToList();
            //去掉航班状态代码为C(取消)和DEL(改直飞)的航班
            flightinfoes = flightinfoes.Where(m => m.LegStsCd != "C" && m.LegStsCd != "DEL").ToList();

            //选出不重复的飞机号
            var planes = flightinfoes.Where(m => m.LatestTailNr != "").OrderBy(m => m.LatestTailNr).Select(m => m.LatestTailNr).Distinct();

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

        public ActionResult SCMulti()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SCMulti(FormCollection collection)
        {
            if (ModelState.IsValid)
            {
                string content = Request["List"];
                string action = collection["action"];
                string station = Request["Station"];
                DateTime daySelected = Convert.ToDateTime(Request["date"]);

                ViewBag.ContentString = content;
                List<string> t = content.Split('\r', '\n').ToList();
                ViewBag.Content = t;
                ViewBag.Count = t.Count;
                int skipCount = 0;
                int faultCount = 0;
                int submitCount = 0;
                if (action == "提交")
                {
                    foreach (string item in t)
                    {
                        if (!string.IsNullOrEmpty(item))
                        {
                            var array = item.Split('\t');
                            if (array[1].Trim().Substring(0, 2) != "CZ"  && array[0].Trim()!="日期" && !array[20].Contains("取消"))  //不是南航的航班或者取消的航班或者是表格第一行，不进行导入
                            {
                                FlightInfo flightInfo = new FlightInfo();
                                flightInfo.AlnCd = array[1].Trim().Substring(0,2);
                                flightInfo.AcfOper = flightInfo.AlnCd;
                                flightInfo.FltDt = Convert.ToDateTime(array[0].Trim());
                                flightInfo.FltNr = array[1].Trim().Substring(2).Trim();
                                flightInfo.LatestEqpCd = array[2].Trim();
                                flightInfo.LatestTailNr = array[3].Trim();
                                flightInfo.ArcDepCityName = array[4].Trim();
                                flightInfo.SchDepCityName = array[4].Trim();
                                // flightInfo.SchDepDt = Convert.ToDateTime(array[0] + " " + array[5].Trim());
                                //川航数据格式改变
                                flightInfo.SchDepDt = Convert.ToDateTime(array[5].Trim());
                                flightInfo.ArcArvCityName = array[9].Trim();
                                flightInfo.SchArvCityName = array[9].Trim();
                                // flightInfo.SchArvDt = Convert.ToDateTime(array[0] + " " + array[10].Trim());
                                //川航数据格式改变
                                flightInfo.SchArvDt = Convert.ToDateTime(array[10].Trim());
                                if (flightInfo.SchArvDt<flightInfo.SchDepDt)
                                {
                                    flightInfo.SchArvDt = flightInfo.SchArvDt.Value.AddDays(1);
                                }
                            


                                //跳过重复的航班
                                var flightInfoinDb = from f in db.FlightInfoes
                                                 where f.AlnCd == flightInfo.AlnCd &&
                                                       f.AcfOper == flightInfo.AcfOper &&
                                                       f.FltDt == flightInfo.FltDt &&
                                                       f.FltNr == flightInfo.FltNr &&
                                                       f.LatestEqpCd == flightInfo.LatestEqpCd &&
                                                       f.LatestTailNr == flightInfo.LatestTailNr &&
                                                       f.ArcDepCityName == flightInfo.ArcDepCityName &&
                                                       f.SchDepCityName == flightInfo.SchDepCityName &&
                                                       f.SchDepDt == flightInfo.SchDepDt &&
                                                       f.ArcArvCityName == flightInfo.ArcArvCityName &&
                                                       f.SchArvCityName == flightInfo.SchArvCityName &&
                                                       f.SchArvDt == flightInfo.SchArvDt
                                                     select f;

                                if (flightInfoinDb.Count() == 0)
                                {
                                    db.FlightInfoes.Add(flightInfo);
                                }
                                else
                                {
                                    skipCount += 1;
                                }


                            }
                            else
                            {
                                skipCount += 1;

                            }
                        }
                       
                    }
                    submitCount = db.SaveChanges();
                    
                }
                ViewBag.SubmitCount = submitCount;
                ViewBag.SkipCount = skipCount;
                ViewBag.FaultCount = faultCount;
                return View();
            }
            return View();
        }

        public ActionResult MFMulti()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MFMulti(FormCollection collection)
        {
            if (ModelState.IsValid)
            {
                string content = Request["List"];
                string action = collection["action"];
                string station = Request["Station"];
                DateTime daySelected = Convert.ToDateTime(Request["date"]);

                ViewBag.ContentString = content;
                List<string> t = content.Split('\r', '\n').ToList();
                ViewBag.Content = t;
                ViewBag.Count = t.Count;
                int skipCount = 0;
                int faultCount = 0;
                int submitCount = 0;
                if (action == "提交")
                {
                    foreach (string item in t)
                    {
                        if (!string.IsNullOrEmpty(item))
                        {
                            var array = item.Split('\t');
                            if (array[1].Trim().Substring(0, 2) != "CZ"&& !array[0].Contains("性质") && !array[0].Contains("延误") && !array[22].Contains("取消"))  //不是南航的航班或者取消的航班或者是表格第一行，不进行导入
                            {

                                FlightInfo flightInfo = new FlightInfo();
                                flightInfo.AlnCd = array[1].Trim().Substring(0,2);
                                flightInfo.AcfOper = flightInfo.AlnCd;
                                flightInfo.SvcChnDesc = array[0];
                                flightInfo.FltDt = Convert.ToDateTime(array[29].Trim());
                                flightInfo.FltNr = array[1].Trim().Substring(2);
                                flightInfo.LatestEqpCd = array[2].Trim();
                                flightInfo.LatestTailNr = array[3].Trim();
                                flightInfo.ArcDepCityName = array[6].Trim();
                                flightInfo.SchDepCityName = array[6].Trim();
                                flightInfo.SchDepDt = Convert.ToDateTime(array[29] + " " + array[7].Trim());

                                flightInfo.ArcArvCityName = array[16].Trim();
                                flightInfo.SchArvCityName = array[16].Trim();
                                flightInfo.SchArvDt = Convert.ToDateTime(array[29] + " " + array[15].Replace('+', ' ').Trim());
                                if (flightInfo.SchArvDt < flightInfo.SchDepDt)
                                {
                                    flightInfo.SchArvDt = flightInfo.SchArvDt.Value.AddDays(1);
                                }




                                var flightInfoinDb = from f in db.FlightInfoes
                                                     where f.AlnCd == flightInfo.AlnCd &&
                                                           f.AcfOper == flightInfo.AcfOper &&
                                                           f.FltDt == flightInfo.FltDt &&
                                                           f.FltNr == flightInfo.FltNr &&
                                                           f.LatestEqpCd == flightInfo.LatestEqpCd &&
                                                           f.LatestTailNr == flightInfo.LatestTailNr &&
                                                           f.ArcDepCityName == flightInfo.ArcDepCityName &&
                                                           f.SchDepCityName == flightInfo.SchDepCityName &&
                                                           f.SchDepDt == flightInfo.SchDepDt &&
                                                           f.ArcArvCityName == flightInfo.ArcArvCityName &&
                                                           f.SchArvCityName == flightInfo.SchArvCityName &&
                                                           f.SchArvDt == flightInfo.SchArvDt
                                                     select f;

                                if (flightInfoinDb.Count() == 0)
                                {
                                    db.FlightInfoes.Add(flightInfo);

                                }
                                else
                                {
                                    skipCount += 1;
                                }


                            }
                            else
                            {
                                skipCount += 1;

                            }
                        }
                       
                }
                    submitCount = db.SaveChanges();


                }
                ViewBag.SubmitCount = submitCount;
                ViewBag.SkipCount = skipCount;
                ViewBag.FaultCount = faultCount;
                return View();
            }
            return View();
        }
        //private DateTime SocTimeStringToDatetime(string soctime, DateTime daySelected)
        //{
        //    //将soc上时间字符串转换为datatime
        //    int day = Convert.ToInt32(soctime.Substring(0, 2));
        //    int hour = Convert.ToInt32(soctime.Substring(3, 2));
        //    int min = Convert.ToInt32(soctime.Substring(5, 2));
        //    if (day == daySelected.Day)
        //    {
        //        DateTime t = new DateTime(daySelected.Year, daySelected.Month, day, hour, min, 0);
        //        return t;
        //    }
        //    else if (day == daySelected.AddDays(1).Day)
        //    {
        //        DateTime t = new DateTime(daySelected.AddDays(1).Year, daySelected.AddDays(1).Month, day, hour, min, 0);
        //        return t;
        //    }
        //    else
        //    {
        //        return DateTime.MinValue;
        //    }


        //}
    }
}
