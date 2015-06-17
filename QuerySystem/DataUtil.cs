using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuerySystem.Model;
using QuerySystem.Exceptions;
using System.Windows;

namespace QuerySystem
{
    class DataUtil
    {
        /**
         * 维护accountId
         * 全局变量 全局设定参数，每页的数量，点击按钮维护curPageNum
         * */
        public const int numPerPage = 8;
        public static int curPageNum = 0;
        public static int totalItemNum = 0;
        public static int totalPageNum = 1;//Constraint: curPageNum < totalPageNum
        public static String studentName = "";
        public static String accountId ="";
        public static bool userExist = false;

        public static List<MyMessage> goNextPage()
        {
            if (curPageNum >= totalPageNum)
            {
                curPageNum = totalPageNum;
                log("In the end of pages");
            }else {
                curPageNum++;
            }
            return getMyMsgList();
        }

        public static List<MyMessage> goPriviousPage()
        {
            if (curPageNum <= 1)
            {
                curPageNum = 1;
                log("In the beginning of pages");
            }
            else
            {
                curPageNum--;
            }
            return getMyMsgList();
        }


        private static List<MyMessage> getMyMsgList()
        {
            List<MyMessage> result = new List<MyMessage>();
            //<GET LISTS>
            ReservationList resvList = getDataFromServer();//For example ... need change....

            if (resvList == null)
            {
                log("get reservation list is null");
                return null;
            }

            //Bind Data....
            foreach (var item in resvList.data)
            {
                MyMessage mmTemp = new MyMessage();
                mmTemp.Recode_id = item.number;
                mmTemp.Date = item.applyDate;//????
                mmTemp.Time = item.slot.title;
                mmTemp.Lab = item.lab.name;
                mmTemp.Content = item.experiment.name;
                mmTemp.Pass_statu = item.status;
                result.Add(mmTemp);
            }

            //refresh data..
            curPageNum = resvList.curPageNum;
            totalItemNum = resvList.totalItemNum;
            totalPageNum = resvList.totalPageNum;
            studentName = resvList.accountName;

            return result;
        }

        private static ReservationList getDataFromServer()
        {
            ReservationList result = null;
            try
            {
                result = LabClient.getReservation(accountId, numPerPage, curPageNum);

            }
            catch (AccountNotFoundException)
            {
                Console.WriteLine("AccountNotFoundException");
                MessageBox.Show("您查询的用户不存在.请检查您的拼写");
            }
            catch(ServerNotResponseException)
            {
                Console.WriteLine("ServerNotResponseException");
                MessageBox.Show("服务端未响应，连接超时，请检查您的网络连接和防火墙设置");
            }
            return result;
        }

        private static ReservationList getDataFromServer(RequestType requestType)
        {

            ReservationList result = null;
            switch (requestType)
            {
                case RequestType.Student:
                    //result = LabClient.getStudentReservationList(accountId, numPerPage, curPageNum);
                    break;
                case RequestType.Teacher:
                    //result = LabClient.getTeacherReservationList(accountId, numPerPage, curPageNum);
                    break;
                case RequestType.Clazz:
                    //result = LabClient.getClazzReservationList(accountId, numPerPage, curPageNum);
                    break;
                default:
                    log("Not Recognized Request Type");
                    break;
            }

            return result;
        }

        public enum RequestType {Student, Clazz, Teacher};

        private static void log(String str)
        {
            if (str == null) return;
            Console.WriteLine(str);
        }
    }
}
