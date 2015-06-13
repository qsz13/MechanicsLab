using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuerySystem.Model
{
    class ReservationList
    {
        public List<Reservation> data{set; get;};
        
        public int numPerPage {get; set;}

        public int curPageNum {get; set;}

        public int totalItemNum{get;set;}

        public int totalPageNum{get; set;}




    }
}
