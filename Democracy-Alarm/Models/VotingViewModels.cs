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
    public class CityVotingViewModel
    {
        public string CityName { get; set; }
        public List<VotingResult> VoteResult { get; set; }
    }
    public class PersonalVotingViewModel
    {
        public List<VotingResult> VoteResult { get; set; }
    }
    public class VotingResult
    {
        public string UserName { get; set; }
        public string UserImage { get; set; }
        public string VotingTarget { get; set; }
        public string VotingSeason { get; set; }
        public string VotingComment { get; set; }
        public bool IsDiscard { get; set; }
        public DateTime VotingTime { get; set; }
    }

}