using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TestAPI
{
    public class DataStore
    {
        JArray _data;

        public DataStore()
        {
            _data = new JArray();
        }

        public JArray Data
        {
            get
            {
                return _data;
            }
        }

        public void Add(JObject to_add)
        {
            _data.Add(to_add);
        }

        public void Remove(JObject to_remove)
        {
            _data.Remove(to_remove);
        }
        
    }
}