using System;
using System.Collections.Generic;

public class ArbreExploration
{
    Noeud racine;
    GestionConsole gc;

    public Noeud Explorer(Noeud racine,int limite,GestionConsole gc)
    {
        this.racine = racine;
        this.gc = gc;
        return  RecursiveDLS(racine, racine, limite);
    }

    public Noeud ExplorerGreedy(Noeud racine, GestionConsole gc)
    {
        this.racine = racine;
        this.gc = gc;
        return GreedySearch(racine);
    }

    public Noeud RecursiveDLS(Noeud noeud, Noeud topNoeud,int limite)
    {
        if (noeud.performance > topNoeud.performance) topNoeud = noeud;
        string s = "[";
        foreach(string act in noeud.listeActions) s += act + ",";
        gc.AddConsole(" DLS: noeud perf:"+noeud.performance+", top perf : " + topNoeud.performance + ", profondeur : " + noeud.profondeur +s+"] "+noeud.posAspiX+":"+noeud.posAspiY);
        if (noeud.profondeur == limite) return topNoeud;
        else
        {
            List<Noeud> listeSuccesseurs = noeud.EtendreNoeud();
            for(int i = 0; i<listeSuccesseurs.Count;i++)
            {
                if (listeSuccesseurs[i] != null)
                {
                    Noeud n = RecursiveDLS(listeSuccesseurs[i], topNoeud, limite);
                    if (topNoeud.performance < n.performance)
                    {
                        topNoeud = n;
                    }
                }
            }
        }
        return topNoeud;
    }

    public Noeud GreedySearch(Noeud noeud)
    {
        string s = "[";
        foreach (string act in noeud.listeActions) s += act + ",";
        gc.AddConsole(" Greedy: noeud perf:" + noeud.performance + ", profondeur : " + noeud.profondeur + s + "] " + noeud.posAspiX + ":" + noeud.posAspiY);
        List<Noeud> listeSuccesseurs = noeud.EtendreNoeud();
        Noeud topNoeud = noeud;
        int topHeuristique = 200;
        for(int i = 0; i< listeSuccesseurs.Count;i++)
        {
            if(listeSuccesseurs[i] != null)
            {
                int heur = Heuristique(listeSuccesseurs[i]);
                if (heur < topHeuristique)
                {
                    topNoeud = listeSuccesseurs[i];
                    topHeuristique = heur;
                }
            }  
        }
        if (topNoeud.action  == "aspirer" || topNoeud.action == "ramasser") return topNoeud;
        else return GreedySearch(topNoeud);
    }

    public int Heuristique(Noeud n)
    {
        Pièce[,] listePièces = n.listePièces;
        int posX = n.posAspiX;
        int posY = n.posAspiY;
        if (n.action == "ramasser") return -2000;
        else if (n.action == "aspirer") return -1500;
        else
        {
            int topDistance = 200;
            for(int i = 0; i < n.NBPIECESLIGNE; i++) // on recuperer la distance entre l'agent et la case sale la plus proche
            {
                for (int j = 0; j < n.NBPIECESLIGNE; j++)
                {
                    if(listePièces[i,j].estSale)
                    {
                        int distance = DistanceEuclidienne(posX, posY, i, j) -2;
                        if (distance < topDistance) topDistance = distance;
                    }
                    if (listePièces[i, j].contientBijoux)
                    {
                        int distance = DistanceEuclidienne(posX, posY, i, j) -5; 
                        // Il est plus intérréssant de recuperer un bijoux que de nettoyer la poussiere , donc on ajoute un bonus à l'heuristique
                        if (distance < topDistance) topDistance = distance;
                    }
                }
            }
            return topDistance;
        }  
    }

    public int DistanceEuclidienne(int x1 , int y1 , int x2 , int y2)
    {
        return (int)Math.Sqrt((x2-x1)*(x2-x1) + (y2-y1)*(y2-y1));
    }
}
