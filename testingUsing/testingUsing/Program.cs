using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace testingUsing
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var p = new Person();
            string name;
            int age;
            try
            {
                
                    name = p.Name;
                    age = p.Age;
                    Console.WriteLine(p);
                    Console.WriteLine(name);
                    Console.WriteLine(age);
               
            }
            finally { 
            Console.WriteLine(p);
            p.Dispose();
                GC.SuppressFinalize(p);

                Console.WriteLine(p);
            }

            
        }
    }
}
