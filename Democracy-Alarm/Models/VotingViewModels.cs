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
        public Dictionary<string,string> MayorMapping;
        public string LastVotingSeason;
        public int CurrentVotingYear;
        public int CurrentVotingSeason;
        public int NextVotingYear;
        public int NextVotingSeason;
        
        public string NextFullVotingSeason;
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
    public class UserVotingInfo
    {
        public string LastVotingSeason { get; set; }
        public string NextFullVotingSeason { get; set; }
        public int CurrentVotingYear { get; set; }
        public int CurrentVotingSeason { get; set; }
        public int NextVotingYear { get; set; }
        public int NextVotingSeason { get; set; }
    }

}