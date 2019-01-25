using System;
using System.Threading;

public class AgentAspirateur 
{
    public Thread thread;
    public ManoirEnvironnement manoir;

    public AgentAspirateur(ManoirEnvironnement env)
	{
        thread = new Thread(new ThreadStart(ThreadLoop));
        manoir = env;
    }

    private void NettoyerPiece(int x , int y)
    {
        manoir.AspiNettoie(x, y);
    }

    private void RecupererBijoux(int x , int y)
    {
        manoir.AspiNettoie(x, y);
    }

    private void DeplacerGauche()
    {
        manoir.posAspiX -= 1;
    }
    private void DeplacerDroite()
    {
        manoir.posAspiX += 1;
    }
    private void DeplacerHaut()
    {
        manoir.posAspiY -= 1;
    }
    private void DeplacerBas()
    {
        manoir.posAspiY += 1;
    }

    private void SeDeplacerRandom() // Pour tester
    {
        Random rnd = new Random();
        int direction = rnd.Next(0, 5);
        //manoir.WriteConsole("Direction:" + direction);

        if (direction == 1 && manoir.posAspiX != 0) DeplacerGauche(); // GAUCHE
        else if (direction == 2 && manoir.posAspiY != 0) DeplacerHaut(); // HAUT
        else if(direction == 3 && manoir.posAspiX != manoir.NBPIECESLIGNE-1) DeplacerDroite(); // DROITE
        else if(direction == 4 && manoir.posAspiY != manoir.NBPIECESLIGNE - 1) DeplacerBas(); // BAS
        // Si 0 pas bouger !
    }

    public void ThreadLoop()
    {
        // Tant que le thread n'est pas tué, on travaille
        while (Thread.CurrentThread.IsAlive)
        {
            // Attente de 500 ms
            Thread.Sleep(1000);

            // Affichage dans la console
            SeDeplacerRandom();
        }
    }
}
