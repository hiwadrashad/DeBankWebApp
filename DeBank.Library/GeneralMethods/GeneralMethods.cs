using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

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
    public static class GeneralMethods
    {
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
    }
}
