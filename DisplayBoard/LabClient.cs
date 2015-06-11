<<<<<<< HEAD
﻿using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
=======
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.ServiceModel;
>>>>>>> 64696cb5577511c9c3c2666285c20fe9a953ed72
using System.Text;
using System.Threading.Tasks;

namespace DisplayBoard
{
    class LabClient
    {
        private String token;
<<<<<<< HEAD
         
        
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
=======
     //   var client = new RestClient("http://ioc.yiliang.me/");
        
        public bool Login(String username, String password)
        {

>>>>>>> 64696cb5577511c9c3c2666285c20fe9a953ed72



            return true;
        }
    }
}
