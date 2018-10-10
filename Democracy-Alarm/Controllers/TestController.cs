using Democracy_Alarm.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Democracy_Alarm.Controllers
{
    public class TestController : Controller
    {
        VotingServices _VotingServices = new VotingServices();
        // GET: Test
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Reset(String UserID)
        {
            _VotingServices.RemoveMemberVotingRecord(UserID);
            return RedirectToAction("Index", "Home");
        }
    }
}