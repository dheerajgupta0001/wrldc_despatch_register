﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Notesheets
{
    public class ModeOfTenderConstants
    {
        public const string OT_PB= "Open Tender (Paper based)";
        public const string LT_PB = "Limited Tender (Paper based)";
        public const string ST = "Single Tender";
        public const string ST_PAC = "Single Tender PAC based";
        public const string MS_SE = "Market Survey through Spot enquiry";
        public const string CPP_OT = "CPP Portal Open Tender";
        public const string CPP_LTE = "CPP Portal LTE";
        public const string GEM_DP = "GeM Direct Purchase";
        public const string GEM_L1 = "GeM L-1 Purchase";
        public const string GEM_BIDDING = "GeM Bidding";

        public static List<string> GetModeOfTenderOptions()
        {
            return typeof(ModeOfTenderConstants).GetFields().Select(x => x.GetValue(null).ToString()).ToList();
        }
    }
}