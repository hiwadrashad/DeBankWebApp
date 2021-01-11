using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DeBank.Library.CSVToIEnumerable
{
    public class NamesConverter
    {
        public static IEnumerable<object[]> TestNamesConverter
        {
            get
            {
                string[] csvLines = File.ReadAllLines("Names.csv");
                var testCases = new List<object[]>();

                foreach (var csvLine in csvLines)
                {
                    IEnumerable<int> values = csvLine.Split(',').Select(int.Parse);
                    List<string> valuesstring = new List<string>();
                    foreach (var item in values)
                    {
                        valuesstring.Add(item.ToString());
                    }

                    object[] testCase = valuesstring.Cast<object>().ToArray();

                    testCases.Add(testCase);
                }
                return testCases;
            }
        }
    }
}
