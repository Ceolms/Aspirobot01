using System;

namespace Aspirobot01
{
    class Programme
    {
        static void Main(string[] args)
        {
            Console.SetWindowSize(120, 40);

            GestionConsole gc = new GestionConsole();
            ManoirEnvironnement manoir = new ManoirEnvironnement(gc);
            AgentAspirateur aspirateur = new AgentAspirateur(manoir,gc);

            manoir.thread.Start();
            aspirateur.thread.Start();
        }
    }
}
