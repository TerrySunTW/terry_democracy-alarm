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
                                       where CityVotingCount.VotingTarget==Voting.Value
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
        public String GetMemberLastVotingSeason(String UserID)
        {
            String LastVotingSeason = (from Season in _MyDB_Entities.KeyValue
                                       where Season.Key== "season"
                                       orderby Season.OrderID
                                       select Season.Value).FirstOrDefault();
            var UserInfo = (from User in _MyDB_Entities.Users
                       where User.UserID == UserID
                       select User).FirstOrDefault();

            if(UserInfo != null)
            {
                LastVotingSeason = (from VotingRecord in _MyDB_Entities.VotingRecords
                                    where VotingRecord.UserUID == UserInfo.UID
                                    orderby VotingRecord.Createtime descending
                                    select VotingRecord.VotingSeason).FirstOrDefault();
            }
            return LastVotingSeason;
        }

        public bool CreateNewVotingRecord(string UserID, string Target,string Season,string Comment,bool IsDiscard, out string Message)
        {
            Message = string.Empty;
            bool ReturnVal = false;
            var UserInfo = (from User in _MyDB_Entities.Users
                            where User.UserID == UserID
                            select User).FirstOrDefault();
            VotingRecords _VotingRecord=new VotingRecords()
            {
                UserUID = UserInfo.UID,
                VotingTarget = Target,
                VotingSeason = Season,
                VotingComment = Comment,
                IsDiscard = IsDiscard,
                Createtime = DateTime.Now
            };
            _MyDB_Entities.VotingRecords.Add(_VotingRecord);
            
            _MyDB_Entities.SaveChanges();
            if (_VotingRecord.UID>0)
            {
                ReturnVal = true;
            }
            return ReturnVal;
        }
        public List<VotingResult> GetMemberVotingRecord(String UserID)
        {
            List<VotingResult> results = new List<VotingResult>();
            var UserInfo = (from User in _MyDB_Entities.Users
                            where User.UserID == UserID
                            select User).FirstOrDefault();

            if (UserInfo != null)
            {

                results = (from VotingRecord in _MyDB_Entities.VotingRecords
                           join User in _MyDB_Entities.Users on VotingRecord.UserUID equals User.UID
                           where VotingRecord.UserUID == UserInfo.UID
                            orderby VotingRecord.Createtime descending
                           select new VotingResult()
                           {
                               UserName = User.UserName,
                               UserImage = User.UserImage,
                               VotingComment = VotingRecord.VotingComment,
                               VotingTarget = VotingRecord.VotingTarget,
                               VotingTime = VotingRecord.Createtime,
                               VotingSeason = VotingRecord.VotingSeason,
                               IsDiscard = VotingRecord.IsDiscard
                           }).ToList();

            }
            return results;
        }
        public List<VotingResult> GetCityVotingRecords(String CityName)
        {
            List<VotingResult> results = new List<VotingResult>();
            var City = (from Citys in _MyDB_Entities.KeyValue
                        where Citys.Key == "city" && Citys.Value== CityName
                        select Citys).FirstOrDefault();

            if (City != null)
            {

                results = (from VotingRecord in _MyDB_Entities.VotingRecords
                           join User in _MyDB_Entities.Users on VotingRecord.UserUID equals User.UID
                           where VotingRecord.VotingTarget == City.Value
                           orderby VotingRecord.Createtime descending
                           select new VotingResult()
                           {
                               UserName = User.UserName,
                               UserImage = User.UserImage,
                               VotingComment = VotingRecord.VotingComment,
                               VotingTarget= VotingRecord.VotingTarget,
                               VotingTime= VotingRecord.Createtime,
                               VotingSeason= VotingRecord.VotingSeason
                           }).ToList();

            }
            return results;
        }

    }
}