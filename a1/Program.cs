using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.IO;

namespace a1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Please enter the Path");
            string path = Console.ReadLine();

            // you can return with the value pair list or list of class you can have
            // both options
            var mylist = new List<KeyValuePair<string, Decimal>>();

            MyParser parserr = new MyParser();
            mylist = parserr.ReadTextFile(path);

            foreach (var item in mylist)
            {
                Console.WriteLine("{0} :  {1}", item.Key, item.Value);
            }

            Console.ReadLine();
        }

    }

    public class MyParser
    {
        // no need for this line
        Regex rx = new Regex(@"(?<type>(start|stop))(?:,(?<key>[A-Z]+))?(?:,(?<value>\d+))", RegexOptions.IgnoreCase);

        public List<KeyValuePair<string, Decimal>> ReadTextFile(string fileName)
        {
            List<KeyValuePair<string, Decimal>> L = new List<KeyValuePair<string, decimal>>();

            StreamReader thisreader = new StreamReader(fileName);

            Stack<string> st = new Stack<string>();

            while(!thisreader.EndOfStream)
            {
                string sL = thisreader.ReadLine();

                if(sL.StartsWith("stop"))
                {
                    //if line starts with 'stop' then format of sL would be
                    //sL = stop,<timestamp>
                    
                    string[] array2index, array3index;

                    string sE = st.Pop();
                    // hence format of sE also is
                    // start,<function-name>,<timestamp>
                    
                    array2index = sL.Split(','); // array2index : { "stop" , "timestamp/stop time of function" } 
                    array3index = sE.Split(','); // array3index : { "start" , "functionName" , "timestamp/start time of function" }
                    
                    // stop time of function
                        // array2index : { "stop" , "timestamp/stop time of function" }
                    string stopTimeString = array2index[1];
                    decimal stopTime = Convert.ToDecimal(stopTimeString);
                    
                    // start time of function
                        // array3index : { "start" , "functionName" , "timestamp/start time of function" }
                    string startTimeString = array3index[2];
                    decimal startTime = Convert.ToDecimal(startTimeString);
                    
                    // total running/elapsed time of function
                    decimal elapsedTime = stopTime - startTime;
                    
                    // array3index : { "start" , "functionName" , "timestamp/start time of function" }
                    string funcName = array3index[1];
                    
                    KeyValuePair<string, decimal> answer = new KeyValuePair<string, decimal>(funcName, elapsedTime);
                       
                    // adding key value pair to list
                    L.Add(answer);
                }
                else
                {
                    // else if line doesnt start with 'stop' then format of sL would be
                    //sL = start,<function-name>,<timestamp>
                    st.Push(sL);
                }
            }
            
            //returning the list
            return L;
        }
    }
}
