//------------------------------------------------------------------------------
// <auto-generated>
//     這個程式碼是由範本產生。
//
//     對這個檔案進行手動變更可能導致您的應用程式產生未預期的行為。
//     如果重新產生程式碼，將會覆寫對這個檔案的手動變更。
// </auto-generated>
//------------------------------------------------------------------------------

namespace HYCWEB.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class AS_WORKTIME
    {
        public int ASS_SN { get; set; }
        public string AS_PSN { get; set; }
        public string AS_USERID { get; set; }
        public int AS_DATE { get; set; }
        public string AS_CLASSTYPE { get; set; }
        public int AS_STIME { get; set; }
        public int AS_ETIME { get; set; }
        public Nullable<double> AS_HOURS { get; set; }
        public Nullable<System.DateTime> AS_UPDATE { get; set; }
        public string AS_UPDUSER { get; set; }
    }
}
