using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HYCWEB.Models.ViewModels
{
    public class vEmployeeListSummary
    {
        [System.ComponentModel.DisplayName("員工")]
        public string as_psn { get; set; }

        [System.ComponentModel.DisplayName("月份")]
        public string as_workmonth { get; set; }

        [System.ComponentModel.DisplayName("總時數")]
        public string as_totaltime { get; set; }

        [System.ComponentModel.DisplayName("每月基本工時")]
        public string as_basetime { get; set; }

        [System.ComponentModel.DisplayName("超(少)工時")]
        public string as_timegap { get; set; }
        [System.ComponentModel.DisplayName("換算結果")]
        public string conversion { get; set; }
    }
}