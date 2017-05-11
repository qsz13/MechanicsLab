using DisplayBoard.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DisplayBoard
{
    class LabClient
    {

        private static String token = null;
        // private static int semester;
        private static String API_URL = "http://labcom.tongji.edu.cn/";
        
        public static bool Login(String username, String password)
        {
            var client = new RestClient(API_URL);
            var request = new RestRequest("api/token", Method.POST);
            request.AddHeader("X-Username", username);
            request.AddHeader("X-Password", password);


            client.ExecuteAsync(request, response =>
            {

                try
                {
                    //don't need semester and token now.
                    //token = response.Headers.ToList().Find(x => x.Name == "X-Auth-Token").Value.ToString();
                    //getSemester();
                    getReservation(0);
                    getReservation(1);
                }
                catch
                {
                }
            });


            return true;
            
        }
        /* 
         * don't need this function anymore. labteacher list contained in the reservation now.
        public static bool getSemester()
        {
            var client = new RestClient(API_URL);
            var request = new RestRequest("/api/semester/current", Method.GET);
            request.AddHeader("X-Auth-Token", token);
            client.ExecuteAsync(request, response =>
            {
                if (response.StatusCode.ToString() == "OK")
                {
                    JObject semesterJObject = JObject.Parse(response.Content);

                        semester = (int)semesterJObject["data"]["id"];
                        getReservation(0);
                        getReservation(1);
                        Console.WriteLine("get semester");
                }
            });
       

            return true;
        }*/
        public static bool testNextwork()
        {
            int day = 0;
            try
            {
                var client = new RestClient(API_URL);
                String request_url = String.Format("/api/reservation/reservations?scope=all&startDate={0}T00:00:00.000Z&endDate={1}T00:00:00.000Z",
                    DateTime.Now.AddDays(day).ToString("yyyy-MM-dd"), DateTime.Now.AddDays(day + 1).ToString("yyyy-MM-dd"));
                System.Console.WriteLine(request_url);
                var request = new RestRequest(request_url, Method.GET);

                request.AddHeader("X-Auth-Token", token);

                IRestResponse response = client.Execute(request);
                var content = response.Content; // raw content as string
                content = content.Replace(@"@", "$");
                if (response.StatusCode.ToString() == "OK")
                {            
                    DataUtil.is_connected = true;
                    LabUtil.is_connected = true;
                }
                else
                {
                    DataUtil.is_connected = false;
                    LabUtil.is_connected = false;
                }
            }
            catch
            {
                DataUtil.is_connected = false;
                LabUtil.is_connected = false;
            }
            //ioc.yiliang.me/api/reservation/semester/3/list/all?startDate=2015-06-09&endDate=2015-06-09
            return true;
        }
        public static bool getReservation(int day)
        {
            try
            {
                var client = new RestClient(API_URL);
                String request_url = String.Format("/api/reservation/reservations?scope=all&startDate={0}T00:00:00.000Z&endDate={1}T00:00:00.000Z",
                    DateTime.Now.AddDays(day).ToString("yyyy-MM-dd"), DateTime.Now.AddDays(day + 1).ToString("yyyy-MM-dd"));
                System.Console.WriteLine(request_url);
                var request = new RestRequest(request_url, Method.GET);

                request.AddHeader("X-Auth-Token", token);

                IRestResponse response = client.Execute(request);
                var content = response.Content; // raw content as string
                content = content.Replace(@"@", "$");
                if (response.StatusCode.ToString() == "OK")
                {
                    JObject reservationJObject = JObject.Parse(content);

                    JArray reservationData = (JArray)reservationJObject["data"];

                    List<Reservation> reservation = JsonConvert.DeserializeObject<List<Reservation>>(reservationData.ToString(),
                        new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects });
                    //getLabTeacher(reservation);
                    filterApply(reservation);
                    if (day == 0)
                    {
                        LabUtil.reservationTodayList = reservation;
                    }
                    else if (day == 1)
                    {
                        LabUtil.reservationTomorrowList = reservation;
                    }
                }
                else
                {

                }
            }
            catch (Exception mye)
            {
                System.Console.WriteLine("error");
            }
            //ioc.yiliang.me/api/reservation/semester/3/list/all?startDate=2015-06-09&endDate=2015-06-09
            return true;
        }
        public static bool filterApply(List<Reservation> reserationList)
        {
            int count = reserationList.Count;
            while (count>0) 
            {
                count--;
                var r = reserationList.ElementAt(count);
                if (r.status.code != "APPROVED")
                {
                    reserationList.RemoveAt(count);
                }
            }

            return true;
        }
        public static bool getLabTeacher(List<Reservation> reserationList)
        {
            /*foreach(var r in reserationList)
            {
                r.labTeacherList = r.labTeacherList;
                int rid = r.id;
                var client = new RestClient(API_URL);
                String request_url = String.Format("/api/reservation/{0}/labteachers",rid);
                var request = new RestRequest(request_url, Method.GET);
                request.AddHeader("X-Auth-Token", token);

                IRestResponse response = client.Execute(request);
                var content = response.Content; // raw content as string
                content = content.Replace(@"@", "$");
                if (response.StatusCode.ToString() == "OK")
                {
                    JObject labTeacherJObject = JObject.Parse(content);
                    JArray labTeacherData = (JArray)labTeacherJObject["data"];
                     List<LabTeacher> labTeacherList = JsonConvert.DeserializeObject<List<LabTeacher>>(labTeacherData.ToString(), 
                    new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects });
                     r.labTeacherList = labTeacherList;

                }
            }*/
            return true;
        }

    }
}
