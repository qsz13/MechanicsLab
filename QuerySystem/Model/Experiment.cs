using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuerySystem.Model
{
    class Experiment
    {
        public int id { get; set; }

        public String number {get; set;}

        public String name { get; set; }

        public int minGrpStuCnt { get; set; }

        public int maxGrpStuCnt { get; set; }

        public String virtualExpLink { get; set; }

        public bool active { get; set; }

    }
}
