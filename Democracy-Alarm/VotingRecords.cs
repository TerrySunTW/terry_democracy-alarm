//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Democracy_Alarm
{
    using System;
    using System.Collections.Generic;
    
    public partial class VotingRecords
    {
        public int UID { get; set; }
        public int UserUID { get; set; }
        public string VotingTarget { get; set; }
        public string VotingSeason { get; set; }
        public string VotingComment { get; set; }
        public System.DateTime Createtime { get; set; }
        public bool IsDiscard { get; set; }
    }
}
