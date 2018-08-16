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
                          where Voting.Key=="city"
                          select new CityVoting {
                              CityName = Voting.Value,
                              Votes = (from CityVotingCount in _MyDB_Entities.VotingRecords
                                       where CityVotingCount.VotingTarget==Voting.Key
                                       select CityVotingCount
                                       ).Count()
                          };

            return _result.ToList();

        }
        public string[] GetCitys()
        {
            var _result = from Citys in _MyDB_Entities.KeyValue
                          where Citys.Key == "city"
                          orderby Citys.OrderID
                          select Citys.Value;

            return _result.ToArray();

        }
    }
}