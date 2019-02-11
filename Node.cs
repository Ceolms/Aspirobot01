using System;

public class Noeud
{
    private Pièce[,] listePièces;
    private int performance;
    public int posAspiX;
    public int posAspiY;
    public string action;

    // Successeurs

    public Noeud noeudActionGauche;
    public Noeud noeudActionDroite;
    public Noeud noeudActionHaut;
    public Noeud noeudActionBas;
    public Noeud noeudActionAspirer;
    public Noeud noeudActionRamasser;

    public Noeud(Pièce[,] listePièces,int performance,int posAspiX,int posAspiY,string action)
	{
        this.listePièces = listePièces;
        this.performance = performance;
        this.posAspiX = posAspiX;
        this.posAspiY = posAspiY;
        this.action = action;
	}

    public void EtendreNoeud()
    {
        noeudActionGauche = creerNoeudGauche();
        noeudActionDroite = creerNoeudDroite();
        noeudActionHaut = creerNoeudHaut();
        noeudActionBas = creerNoeudBas();
        noeudActionAspirer = creerNoeudAspirer();
        noeudActionRamasser = creerNoeudRamasser();
    }

    public Noeud creerNoeudGauche()
    {
        int test = "false";
    }
    public Noeud creerNoeudDroite()
    {

    }
    public Noeud creerNoeudHaut()
    {

    }
    public Noeud creerNoeudBas()
    {

    }
    public Noeud creerNoeudAspirer()
    {

    }
    public Noeud creerNoeudRamasser()
    {

    }
}
