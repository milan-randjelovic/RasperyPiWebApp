using RestSharp;
using System;

namespace SmartSolutionAPILib
{
    public class SmartSolutionAPI
    {
        public static IRestResponse Get(string baseAddress, string resource, string query)
        {
            try
            {
                IRestClient API = new RestClient("http://" + baseAddress + "/api/");
                IRestRequest req = new RestRequest("http://" + baseAddress + "/api/" + resource + "/" + query, Method.GET);
                IRestResponse res = API.Get(req);
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static IRestResponse Post(string baseAddress, string resource,string query, object data)
        {
            try
            {
                IRestClient API = new RestClient("http://" + baseAddress + "/api/");
                IRestRequest req = new RestRequest("http://" + baseAddress + "/api/" + resource + "/" + query, Method.POST);
                req.AddJsonBody(data);
                IRestResponse res = API.Post(req);
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static IRestResponse Put(string baseAddress, string resource, string query, object data)
        {
            try
            {
                IRestClient API = new RestClient("http://" + baseAddress + "/api/");
                IRestRequest req = new RestRequest("http://" + baseAddress + "/api/" + resource + "/" + query, Method.PUT);
                req.AddJsonBody(data);
                IRestResponse res = API.Put(req);
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static IRestResponse Delete(string baseAddress, string resource, string query, object data)
        {
            try
            {
                IRestClient API = new RestClient("http://" + baseAddress + "/api/");
                IRestRequest req = new RestRequest("http://" + baseAddress + "/api/" + resource + "/" + query, Method.DELETE);
                req.AddJsonBody(data);
                IRestResponse res = API.Delete(req);
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
