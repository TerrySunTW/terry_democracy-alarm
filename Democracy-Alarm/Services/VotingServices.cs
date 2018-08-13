using Democracy_Alarm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Democracy_Alarm.Services
{
    public class VotingServices
    {
        MyDB_Entities _MyDB_Entities = new MyDB_Entities();
        public List<CityVoting> GetCityVotes()
        {
            var _result = from Voting in _MyDB_Entities.KeyValue
                          join vt in _MyDB_Entities.VotingRecords on
                           new { X1 = Voting.Key } equals new { X1 = vt.VotingTarget }
                          group vt by vt.VotingTarget into VotingResult
                          select new CityVoting {  CityName = VotingResult.Key, Votes = VotingResult.Count() };

            return _result.ToList();

        }
    }
}