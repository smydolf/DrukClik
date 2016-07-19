using System;
using System.Collections.Generic;
using System.Linq;

namespace DrukClik
{
    public static class Mapper
    {
        public static T ToObject<T>(this IDictionary<string, List<string>> source, int i)
           where T : class, new()
        {
            T someObject = new T();
            Type someObjectType = someObject.GetType();
            int j = 0;
            foreach (KeyValuePair<string, List<string>> item in source)
            {
                if (j == 0)
                {
                    DateTime myDate = DateTime.ParseExact(item.Value[i], "dd.MM.yyyy HH:mm:ss",
                        System.Globalization.CultureInfo.InvariantCulture);
                    someObjectType.GetProperty(item.Key).SetValue(someObject, myDate, null);
                    j++;
                }
                else
                {
                    someObjectType.GetProperty(item.Key).SetValue(someObject, item.Value[i], null);
                    j++;
                }
            }
            return someObject;
        }
        public static Dictionary<string, List<string>> Mapp(IDictionary<string, List<string>> _dictionary)
        {
            Dictionary<string, List<string>> dictionary = new Dictionary<string, List<string>>
            {
                {"Q1FilledDateTime", _dictionary.Values.ToArray()[0]},
                {"Q2AgeRange", _dictionary.Values.ToArray()[1]},
                {"Q3Sex", _dictionary.Values.ToArray()[2]},
                {"Q4City", _dictionary.Values.ToArray()[3]},
                {"Q5Profession", _dictionary.Values.ToArray()[4]},
                {"Q6FrequencyOfUse", _dictionary.Values.ToArray()[5]},
                {"Q7PrintedPage", _dictionary.Values.ToArray()[6]},
                {"Q8DataSafety", _dictionary.Values.ToArray()[7]},
                {"Q9PayAttentionFor", _dictionary.Values.ToArray()[8]},
                {"Q10KnowOrNotAnnonymusPrintingHouse", _dictionary.Values.ToArray()[9]},
                {"Q11SpentTime", _dictionary.Values.ToArray()[10]},
                {"Q12KeepingDateInCloud", _dictionary.Values.ToArray()[11]},
                {"Q13PercentKeepingDateInCloud", _dictionary.Values.ToArray()[12]},
                {"Q14SendDataViaEmail", _dictionary.Values.ToArray()[13]},
                {"Q15PercentSendDataViaEmail", _dictionary.Values.ToArray()[14]},
                {"Q16SatisfactionPercentSendDataViaEmail", _dictionary.Values.ToArray()[15]},
                {"Q17SpentMoneyForPrinting", _dictionary.Values.ToArray()[16]},
                {"Q18Email", _dictionary.Values.ToArray()[17]}
            };
            return dictionary;
        }
    }





}
