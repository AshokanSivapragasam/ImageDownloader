using Irctc.Web.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Irctc.Web.Controllers
{
    //[Authorize]
    [OutputCacheAttribute(VaryByParam = "*", Duration = 0, NoStore = true)] 
    public class TrainController : Controller
    {
        private IrctcDbContext db = new IrctcDbContext();

        public ActionResult SearchTrains()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SearchTrains(SearchTrainViewModel searchTrainViewModel)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("GetTrains", new { FromStation = searchTrainViewModel.FromStation, ToStation = searchTrainViewModel.ToStation });
            }

            return View(searchTrainViewModel);
        }

        public ActionResult GetTrains(string FromStation, string ToStation)
        {
            var trainModel = db.Trains.Where(r => r.FromStation.Equals(FromStation) && r.ToStation.Equals(ToStation)).AsEnumerable();
            if (trainModel == null)
            {
                return HttpNotFound();
            }
            return View(trainModel);
        }

        public ActionResult GetTrainsJson(string FromStation, string ToStation)
        {
            var trainModel = db.Trains.Where(r => r.FromStation.Equals(FromStation) && r.ToStation.Equals(ToStation)).AsEnumerable();
            if (trainModel == null)
            {
                return HttpNotFound();
            }
            return Json(trainModel, JsonRequestBehavior.AllowGet);
        }

        public JsonResult BookTicket(string TrainId, string TrainSeatType, string DateOfJourney, string NameOfPassenger, string PanNumber, bool IsPaymentSuccessful)
        {
            var trainSeat = db.TrainSeats.FirstOrDefault(ts => ts.TrainId.Equals(TrainId) && ts.SeatType.Equals(TrainSeatType) && ts.IsReserved.Equals(false));

            if (trainSeat != null)
            {
                var passengerInfo = new PassengerInformation
                {
                    SeatNumber = trainSeat.SeatNumber,
                    PanNumber = PanNumber,
                    NameOfPassenger = NameOfPassenger,
                    IsPaymentSuccessful = IsPaymentSuccessful,
                    IsPaymentReturned = false,
                    IsTicketCanceled = false
                };

                db.PassengerInfo.Add(passengerInfo);
                db.SaveChanges();

                var trainSeatReserve = new TrainSeat
                {
                    Id = trainSeat.Id,
                    IsReserved = true,
                    SeatNumber = trainSeat.SeatNumber,
                    SeatType = trainSeat.SeatType,
                    TrainId = trainSeat.TrainId,
                    TrainScheduledDateTime = trainSeat.TrainScheduledDateTime
                };

                //db.Entry(trainSeatReserve).State = EntityState.Modified;
                db.Entry(trainSeat).CurrentValues.SetValues(trainSeatReserve);
                db.SaveChanges();
            }

            return Json(new
            {
                SeatNumber = trainSeat != null ? trainSeat.SeatNumber : "No seats available",
                PanNumber = PanNumber,
                NameOfPassenger = NameOfPassenger,
                IsPaymentSuccessful = IsPaymentSuccessful,
                IsPaymentReturned = false,
                IsTicketCanceled = false,
                Confirmed = trainSeat != null ? "Yes" : "No"
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
