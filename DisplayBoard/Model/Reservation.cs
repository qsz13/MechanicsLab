using DisplayBoard.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisplayBoard
{
    class Reservation
    {
        public int id {get;set;}

        public String number { get; set; }

        public String personCount { get; set; }
        public Experiment experiment { get; set; }
        public Lab lab { get; set; }
        public String account { get; set; }
        public Clazz clazz { get; set; }
        public Slot slot { get; set; }

        public String applyDate { get; set; }

        public String remark { get; set; }

        public Status status { get; set; }

        public List<LabTeacher> teachers{get;set;}

        public bool isExpire { get; set; }
        public String timeStatus { get; set; }
    }
}
