using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Democracy_Alarm.Services
{
    public class SystemService
    {
        public static DateTime GetTW_Time()
        {
            return DateTime.UtcNow.AddHours(8);
        }
    }
}
