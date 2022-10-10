using System.Collections.Generic;

namespace Examples.OCP
{
    public class CourseCalculator
    {
        public static IEnumerable<object> GetAll()
        {
            var list = new List<object>();
            list.Add(new Python() { CourseName = "Basics of Python", BasicCost = 1000, Tax = 20 });
            list.Add(new AdvanceDotNet() { CourseName = "Advance .Net", BasicCost = 2000, Tax = 40 });
            return list;
        }

        public static double TotalCost(params object[] arrObjects)
        {
            double cost = 0;
            foreach (var obj in arrObjects)
            {
                if (obj is Python python)
                {
                    cost += (python.BasicCost * python.Tax) + 100;
                }
                else if (obj is AdvanceDotNet advanceDotNet)
                {
                    cost += (advanceDotNet.BasicCost * advanceDotNet.Tax) + 50;
                }
            }

            return cost;
        }

        public static IEnumerable<string> GetModules(object obj)
        {
            var modules = new List<string>();
           
            if (obj is Python)
            {
                modules.Add("Basic Fundamentals");
                modules.Add("DataType in Python");
                modules.Add("Loops for, while etc in Python");
            }
            else if (obj is AdvanceDotNet)
            {
                modules.Add("Garbage Collector");
                modules.Add("Memory Management");
                modules.Add("Multi Threads");
            }           

            return modules;
        }
    }
}
