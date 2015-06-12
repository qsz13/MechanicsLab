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
        public static int tomorrowExpSum = 0;//Tomorrow Experiment Number
        public static List<Reservation> resvtTodayListCopy = null;
        public static List<Reservation> resvtTomorrowListCopy = null;
        public static List<MyMessage> ongoingExpMsg = new List<MyMessage>();//Ongoing Experiment Messages
        public static List<MyMessage> upcomingExpMsg = new List<MyMessage>();//Upcoming Experiment Messages
        public static List<MyMessage> tomorrowExpMsg = new List<MyMessage>();//Tomorrow Experiment Messages
        private static int iOngoing=0, iUpcoming=0, iTomorrow=0;

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
        public static void getTodayExpMsg(List<Reservation> todayList, int flag)
        {
            if (todayList == null) return;
            
            //refresh all data, clear it!
            if(flag == 0) ongoingExpMsg.Clear();
            if(flag == 1) upcomingExpMsg.Clear();

            foreach (var item in todayList)
            {
                int resu = compaireTime(item.slot.startTime, item.slot.endTime);
                if (resu == -1) continue;
                MyMessage mmTemp = convertResvtToMyMsg(item);

                if (resu == 0&&flag==0)
                {
                    ongoingExpMsg.Add(mmTemp);                    
                }
                else if (resu == 1&&flag==1)
                {
                    upcomingExpMsg.Add(mmTemp);
                }

            }
            DataUtil.todayExpSum = ongoingExpMsg.Count + upcomingExpMsg.Count;

        }

        public static void getTomorrowExpMsg(List<Reservation> tomorrowList)
        {
            if (tomorrowList == null) return;

            foreach (var item in tomorrowList)
            {
                MyMessage mmTemp = convertResvtToMyMsg(item);
                tomorrowExpMsg.Add(mmTemp);
            }
            tomorrowExpSum = tomorrowExpMsg.Count;
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
                //dtNow<dtStart
                return 1;
            }
            else if (DateTime.Compare(dtEnd, dtNow) <= 0)
            {
                //No use date, Overdue:dtEnd <= dtNow
                return -1;
            }
            else
            {
                //:dtStart <= dtNow < dtEnd
                return 0;
            }

        }

        public static MyMessage getNextOngoingMyMsg()
        {
            //if (ongoingExpMsg.Count == 0) return getDefaultMyMsg();

            if (iOngoing == ongoingExpMsg.Count)
            {
                iOngoing = 0;
                //NEED REPLAY!
                //GET NEW INFO ADN THEN RFRESH ALL MSGES
                if (LabUtil.reservationTodayList != null)
                {
                    resvtTodayListCopy = LabUtil.reservationTodayList;
                }
                else log("Refresh at:" + DateTime.Now + " reservationTodayList is null");
                //Refresh complete May be need lock
                getTodayExpMsg(resvtTodayListCopy,0);
                if (ongoingExpMsg.Count == 0) return getDefaultMyMsg();
            }
            MyMessage result = ongoingExpMsg.ElementAt(iOngoing);
            iOngoing++;
            return result;
        }
        public static MyMessage getNextUpcomingMyMsg()
        {
            //if (ongoingExpMsg.Count == 0) return getDefaultMyMsg();

            if (iUpcoming == upcomingExpMsg.Count)
            {
                iUpcoming = 0;
                //NEED REPLAY!
                //GET NEW INFO ADN THEN RFRESH ALL MSGES
                if (LabUtil.reservationTodayList != null)
                {
                    resvtTodayListCopy = LabUtil.reservationTodayList;
                }
                else log("Refresh at:" + DateTime.Now + " reservationTodayList is null");
                //Refresh complete May be need lock
                getTodayExpMsg(resvtTodayListCopy, 1);
                if (upcomingExpMsg.Count == 0) return getDefaultMyMsg();
            }
            MyMessage result = upcomingExpMsg.ElementAt(iUpcoming);
            iUpcoming++;
            return result;
        }
        public static MyMessage getNextTomorrowMyMsg()
        {
            //if (ongoingExpMsg.Count == 0) return getDefaultMyMsg();

            if (iTomorrow == tomorrowExpMsg.Count)
            {
                iTomorrow = 0;
                //NEED REPLAY!
                //GET NEW INFO ADN THEN RFRESH ALL MSGES
                if (LabUtil.reservationTomorrowList != null)
                {
                    resvtTomorrowListCopy = LabUtil.reservationTomorrowList;
                }
                else log("Refresh at:" + DateTime.Now + " reservationTomorrowList is null");
                //Refresh complete May be need lock
                getTomorrowExpMsg(resvtTomorrowListCopy);
                if (tomorrowExpMsg.Count == 0) return getDefaultMyMsg();
            }
            MyMessage result = tomorrowExpMsg.ElementAt(iTomorrow);
            iTomorrow++;
            return result;
        }

        private static MyMessage getDefaultMyMsg()
        {
            MyMessage mm = new MyMessage();
            mm.m_lab = "没有下一个";
            mm.m_statu = "";
            mm.m_content = "";
            mm.m_class = "";
            mm.m_people = "";
            return mm;
        }

        public static void log(String content)
        {
            Console.WriteLine(content);
        }
    }
}
