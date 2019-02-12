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
            Console.WriteLine("Recherche non-informée(1) ou recherche informée(2)?");
            string reponse = Console.ReadLine();
            if(reponse != "1" || reponse != "2")
            {
                Console.WriteLine("Veuillez entrer 1 ou 2");
                reponse = Console.ReadLine();
            }
            manoir.thread.Start();
            aspirateur.thread.Start();
        }
    }
}
