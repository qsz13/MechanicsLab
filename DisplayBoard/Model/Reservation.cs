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

        public String applyDate { get; set; }

        public Slot slot { get; set; }

        public Experiment experiment { get; set; }

        public Lab lab { get; set; }

        public Clazz clazz { get; set; }

        public String status { get; set; }

        public int count { get; set; }


    }
}
