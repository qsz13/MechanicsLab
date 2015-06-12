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

        public static void getOngingExpMsg(List<Reservation> todayList)
        {

        }
        public static void getUpcomingExpMsg(List<Reservation> todayList)
        {

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

        public static bool compaireTime()
        {

        }

    }
}
