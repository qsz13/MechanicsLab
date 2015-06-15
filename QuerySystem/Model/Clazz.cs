using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuerySystem.Model
{
    class Clazz
    {
        public int id { get; set; }

        public String number { get; set; }

        public String clazzHour { get; set; }

        public String clazzroom { get; set; }

        public Course course { get; set; }

        public Teacher teacher { get; set; }
    }
}
