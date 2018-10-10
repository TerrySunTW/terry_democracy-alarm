using Democracy_Alarm.Models;
using Democracy_Alarm.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Democracy_Alarm.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(UserModel UserModel)
        {
            VotingCityViewModel _VotingCityViewModel = new VotingCityViewModel();
            if (UserModel.LogingUserID != null)
            {
                UserServices _UserServices = new UserServices();
                _UserServices.UpdateOrCreateUser(UserModel);
                if (!User.Identity.IsAuthenticated)
                {
                    Session.RemoveAll();

                    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,
                        UserModel.LogingUserID,
                        DateTime.Now,
                        DateTime.Now.AddSeconds(10),
                        false,
                        UserModel.LoginType,
                        FormsAuthentication.FormsCookiePath);

                    string encTicket = FormsAuthentication.Encrypt(ticket);

                    Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));
                }
                
            }
            VotingServices _VotingServices = new VotingServices();
            _VotingCityViewModel.Cities = _VotingServices.GetCitys();
            _VotingCityViewModel.MayorMapping = _VotingServices.GetMayorMapping();
            _VotingCityViewModel.CityVotes = _VotingServices.GetCityVotes();
            _VotingCityViewModel.LastVotingSeason = _VotingServices.GetMemberLastVotingSeason(UserModel.LogingUserID);
            _VotingCityViewModel.CurrentVotingSeason = _VotingServices.GetCurrentVotingSeason();
            _VotingCityViewModel.NextFullVotingSeason = _VotingServices.GetnNextVotingSeason(_VotingCityViewModel.LastVotingSeason);
            _VotingServices.GetVotingYearSeasonFromString(_VotingCityViewModel.NextFullVotingSeason,
                out _VotingCityViewModel.NextVotingYear,
                out _VotingCityViewModel.NextVotingSeason);
            //_VotingCityViewModel.ID = UserModel.LogingUserID;
            return View(_VotingCityViewModel);
        }
        /**
        public ActionResult Login()
        {
            
            return Content(Convert.ToInt32(IsNewLogin).ToString());
        }**/

    }
}