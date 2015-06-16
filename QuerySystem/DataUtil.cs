using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuerySystem.Model;

namespace QuerySystem
{
    class DataUtil
    {
        private static List<MyMessage> getMyMsgList()
        {
            List<MyMessage> result = new List<MyMessage>();
            //<GET LISTS>
            ReservationList resvList = getDataFromServer(RequestType.Student);//For example ... need change....

            //Bind Data....


            return result;
        }

        private static ReservationList getDataFromServer(RequestType requestType)
        {
            ReservationList result = null;
            switch (requestType)
            {
                case RequestType.Student:
                    
                    break;
                case RequestType.Teacher:
                    break;
                case RequestType.Clazz:
                    break;
                default:
                    log("Not Recognized Request Type");
                    break;
            }

            return result;
        }

        public static enum RequestType {Student, Clazz, Teacher};

        private static void log(String str)
        {
            if (str == null) return;
            Console.WriteLine(str);
        }
    }
}
