using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json.Linq;
using QuerySystem.Model;
using Newtonsoft.Json;
using QuerySystem.Exceptions;
<<<<<<< HEAD
using System.Windows;
=======
>>>>>>> 2ddb89af065ba1ee0ee9740abed07574e6ff1249

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
            IRestResponse response = client.Execute(request);
<<<<<<< HEAD
            try
            {
                token = response.Headers.ToList().Find(x => x.Name == "X-Auth-Token").Value.ToString();
                getSemester();
            }
            catch(Exception)
            {
                MessageBox.Show("登陆失败");
            }
                         
=======
            token = response.Headers.ToList().Find(x => x.Name == "X-Auth-Token").Value.ToString();
            getSemester();
               
            //Console.WriteLine("login failed");
               
>>>>>>> 2ddb89af065ba1ee0ee9740abed07574e6ff1249
           

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

        public static ReservationList getReservation(String accountNumber, int pageSize, int pageNumber)
        {
            Account account = null;
            int accountID = 0;
            try
            {
                account = getAccount(accountNumber);
                
                if(account==null)
                {
                    throw new AccountNotFoundException();
                }
                accountID = account.id;
            }
            catch(Exception ex)
            {
                throw ex;
            }
           
            if (account!=null)
            {
                try
                {
                    if (account.role.Equals("NOR_TEACHER"))
                    {
                        return getTeacherReservationList(accountID, account.name, pageSize, pageNumber);
                    }
                    else if (account.role.Equals("ALL_TEACHER"))
                    {
                        return getLabTeacherReservationList(accountID, account.name, pageSize, pageNumber);
                    }
                    else if (account.role.Equals("STUDENT"))
                    {
                        return getClazzReservationList(accountID, account.name, pageSize, pageNumber);
                    }
                    else
                    {
                        return null;
                    }
                }
                catch(Exception ex)
                {
                    throw ex;
                }
               

            }
            else
            {
                return null;
            }
           

        }

        private static Account getAccount(String accountID)
        {
            var client = new RestClient(API_URL);
            String request_url = String.Format("api/account?number={0}", accountID);
            var request = new RestRequest(request_url, Method.GET);
            request.AddHeader("X-Auth-Token", token);
            IRestResponse response = null;
            response = client.Execute(request);

            
            var content = response.Content; // raw content as string
            Account account = null;
            if (response.StatusCode.ToString() == "OK")
            {
                JObject accountJObject = JObject.Parse(content);
                
                if(accountJObject["data"].HasValues==false)
                {
                    throw new AccountNotFoundException();
                }
                JObject accountData = (JObject)accountJObject["data"];                

                account = JsonConvert.DeserializeObject<Account>(accountData.ToString(),
                new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects });
                return account;
            }
            else
            {
                throw new ServerNotResponseException();
            }
        }

        private static ReservationList getStudentReservationList(int accountID, String name, int pageSize, int pageNumber)
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
                reservationList.accountName = name;
                return reservationList;
            }
            else
            {
                throw new ServerNotResponseException();
            }
        }

        private static ReservationList getClazzReservationList(int accountID,String name, int pageSize, int pageNumber)
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
                reservationList.accountName = name;
                return reservationList;
            }
            else
            {
                throw new ServerNotResponseException();
            }



        }

        private static ReservationList getTeacherReservationList(int accountID,String name, int pageSize, int pageNumber)
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
                reservationList.accountName = name;
                return reservationList;
            }
            else
            {
                throw new ServerNotResponseException();
            }

        }

        private static ReservationList getLabTeacherReservationList(int accountID,String name, int pageSize, int pageNumber)
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
                reservationList.accountName = name;
                return reservationList;
            }
            else
            {
                throw new ServerNotResponseException();
            }


        }

    }

    

}
