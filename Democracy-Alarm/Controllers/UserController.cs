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
        VotingServices _VotingServices = new VotingServices();
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
        public ActionResult GetVotingInfo(string UserID)
        {
            UserVotingInfo _UserVotingInfo = new UserVotingInfo();
            _UserVotingInfo.LastVotingSeason = _VotingServices.GetMemberLastVotingSeason(UserID);
            _UserVotingInfo.NextFullVotingSeason = _VotingServices.GetnNextVotingSeason(_UserVotingInfo.LastVotingSeason);
            int TempYear = 0;
            int TempSeason = 0;
            _VotingServices.GetVotingYearSeasonFromString(_VotingServices.GetCurrentVotingSeason(),
                out TempYear,
                out TempSeason);
            _UserVotingInfo.CurrentVotingYear = TempYear;
            _UserVotingInfo.CurrentVotingSeason = TempSeason;

            _VotingServices.GetVotingYearSeasonFromString(_UserVotingInfo.NextFullVotingSeason,
                 out TempYear,
                out TempSeason);
            _UserVotingInfo.NextVotingYear = TempYear;
            _UserVotingInfo.NextVotingSeason = TempSeason;
            return this.Json(_UserVotingInfo);
        }
        

    }
}