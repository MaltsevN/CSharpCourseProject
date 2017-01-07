using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace OnlineShop.Client.Common
{
    public static class ObjectCopier
    {
        public static T Clone<T>(T source)
        {
            if (Object.ReferenceEquals(source, null))
            {
                return default(T);
            }

            var serializer = new JavaScriptSerializer();

            string json = serializer.Serialize(source);
            return serializer.Deserialize<T>(json);
        }
    }
}
