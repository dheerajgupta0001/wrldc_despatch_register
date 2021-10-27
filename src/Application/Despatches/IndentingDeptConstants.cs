using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Despatches
{
    public class IndentingDeptConstants
    {
        public const string IT = "IT";
        public const string RE_R = "RE&R";
        public const string TS = "TS";
        public const string HR = "HR";
        public const string Logistic = "Logistic";
        public const string Finance = "Finance";
        public const string MO = "MO";
        public const string CR = "CR";
        public const string SO1 = "SO-1";
        public const string SO2 = "SO-2";

        public static List<string> GetIndentingDeptOptions()
        {
            return typeof(IndentingDeptConstants).GetFields().Select(x => x.GetValue(null).ToString()).ToList();
        }
    }
}
