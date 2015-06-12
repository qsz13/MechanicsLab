using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisplayBoard
{
    class LabClient
    {

        private static String token = null;
        private static int semester;
        
        public static bool Login(String username, String password)
        {
            var client = new RestClient("http://ioc.yiliang.me");
            var request = new RestRequest("api/token", Method.POST);
            request.AddHeader("X-Username", username);
            request.AddHeader("X-Password", password);
            client.ExecuteAsync(request, response =>
            {
                token = response.Headers.ToList().Find(x => x.Name == "X-Auth-Token").Value.ToString();
                getSemester();
            });

            return true;
            
        }
        
        public static bool getSemester()
        {
            var client = new RestClient("http://ioc.yiliang.me");
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
            var client = new RestClient("http://ioc.yiliang.me");
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

                JArray reserVationData = (JArray)reservationJObject["data"];

                List<Reservation> reservation = JsonConvert.DeserializeObject<List<Reservation>>(reserVationData.ToString(), 
                    new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects });
                        

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
        



    }
}
