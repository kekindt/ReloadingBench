/*

{
    "action": "get",
    "node": {
        "dir": true,
        "nodes": [
            {
                "key": "/Some",
                "dir": true,
                "nodes": [
                    {
                        "key": "/Some/key",
                        "value": "some value",
                        "modifiedIndex": 12,
                        "createdIndex": 12
                    }
                ],
                "modifiedIndex": 12,
                "createdIndex": 12
            }
        ]
    }
}


 */
using System.Collections.Generic;
using Newtonsoft.Json;

namespace EtcdProvider
{
    public class Node
    {
        private readonly char[] SLASH = new char[] {'/'} ;
        string key = string.Empty, directory = string.Empty;

        [JsonProperty(PropertyName = "key")]
        public string Key
        {
            get
            {
                return key;
            }
            set
            {
                key = value;
                var trees = value.Split(SLASH);
                for(int i = 0; i < trees.Length -1; i++)
                {
                    directory += trees[i] + SLASH;
                }
            }
        }

        [JsonProperty(PropertyName = "value")]
        public string Value { get; set; }

        [JsonProperty(PropertyName = "createdIndex")]
        public int ModifiedIndex { get; set; }

        [JsonProperty(PropertyName = "createdIndex")]
        public int CreatedIndex { get; set; }

        [JsonProperty(PropertyName = "dir")]
        public bool IsDirectory { get; set; }

        [JsonIgnore]
        public string Directory
        {
            get
            {
                return directory;
            }
        }

        [JsonProperty(PropertyName = "nodes")]
        public List<Node> Nodes { get; set; }
    }
}
