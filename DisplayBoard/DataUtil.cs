using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DisplayBoard.Model;

namespace DisplayBoard
{
    class DataUtil
    {
        public static int todayExpSum = 0;//Today Experiment Number
        public static int tommorrowExpSum = 0;//Tommorrow Experiment Number
        public static List<Reservation> resvtTodayListCopy = null;
        public static List<Reservation> resvtTomorrowListCopy = null;
        public static List<MyMessage> ongoingExpMsg = new List<MyMessage>();//Ongoing Experiment Messages
        public static List<MyMessage> upcomingExpMsg = new List<MyMessage>();//Upcoming Experiment Messages
        public static List<MyMessage> tommorrowExpMsg = new List<MyMessage>();//Tommorrow Experiment Messages
        private static int iOngoing=0, iUpcoming=0, iTommorrow=0;

        public static void getOngingExpMsg(List<Reservation> todayList)
        {

        }
        public static void getUpcomingExpMsg(List<Reservation> todayList)
        {

        }

        /**
         * Every time call this method, all ongoingExpMsg and upcomingExpMsg will be
         * refreshed!! be careful
         * May exist some error?? Check
         * */
        public static void getTodayExpMsg(List<Reservation> todayList)
        {
            if (todayList == null) return;
            
            //refresh all data, clear it!
            ongoingExpMsg.Clear();
            upcomingExpMsg.Clear();

            foreach (var item in todayList)
            {
                int resu = compaireTime(item.slot.startTime, item.slot.endTime);
                if (resu == -1) continue;
                MyMessage mmTemp = convertResvtToMyMsg(item);

                if (resu == 0)
                {
                    ongoingExpMsg.Add(mmTemp);                    
                }
                else if (resu == 1)
                {
                    upcomingExpMsg.Add(mmTemp);
                }

            }
            DataUtil.todayExpSum = ongoingExpMsg.Count + upcomingExpMsg.Count;

        }

        public static void getTomorrowExpMsg(List<Reservation> tommorrowList)
        {
            if (tommorrowList == null) return;

            foreach (var item in tommorrowList)
            {
                MyMessage mmTemp = convertResvtToMyMsg(item);
                tommorrowExpMsg.Add(mmTemp);
            }
            tommorrowExpSum = tommorrowExpMsg.Count;
        }

        /**
         * bind data
         **/
        private static MyMessage convertResvtToMyMsg(Reservation resvt)
        {
            MyMessage myMsg = new MyMessage();
            myMsg.m_lab = resvt.lab.name;
            myMsg.m_statu = "时段"+resvt.slot.slotNo+"- "+"<正在进行?>";
            myMsg.m_content = resvt.experiment.name;
            myMsg.m_class = resvt.clazz.course.number + " " + resvt.clazz.course.name + " " + resvt.clazz.teacher.name;
            myMsg.m_people = "";
            return myMsg;
        }

        public static int compaireTime(String startTime, String endTime)
        {
            DateTime dtStart = Convert.ToDateTime(startTime);
            DateTime dtEnd = Convert.ToDateTime(endTime);
            DateTime dtNow = DateTime.Now;
            //Console.WriteLine(dtStart);
            //Console.WriteLine(dtEnd);
            if (DateTime.Compare(dtNow, dtStart) < 0)
            {
                //No use date, Overdue: dtNow<dtStart
                return -1;
            }
            else if (DateTime.Compare(dtEnd, dtNow) <= 0)
            {
                //:dtEnd <= dtNow
                return 1;
            }
            else
            {
                //:dtStart <= dtNow < dtEnd
                return 0;
            }

        }

        public static MyMessage getNextOngoingMyMsg()
        {
            return null;
        }
        public static MyMessage getNextUpcomingMyMsg()
        {
            return null;
        }
        public static MyMessage getNextTommorrowMyMsg()
        {
            return null;
        }

    }
}
