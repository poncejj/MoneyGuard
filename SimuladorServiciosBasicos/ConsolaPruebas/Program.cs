using ServiceBusRole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimuladorServiciosBasicos.ConsolaPruebas
{
    class Program
    {
        static void Main(string[] args)
        {
            WorkerRole objWorkerRole = new WorkerRole();
            objWorkerRole.OnStart();
            Console.ReadLine();
        }
    }
}
