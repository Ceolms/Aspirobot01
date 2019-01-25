using System;

namespace Aspirobot01
{
    class Programme
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
 
            ManoirEnvironnement manoir = new ManoirEnvironnement();
            AgentAspirateur aspirateur = new AgentAspirateur(manoir);
            manoir.thread.Start();
            aspirateur.thread.Start();
        }
    }
}
