using SimuladorCooperativaVerde.ServiceBusRole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsolaPruebas
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
