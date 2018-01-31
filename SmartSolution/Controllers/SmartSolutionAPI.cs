using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebPortal.Controllers
{
    public class SmartSolutionAPI
    {
        public static string Get(string baseAddress, string resource, string query)
        {
            string result = "";

            try
            {
                IRestClient API = new RestClient("http://" + baseAddress + "/api/");
                IRestRequest req = new RestRequest("http://" + baseAddress + "/api/" + resource + "/" + query, Method.GET);
                IRestResponse res = API.Get(req);
                result = res.Content;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public static string Post(string baseAddress, string resource,string query, object data)
        {
            string result = "";

            try
            {
                IRestClient API = new RestClient("http://" + baseAddress + "/api/");
                IRestRequest req = new RestRequest("http://" + baseAddress + "/api/" + resource + "/" + query, Method.POST);
                req.AddJsonBody(data);
                IRestResponse res = API.Post(req);
                result = res.Content;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public static string Put(string baseAddress, string resource, string query, object data)
        {
            string result = "";

            try
            {
                IRestClient API = new RestClient("http://" + baseAddress + "/api/");
                IRestRequest req = new RestRequest("http://" + baseAddress + "/api/" + resource + "/" + query, Method.PUT);
                req.AddJsonBody(data);
                IRestResponse res = API.Put(req);
                result = res.Content;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public static string Delete(string baseAddress, string resource, string query, object data)
        {
            string result = "";

            try
            {
                IRestClient API = new RestClient("http://" + baseAddress + "/api/");
                IRestRequest req = new RestRequest("http://" + baseAddress + "/api/" + resource + "/" + query, Method.DELETE);
                req.AddJsonBody(data);
                IRestResponse res = API.Delete(req);
                result = res.Content;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }
    }
}
