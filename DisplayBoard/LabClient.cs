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
        private String token;
         
        
        public static bool Login(String username, String password)
        {
            var client = new RestClient("http://ioc.yiliang.me");
            var request = new RestRequest("api/token", Method.POST);
            request.AddHeader("X-Username", username);
            request.AddHeader("X-Password", password);
            client.ExecuteAsync(request, response =>
            {
                foreach (var h in response.Headers)
                {
                    Console.WriteLine(h);
                }
                
            });



            return true;
        }
    }
}
