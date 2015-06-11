using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisplayBoard
{
    class Slot
    {
        public int id { get; set; }
        public int slotNo { get; set; }

        public String startTime { get; set; }

        public String endTime { get; set; }

        public bool active { get; set; }

        public String title { get; set; }
    }
}
