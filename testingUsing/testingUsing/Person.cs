using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testingUsing
{
    internal class Person:IDisposable
    {
        public string Name { get; set; } = "Nafis";
        public int Age { get; set; } = 24;

        public void Dispose()
        {
            this.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
