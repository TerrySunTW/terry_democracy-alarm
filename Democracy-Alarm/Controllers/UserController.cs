using Democracy_Alarm.Models;
using Democracy_Alarm.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Democracy_Alarm.Controllers
{
    public class UserController : Controller
    {
        UserServices _UserServices = new UserServices();
        public ActionResult MyLocation(string UserID)
        {
            VotingServices _VotingServices = new VotingServices();
            MyLocationViewModel _MyLocationViewModel = new MyLocationViewModel();
            _MyLocationViewModel.Cities = _VotingServices.GetCitys();
            _MyLocationViewModel.MyCity = _UserServices.GetUserLocation(UserID);
            return PartialView(_MyLocationViewModel);
        }
        public ActionResult SetMyLocation(string UserID,string Location)
        {
            UserServices _UserServices = new UserServices();
            _UserServices.UpdateUserLocation(UserID, Location);
            return Content("true");
        }
        public ActionResult GetMyLocation(string UserID)
        {
            return Content(_UserServices.GetUserLocation(UserID));
        }

    }
}