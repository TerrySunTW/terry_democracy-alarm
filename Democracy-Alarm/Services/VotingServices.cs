using Democracy_Alarm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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
                                       where CityVotingCount.VotingTarget==Voting.Value && CityVotingCount.IsDiscard==false
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
        public string[] GetMayors()
        {
            var _result = from Citys in _MyDB_Entities.KeyValue
                          where Citys.Key == "mayor"
                          orderby Citys.OrderID
                          select Citys.Value;

            return _result.ToArray();
        }
        public Dictionary<string,string> GetMayorMapping()
        {
            var Citys = GetCitys();
            var Mayors = GetMayors();
            Dictionary<string, string> MayorMapping = new Dictionary<string, string>();
            for (int i = 0; i < Citys.Length; i++)
            {
                MayorMapping.Add(Citys[i], Mayors[i]);
            }
            return MayorMapping;
        }
        public String GetMemberLastVotingSeason(String UserID)
        {
            //get first season
            String LastVotingSeason = (from Season in _MyDB_Entities.KeyValue
                                       where Season.Key== "season"
                                       orderby Season.OrderID
                                       select Season.Value).FirstOrDefault();
            //get user votingrecord
            var UserInfo = (from User in _MyDB_Entities.Users
                       where User.UserID == UserID
                       select User).FirstOrDefault();

            if(UserInfo != null)
            {
                var UserLastSeason = (from VotingRecord in _MyDB_Entities.VotingRecords
                                      where VotingRecord.UserUID == UserInfo.UID
                                      orderby VotingRecord.UID descending
                                      select VotingRecord.VotingSeason).FirstOrDefault();
                if(UserLastSeason!=null)
                {
                    LastVotingSeason = UserLastSeason;
                }
                
            }
            return LastVotingSeason;
        }
        public String GetCurrentVotingSeason()
        {
            int YearNumber = SystemService.GetTW_Time().Year;
            String SeasonString = "Q1";
            if ((SystemService.GetTW_Time().Month >= 12) ||
                (SystemService.GetTW_Time().Month < 3)
                )
            {
                YearNumber = YearNumber - 1;
                SeasonString = "Q4";
            }
            else if ((SystemService.GetTW_Time().Month >= 3) &&
                (SystemService.GetTW_Time().Month < 6)
                )
            {
                SeasonString = "Q1";
            }
            else if ((SystemService.GetTW_Time().Month >= 6 ) &&
                (SystemService.GetTW_Time().Month < 9 )
                )
            {
                SeasonString = "Q2";
            }
            else if ((SystemService.GetTW_Time().Month >= 9 ) &&
               (SystemService.GetTW_Time().Month < 12 )
               )
            {
                SeasonString = "Q3";
            }

            return YearNumber.ToString()+SeasonString;
        }
        public string GetnNextVotingSeason(String LastVotingSeason)
        {
            String result = "2019Q1";
            int LastYear = 2019;
            int LastSeason = 1;
            GetVotingYearSeasonFromString(LastVotingSeason, out LastYear, out LastSeason);
            LastSeason++;
            if (LastSeason>4)
            {
                LastSeason = 1;
                LastYear++;
            }
            if(LastYear>0&& LastSeason>0)
            {
                result = string.Format("{0}Q{1}", LastYear.ToString(), LastSeason.ToString());
            }
            return result;
        }
        public void GetVotingYearSeasonFromString(String SourceVotingSeason, out int VotingYear,out int VotingSeason)
        {
            Regex regex = new Regex("([0-9]{0,4})Q([0-4])", RegexOptions.IgnoreCase);
            VotingYear = 2019;
            VotingSeason = 1;
            if(!string.IsNullOrEmpty(SourceVotingSeason))
            {
                int.TryParse(regex.Matches(SourceVotingSeason)[0].Groups[1].Value, out VotingYear);
                int.TryParse(regex.Matches(SourceVotingSeason)[0].Groups[2].Value, out VotingSeason);
            }
        }

        public bool CreateNewVotingRecord(string UserID, string Target,string Season,string Comment,bool IsDiscard, out string Message)
        {
            Message = string.Empty;
            bool ReturnVal = false;
            var UserInfo = (from User in _MyDB_Entities.Users
                            where User.UserID == UserID
                            select User).FirstOrDefault();
            if(UserInfo!=null)
            {
                if(Comment.Length>120)
                {
                    Comment = Comment.Substring(0, 120);
                }
                VotingRecords _VotingRecord = new VotingRecords()
                {
                    UserUID = UserInfo.UID,
                    VotingTarget = Target,
                    VotingSeason = Season,
                    VotingComment = Comment,
                    IsDiscard = IsDiscard,
                    Createtime = DateTime.UtcNow.AddHours(8)
                };
                _MyDB_Entities.VotingRecords.Add(_VotingRecord);

                _MyDB_Entities.SaveChanges();
                if (_VotingRecord.UID > 0)
                {
                    ReturnVal = true;
                }
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
                           where VotingRecord.VotingTarget == City.Value && VotingRecord.IsDiscard == false
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
        public void RemoveMemberVotingRecord(String UserID)
        {
            List<VotingResult> results = new List<VotingResult>();
            var UserInfo = (from User in _MyDB_Entities.Users
                            where User.UserID == UserID
                            select User).FirstOrDefault();

            if (UserInfo != null)
            {
                UserInfo.UserLocation = null;
                var _VotingRecords = _MyDB_Entities.VotingRecords.Where(p => p.UserUID == UserInfo.UID).ToList();
                _MyDB_Entities.VotingRecords.RemoveRange(_VotingRecords);
                _MyDB_Entities.SaveChanges();
            }
        }

    }
}