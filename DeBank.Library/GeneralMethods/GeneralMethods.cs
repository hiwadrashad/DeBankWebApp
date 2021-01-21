using DeBank.Library.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;

namespace DeBank.Library.GeneralMethods
{

    public static class Cloning
    {
        public static T DeepClone<T>(this T obj)
        {
            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, obj);
                ms.Position = 0;

                return (T)formatter.Deserialize(ms);
            }
        }
    }

    public static class TempDataExtensions
    {
        public static void Put<T>(this ITempDataDictionary tempData, string key, T value) where T : class
        {
            tempData[key] = JsonConvert.SerializeObject(value);
        }

        public static T Get<T>(this ITempDataDictionary tempData, string key) where T : class
        {
            object o;
            tempData.TryGetValue(key, out o);
            return o == null ? null : JsonConvert.DeserializeObject<T>((string)o);
        }
    }
    public class GeneralMethods
    {
  
        public static IEnumerable<T> CreateEnumerable<T>(params T[] items)
        {
            if (items == null)
                yield break;

            foreach (T mitem in items)
                yield return mitem;
        }

        public static void ShowGeneralErrorMessage()
        {
            //<summary>
            // write messagebox dependant on front end platform
            //<summary>
        }

        public static void ShowIncorrectValueErrorMessage()
        {
            //<summary>
            // write messagebox dependant on front end platform
            //<summary>
        }

        public static void ShowMinimumOneBankAccountNeededMessage()
        {
            //<summary>
            // write messagebox dependant on front end platform
            //<summary>
        }

        public static void ShowUserNotFoundMessage()
        {
            //<summary>
            // write messagebox dependant on front end platform
            //<summary>
        }


    }
}
