using Democracy_Alarm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Democracy_Alarm.Services
{
    public class UserServices
    {
        MyDB_Entities _MyDB_Entities = new MyDB_Entities();
        public Users GetUserObject(string UserID)
        {
            Users DBUser = _MyDB_Entities.Users.SingleOrDefault(p => p.UserID == UserID);
            return DBUser;
        }
        public string GetUserLocation(string UserID)
        {
            string Location = "";
            Users DBUser = _MyDB_Entities.Users.SingleOrDefault(p => p.UserID == UserID);
            if(DBUser!=null&& string.IsNullOrEmpty(DBUser.UserLocation)==false)
            {
                Location = DBUser.UserLocation;
            }
            return Location;
        }
        public string UpdateUserLocation(string UserID,string Location)
        {
            Users DBUser = _MyDB_Entities.Users.SingleOrDefault(p => p.UserID == UserID);
            if (DBUser != null)
            {
                DBUser.UserLocation= Location;
                _MyDB_Entities.SaveChanges();
            }
            return Location;
        }
        public void UpdateOrCreateUser(UserModel _UserModel)
        {
            var DBUser = _MyDB_Entities.Users.SingleOrDefault(p => p.UserID == _UserModel.LogingUserID && p.LoginType == _UserModel.LoginType);

            if(DBUser ==null)
            {
                string UserImageUrl=string.Empty;
                switch (_UserModel.LoginType)
                {
                    case "facebook":
                        UserImageUrl = string.Format("http://graph.facebook.com/{0}/picture?type=square", _UserModel.LogingUserID);
                        break;
                }

                DBUser = new Users()
                {
                    UserID = _UserModel.LogingUserID,
                    LoginType = _UserModel.LoginType,
                    UserName = _UserModel.LoginUserName,
                    UserImage = UserImageUrl,
                    Createtime = SystemService.GetTW_Time()
                };
                //new item
                _MyDB_Entities.Users.Add(DBUser);
            }
            else
            {
                DBUser.UserName = _UserModel.LoginUserName;
            }
            _MyDB_Entities.SaveChanges();
        }

    }
}