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
        private static int semester;
        private static String API_URL = "http://202.120.188.5";
        
        public static bool Login(String username, String password)
        {
            var client = new RestClient(API_URL);
            var request = new RestRequest("api/token", Method.POST);
            request.AddHeader("X-Username", username);
            request.AddHeader("X-Password", password);
            try
            {

                client.ExecuteAsync(request, response =>
                {
                    token = response.Headers.ToList().Find(x => x.Name == "X-Auth-Token").Value.ToString();
                    getSemester();
                    
                });
            }
            catch
            {
                Console.WriteLine("no connection");
            }

            return true;
            
        }
        
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
        }
        
        public static bool getReservation(int day)
        {
            var client = new RestClient(API_URL);
            String request_url = String.Format("/api/reservation/semester/{0}/list/all?startDate={1}&endDate={2}&status=APPROVED",
                semester, DateTime.Now.AddDays(day).ToString("yyyy-MM-dd"), DateTime.Now.AddDays(day).ToString("yyyy-MM-dd"));
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


                getLabTeacher(reservation);

                if(day == 0)
                {
                    LabUtil.reservationTodayList = reservation;
                }
                else if(day==1)
                {
                    LabUtil.reservationTomorrowList = reservation;
                }
            }
            
            


            //ioc.yiliang.me/api/reservation/semester/3/list/all?startDate=2015-06-09&endDate=2015-06-09
            return true;

        }

        public static bool getLabTeacher(List<Reservation> reserationList)
        {
            foreach(var r in reserationList)
            {
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
            }



            return true;
        }




        



    }
}
