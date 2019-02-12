using System;
using System.Collections.Generic;

public class Noeud // Noeud de l'arbre d'exploration
{
    public Pièce[,] listePièces; //l'etat du manoir
    public int performance; // Le score jusqu'ici
    public int profondeur = 0;
    public int posAspiX; 
    public int posAspiY;
    public string action;
    public int NBPIECESLIGNE;
    public List<string> listeActions = new List<string>(); // Liste des actions amenant à ce noeud

    public Noeud(Pièce[,] listePièces, int performance, int profondeur, int posAspiX, int posAspiY, string action,List<string>listeActions, int nbPiecesLigne)
    {
        this.listePièces = listePièces;
        this.performance = performance;
        this.profondeur = profondeur;
        this.posAspiX = posAspiX;
        this.posAspiY = posAspiY;
        this.action = action;
        this.listeActions = new List<string>(listeActions);
        this.listeActions.Add(action);

        this.NBPIECESLIGNE = nbPiecesLigne;
    }

    public List<Noeud> EtendreNoeud() // Creer les successeurs de ce noeud
    {   // on évite de répeter des actions (ramasser -> ramasser) et inutiles ( gauche -> droite)
        List<Noeud> listeSuccesseurs = new List<Noeud>();
        if (this.action!= "droite") listeSuccesseurs.Add(CreerNoeudGauche());
        if (this.action != "gauche") listeSuccesseurs.Add(CreerNoeudDroite());
        if (this.action != "bas") listeSuccesseurs.Add(CreerNoeudHaut());
        if (this.action != "haut") listeSuccesseurs.Add(CreerNoeudBas());
        if (this.action != "aspirer") listeSuccesseurs.Add(CreerNoeudAspirer());
        if (this.action != "ramasser" || this.action != "aspirer") listeSuccesseurs.Add(CreerNoeudRamasser());

        return listeSuccesseurs;
    }

    public Pièce[,] CreerPiecesSuccesseur(int posX,int posY,string action) // etat du manoir selon la prochaine action de l'agent
    {
        Pièce[,]  listePiècesSuccesseurs = new Pièce[NBPIECESLIGNE, NBPIECESLIGNE];

        if (action == "aspirer")
        {
            for (int i = 0; i < NBPIECESLIGNE; i++)
            {
                for (int j = 0; j < NBPIECESLIGNE; j++)
                {
                    listePiècesSuccesseurs[i, j] = new Pièce(i, j);
                    if (i == posX && j == posY)
                    {
                        listePiècesSuccesseurs[i, j].estSale = false;
                        listePiècesSuccesseurs[i, j].contientBijoux = false;

                    }
                }
            }
        }
        else if (action == "ramasser")
        {
            for (int i = 0; i < NBPIECESLIGNE; i++)
            {
                for (int j = 0; j < NBPIECESLIGNE; j++)
                {
                    listePiècesSuccesseurs[i, j] = new Pièce(i, j);
                    if (i == posX && j == posY)
                    {
                        listePiècesSuccesseurs[i, j].contientBijoux = false;
                    }
                    listePiècesSuccesseurs[i, j].estSale = listePièces[i, j].estSale;
                }
            }
        }
        else
        {
            for (int i = 0; i < NBPIECESLIGNE; i++)
            {
                for (int j = 0; j < NBPIECESLIGNE; j++)
                {
                    listePiècesSuccesseurs[i, j] = new Pièce(i, j);
                    listePiècesSuccesseurs[i, j].contientBijoux = listePièces[i, j].contientBijoux;
                    listePiècesSuccesseurs[i, j].estSale = listePièces[i, j].estSale;
                }
            }
        }
        return listePiècesSuccesseurs;
    }

    public Noeud CreerNoeudGauche()
    {
        if (posAspiX != 0)
        {
            Noeud n = new Noeud(CreerPiecesSuccesseur(posAspiX-1,posAspiY,"gauche"),performance-1,profondeur+1,posAspiX-1,posAspiY, "gauche", listeActions,NBPIECESLIGNE);
            return n;
        }
        return null;
    }
    public Noeud CreerNoeudDroite()
    {
        if (posAspiX != NBPIECESLIGNE-1)
        {
            Noeud n = new Noeud(CreerPiecesSuccesseur(posAspiX+1, posAspiY, "droite"),performance-1,profondeur+1,posAspiX+1,posAspiY,"droite", listeActions, NBPIECESLIGNE);
            return n;
        }
        return null;
    }
    public Noeud CreerNoeudHaut()
    {
        if (posAspiY != 0)
        {
            Noeud n = new Noeud(CreerPiecesSuccesseur(posAspiX, posAspiY-1, "haut"),performance-1,profondeur+1,posAspiX,posAspiY-1,"haut",listeActions,NBPIECESLIGNE);
            return n;
        }
        return null;
    }
    public Noeud CreerNoeudBas()
    {
        if (posAspiY != NBPIECESLIGNE -1)
        {
            Noeud n = new Noeud(CreerPiecesSuccesseur(posAspiX, posAspiY+1,"bas"),performance-1,profondeur+1,posAspiX,posAspiY+1,"bas", listeActions,NBPIECESLIGNE);
            return n;
        }
        return null;
    }
    public Noeud CreerNoeudAspirer()
    {

        int newPerf = performance - 1;
        if (listePièces[posAspiX, posAspiY].contientBijoux)
        {
            newPerf -= 25;
        }
        else if (listePièces[posAspiX, posAspiY].estSale)
        {
            newPerf += 15;
        }
        else return null; // on ne créer pas ne noeud aspirer si cela n'a pas d'utilité

        Noeud n = new Noeud(CreerPiecesSuccesseur(posAspiX, posAspiY,"aspirer"),newPerf,profondeur+1,posAspiX,posAspiY,"aspirer", listeActions,NBPIECESLIGNE);
        return n;
    }
    public Noeud CreerNoeudRamasser()
    {
        int newPerf = performance - 1;
        if (listePièces[posAspiX, posAspiY].contientBijoux) newPerf += 20;
        else return null; // on ne créer pas ne noeud ramasser si cela n'a pas d'utilité

        Noeud n = new Noeud(CreerPiecesSuccesseur(posAspiX, posAspiY,"ramasser"),newPerf,profondeur+1,posAspiX,posAspiY,"ramasser", listeActions,NBPIECESLIGNE);
        return n;
    }
}
