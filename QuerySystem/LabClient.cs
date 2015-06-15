using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json.Linq;
using QuerySystem.Model;
using Newtonsoft.Json;

namespace QuerySystem
{
    class LabClient
    {
        private String token = null;
        private int semester;
        private String API_URL = "http://202.120.188.5";

        public bool Login(String username, String password)
        {
            var client = new RestClient(API_URL);
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

        public bool getSemester()
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
                    //getReservation(0);
                    //getReservation(1);

                }

            });
            return true;
        }

        public ReservationList getStudentReservationList(int accountID, int pageSize, int pageNumber)
        {
            var client = new RestClient(API_URL);
            String request_url = String.Format("/api/reservation/student/{0}/page/{1}/{2}?semester={3}&type=student", accountID, pageSize, pageNumber, this.semester);
            var request = new RestRequest(request_url, Method.GET);
            request.AddHeader("X-Auth-Token", token);
            IRestResponse response = client.Execute(request);
            var content = response.Content; // raw content as string
            content = content.Replace(@"@", "$");
            if (response.StatusCode.ToString() == "OK")
            {
                ReservationList reservationList = JsonConvert.DeserializeObject<ReservationList>(content.ToString(),
                    new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects });
                return reservationList;
            }
            
            return null;
        }

        public ReservationList getClazzReservationList(int accountID, int pageSize, int pageNumber)
        {
            var client = new RestClient(API_URL);
            String request_url = String.Format("/api/reservation/student/{0}/page/{1}/{2}?semester={3}&type=clazz", accountID, pageSize, pageNumber, this.semester);
            var request = new RestRequest(request_url, Method.GET);
            request.AddHeader("X-Auth-Token", token);
            IRestResponse response = client.Execute(request);
            var content = response.Content; // raw content as string
            content = content.Replace(@"@", "$");
            if (response.StatusCode.ToString() == "OK")
            {
                ReservationList reservationList = JsonConvert.DeserializeObject<ReservationList>(content.ToString(),
                    new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects });
                return reservationList;
            }


            return null; ;
        }

        public ReservationList getTeacherReservationList(int accountID, int pageSize, int pageNumber)
        {
            var client = new RestClient(API_URL);
            String request_url = String.Format("/api/reservation/teacher/{0}/page/{1}/{2}?semester={3}&status=APPROVED", accountID, pageSize, pageNumber,this.semester);
            var request = new RestRequest(request_url, Method.GET);
            request.AddHeader("X-Auth-Token", token);
            IRestResponse response = client.Execute(request);
            var content = response.Content; // raw content as string
            content = content.Replace(@"@", "$");
            if (response.StatusCode.ToString() == "OK")
            {
                ReservationList reservationList = JsonConvert.DeserializeObject<ReservationList>(content.ToString(),
                    new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects });
                return reservationList;
            }


            return null;
        }

    }

    

}
