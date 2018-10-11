using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Democracy_Alarm.ExtensionMethods
{
    public static class MyExtensions
    {
        public static string ToChineseSeason(this String str)
        {
            return str.Replace("Q1", " 第一季").Replace("Q2", " 第二季").Replace("Q3", " 第三季").Replace("Q4", " 第四季");
        }
    }
}