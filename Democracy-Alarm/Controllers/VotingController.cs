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
        // GET: Voting
        public ActionResult SubmitNewVoting(string UserID, string Target, string Season, string Comment,bool IsDiscard)
        {
            string Message;
            VotingServices _VotingServices = new VotingServices();
            var result=_VotingServices.CreateNewVotingRecord(UserID, Target, Season, Comment, IsDiscard, out Message);
            if(!result)
            {
                return Content(Message);
            }
            return Content("true");
        }
    }
}