using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HYCWEB.Models.ViewModels
{
    public class vEmployeeWorktime
    {
        [System.ComponentModel.DisplayName("序號")] 
        public int ASS_SN { get; set; }

        [System.ComponentModel.DisplayName("員工編號")] 
        public string AS_PSN { get; set; }

        [System.ComponentModel.DisplayName("使用者編號")] 
        public string AS_USERID { get; set; }

        [System.ComponentModel.DisplayName("上班日期")] 
        public int AS_DATE { get; set; }

        [System.ComponentModel.DisplayName("上班打卡")] 
        public int AS_STIME { get; set; }

        [System.ComponentModel.DisplayName("下班打卡")] 
        public int AS_ETIME { get; set; }

        [System.ComponentModel.DisplayName("時數")] 
        public double? AS_HOURS { get; set; }

        public DateTime? AS_UPDATE { get; set; }

        public string AS_UPDUSER { get; set; }
        public string SHOWTIME { get; set; }
    }
}