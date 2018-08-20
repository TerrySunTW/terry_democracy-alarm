using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Democracy_Alarm.Models
{
    public class VotingCityViewModel
    {
        public List<CityVoting> CityVotes;
        public string[] Cities;
        public string LastVotingSeason;
        public string ID;
    }
    public class CityVoting
    {
        public string CityName { get; set; }
        public int Votes { get; set; }
    }
}