using System;
using System.Collections.Generic;
using System.Threading;

public class ManoirEnvironnement
{
    public Thread thread;
    
    public int NBPIECESLIGNE = 10; // 3x3 salles pour le moment , il suffira de changer

    private const int PROBAPOUSSIERE = 20; // chance sur 100 a chaque boucle
    private const int PROBABIJOUX = 15; // chance sur 100 a chaque boucle

    private const int BONUSBIJOUX = 20;
    private const int BONUSPOUSSIERE = 12;
    private const int MALUSBIJOUX = 25;


    private Pièce[,] listePièces;
    private GestionConsole gc;
   
    private int performance;
    public int posAspiX;
    public int posAspiY;


    public ManoirEnvironnement(GestionConsole gc)
	{
        thread = new Thread(new ThreadStart(ThreadLoop));
        this.gc = gc;

        listePièces = new Pièce[NBPIECESLIGNE, NBPIECESLIGNE];
        performance = 100;
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

    public void AspiNettoie(int x, int y)
    {
        if (listePièces[x, y].contientBijoux) performance -= MALUSBIJOUX; // bijoux aspirées
        else if (listePièces[x, y].estSale) performance += BONUSPOUSSIERE; // poussiere nettoyé , pas de bijoux aspirées
        performance--; // 1 action = 1 electricite

        listePièces[x, y].estSale = false;
        listePièces[x, y].contientBijoux = false;

    }

    public void AspiRecupereBijoux(int x, int y)
    {
        if (listePièces[x, y].contientBijoux) performance += BONUSBIJOUX;
        performance--; // 1 action = 1 electricite
        listePièces[x, y].contientBijoux = false;
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

    private void DessinerPiece(Pièce piece) // Dessin en ASCII du manoir
    {
        if(posAspiX == piece.coordsX && posAspiY == piece.coordsY)
        {
            Console.Write("[A]"); // piece contient l'agent Aspirateur
        }
        else if (piece.estSale == true && piece.contientBijoux == true)
        {
            Console.Write("[*]"); // piece vide
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
                DessinerPiece(listePièces[j, i]);
            }
            Console.Write("\n");
        }
    }

    public Pièce[,] getEnvironnement()
    {
        return listePièces;
    }

    public int getPerformance()
    {
        int perf =  performance;
        performance = 100;
        return perf;
    }

    public void ThreadLoop()
    {
        listePièces[1, 0].estSale = true;
        listePièces[0, 1].contientBijoux = true;
        while (Thread.CurrentThread.IsAlive)
        {
            Console.Clear();
            SalirPiece();
            DessinerManoir();
           
            //gc.AddConsole(posAspiX + ":" + posAspiY);
            gc.Write();
            Thread.Sleep(1000);
        }
    }
}
