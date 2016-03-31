using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foo.Crm.BusinessLogic.Common
{
    public class HandlerCache
    {
        private Dictionary<string, object> LocalDictinary { get; set; }
        public HandlerCache()
        {
            this.LocalDictinary = new Dictionary<string, object>();
        }

        public void AddToCache(string key, object value)
        {
            if (this.LocalDictinary.ContainsKey(key))
                this.LocalDictinary[key] = value;
            else
                this.LocalDictinary.Add(key, value);
        }

        public T GetFromCache<T>(string key)
        {
            object obj = this.LocalDictinary[key];
            if (obj is T)
                return (T)obj;

            return default(T);
        }

        public bool Contains(string key)
        {
            return this.LocalDictinary.ContainsKey(key);
        }

    }
}
