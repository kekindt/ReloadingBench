using System;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;

namespace EtcdProvider
{
    public class EtcdProvider
    {
        #region constants
        private const string DEFAULT_URL = "http://etcd:2379";
        private const string PREFIX = "/v2/keys/";
        private const string RECUSIVE = "?recursive=true";
        #endregion

        public string ServerUrl { get; set; }

        private HttpClient client;

        public EtcdProvider()
        {
            client = new HttpClient();
            var url = Environment.GetEnvironmentVariable("etcdUrl");
            if (string.IsNullOrEmpty(url))
                ServerUrl = DEFAULT_URL;
            else
                ServerUrl = url;
        }

        public EtcdProvider(string serverUrl) : this()
        {
            ServerUrl = serverUrl;
        }

        public Node GetNode(string key, bool isDir = false)
        {
            string url = $"{ServerUrl}{PREFIX}/{key}";
            if (isDir)
                url += RECUSIVE;

            var o = client.GetAsync(url).Result;
            if ((int)o.StatusCode < 400)
            {

                var response = JsonConvert.DeserializeObject<Response>(o.Content.ReadAsStringAsync().Result);
                if (response.Node != null)
                    return response.Node;
                else
                    return null;
            }
            else
            {
                throw new KeyNotFoundException($"Configuration key: {key} not found");
            }
        }

        public Node UpdateNode(string key, string value)
        {
            string url = $"{ServerUrl}{PREFIX}/{key}";
            var dict = new Dictionary<string, string>();
            dict.Add("value", value);
            var client = new HttpClient();
            var req = new HttpRequestMessage(HttpMethod.Put, url) 
            { 
                Content = new FormUrlEncodedContent(dict) 
            };
            var res = client.SendAsync(req).Result;
            var response = JsonConvert.DeserializeObject<Response>(res.Content.ReadAsStringAsync().Result);
            if (response.Node != null)
                return response.Node;
            else
                return null;
        }

        public Node UpdateNode(Node node)
        {
            return UpdateNode(node.Key, node.Value);
        }


    }
}
