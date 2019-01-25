using System;
using System.Collections.Generic;
using System.Threading;

public class ManoirEnvironnement
{
    public Thread thread;
    List<string> texteConsole = new List<string>();

    public  int NBPIECESLIGNE = 10; // 3x3 salles pour le moment , il suffira de changer

    private const int PROBAPOUSSIERE = 20; // chance sur 100 a chaque boucle
    private const int PROBABIJOUX = 15; // chance sur 100 a chaque boucle

    public Pièce[,] listePièces; 
   
    public int performance;
    public int posAspiX;
    public int posAspiY;


    public ManoirEnvironnement()
	{
        thread = new Thread(new ThreadStart(ThreadLoop));

        listePièces = new Pièce[NBPIECESLIGNE, NBPIECESLIGNE];
        performance = 0;
        posAspiX = 0;
        posAspiY = 0;

        for(int i =0; i < NBPIECESLIGNE; i++)
        {
            for(int j =0; j < NBPIECESLIGNE; j++)
            {
                listePièces[i, j] = new Pièce(i,j);
            }
        }
    }

    public void WriteConsole(string txt)
    {
        texteConsole.Add(txt);
        if(texteConsole.Count > 10)
        {
            texteConsole.RemoveAt(0);
        }
    }

    public void AspiNettoie(int x, int y)
    {
        listePièces[x, y].estSale = false;
        listePièces[x, y].contientBijoux = false;

        // update Score
    }

    public void AspiRecupereBijoux(int x, int y)
    {
        listePièces[x, y].contientBijoux = false;
        // update Score
    }


    private void SalirPiece()
    {
        Random rnd = new Random();

        int salleX = rnd.Next(0, NBPIECESLIGNE);
        int salleY = rnd.Next(0, NBPIECESLIGNE);

        if(rnd.Next(0, 100) < PROBAPOUSSIERE)
        {
            listePièces[salleX, salleY].estSale = true;
        }

        salleX = rnd.Next(0, NBPIECESLIGNE);
        salleY = rnd.Next(0, NBPIECESLIGNE);

        if (rnd.Next(0, 100) < PROBABIJOUX)
        {
            listePièces[salleX, salleY].contientBijoux = true;
        }
    }

    private void DessinerPiece(Pièce piece)
    {
        if(posAspiX == piece.coordsX && posAspiY == piece.coordsY)
        {
            Console.Write("[A]"); // piece contient l'agent Aspirateur
        }
        else if (piece.estSale == false && piece.contientBijoux == false)
        {
            Console.Write("[ ]"); // piece vide
        }
        else if (piece.estSale == true && piece.contientBijoux == false)
        {
            Console.Write("[.]"); // piece est salle
        }
        else Console.Write("[$]"); // piece contient un bijoux
    }

    private void DessinerManoir()
    {
        for (int i = 0; i < NBPIECESLIGNE; i++)
        {
            for (int j = 0; j < NBPIECESLIGNE; j++)
            {
                DessinerPiece(listePièces[i, j]);
            }
            Console.Write("\n");
        }
    }

    // Cette méthode est appelé lors du lancement du thread
    // C'est ici qu'il faudra faire notre travail.
    public void ThreadLoop()
    {
        // Tant que le thread n'est pas tué, on travaille
        while (Thread.CurrentThread.IsAlive)
        {
            Console.Clear();
            SalirPiece();
            DessinerManoir();
            texteConsole.ForEach(txt => Console.WriteLine(txt));
            WriteConsole(posAspiX + ":" + posAspiY);
            Thread.Sleep(1000);
            
        }
    }
}
