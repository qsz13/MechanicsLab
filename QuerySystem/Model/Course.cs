using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuerySystem.Model
{
    class Course
    {
        public int id { get; set; }

        public String number { get; set; }

        public String name { get; set; }

        public String department { get; set; }

        public String startYear { get; set; }

        public bool active { get; set; }
    }
}
