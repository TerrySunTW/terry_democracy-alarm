using Democracy_Alarm.Models;
using Democracy_Alarm.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Democracy_Alarm.Controllers
{
    public class VotingController : Controller
    {
        VotingServices _VotingServices = new VotingServices();
        // GET: Voting
        public ActionResult SubmitNewVoting(string UserID, string Target, string Season, string Comment, bool IsDiscard)
        {
            string Message;

            var result = _VotingServices.CreateNewVotingRecord(UserID, Target, Season, Comment, IsDiscard, out Message);
            if (!result)
            {
                return Content(Message);
            }
            return Content("true");
        }
        public ActionResult GetCityVoting(string City)
        {
            CityVotingViewModel _CityVotingViewModel = new CityVotingViewModel();
            _CityVotingViewModel.CityName = City;
            _CityVotingViewModel.VoteResult = _VotingServices.GetCityVotingRecords(City);
            return PartialView(_CityVotingViewModel);
        }
        public ActionResult GetMyVoting(string UserID)
        {
            PersonalVotingViewModel _PersonalVotingViewModel = new PersonalVotingViewModel();
            _PersonalVotingViewModel.VoteResult = _VotingServices.GetMemberVotingRecord(UserID);
            return PartialView(_PersonalVotingViewModel);
        }
    }
}