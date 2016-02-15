using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisplayBoard.Model
{
    class LabTeacher
    {
        public int id { get; set; }
        public String account { get; set; }
        public String password { get; set; }
        public String name { get; set; }
        public Gender gender { get; set; }
        public String mobile { get; set; }
        public String grade { get; set; }
        public String major { get; set; }
        public String mail { get; set; }
        public String title { get; set; }
        public String department { get; set; }
        public String remark { get; set; }
        public List<Roles> roles { get; set; }
        public List<Permissions> permissions { get; set; }


    }
}
