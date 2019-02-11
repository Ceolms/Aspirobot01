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
}
