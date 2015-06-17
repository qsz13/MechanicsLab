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
        private static String token = null;
        private static int semester;
        private static String API_URL = "http://202.120.188.5";

        public static bool Login(String username, String password)
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

        private static bool getSemester()
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
                }

            });
            return true;
        }

        public static ReservationList getReservation(int accountID, int pageSize, int pageNumber)
        {
            Account account = getAccount(accountID);
            if (account.role.Equals("NOR_TEACHER"))
            {
                return getTeacherReservationList(accountID, pageSize, pageNumber);
            }
            else if (account.role.Equals("ALL_TEACHER"))
            {
                return getLabTeacherReservationList(accountID, pageSize, pageNumber);
            }
            else if (account.role.Equals("STUDENT"))
            {
                return getClazzReservationList(accountID, pageSize, pageNumber);
            }
            else
            {
                return null;
            }

        }

        private static Account getAccount(int accountID)
        {
            var client = new RestClient(API_URL);
            String request_url = String.Format("api/account?number={0}", accountID);
            var request = new RestRequest(request_url, Method.GET);
            request.AddHeader("X-Auth-Token", token);
            IRestResponse response = client.Execute(request);
            var content = response.Content; // raw content as string
            Account account = null;
            if (response.StatusCode.ToString() == "OK")
            {
                JObject accountJObject = JObject.Parse(content);

                JArray accountData = (JArray)accountJObject["data"];

                account = JsonConvert.DeserializeObject<Account>(accountData.ToString(),
                new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects });
                return account;
            }

            return account;
        }

        private static ReservationList getStudentReservationList(int accountID, int pageSize, int pageNumber)
        {
            var client = new RestClient(API_URL);
            String request_url = String.Format("/api/reservation/student/{0}/page/{1}/{2}?semester={3}&type=student", accountID, pageSize, pageNumber, semester);
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

        private static ReservationList getClazzReservationList(int accountID, int pageSize, int pageNumber)
        {
            var client = new RestClient(API_URL);
            String request_url = String.Format("/api/reservation/student/{0}/page/{1}/{2}?semester={3}&type=clazz", accountID, pageSize, pageNumber, semester);
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

        private static ReservationList getTeacherReservationList(int accountID, int pageSize, int pageNumber)
        {
            var client = new RestClient(API_URL);
            String request_url = String.Format("/api/reservation/teacher/{0}/page/{1}/{2}?semester={3}&status=APPROVED", accountID, pageSize, pageNumber, semester);
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

        private static ReservationList getLabTeacherReservationList(int accountID, int pageSize, int pageNumber)
        {
            var client = new RestClient(API_URL);
            String request_url = String.Format("/api/reservation/labteacher/{0}/page/{1}/{2}?semester={3}&status=APPROVED", accountID, pageSize, pageNumber, semester);
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
