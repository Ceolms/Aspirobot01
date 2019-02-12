using System;

public class Effecteur // Classe qui agit sur le manoir , déplace l'aspirateur et nettoye les salles
{
    ManoirEnvironnement manoir;

    public Effecteur(ManoirEnvironnement m)
	{
        manoir = m;
    }

    public void NettoyerPiece()
    {
        manoir.AspiNettoie(manoir.posAspiX, manoir.posAspiY);
    }

    public void RecupererBijoux()
    {
        manoir.AspiNettoie(manoir.posAspiX, manoir.posAspiY);
    }

    public void DeplacerGauche()
    {
        manoir.posAspiX -= 1;
    }
    public void DeplacerDroite()
    {
        manoir.posAspiX += 1;
    }
    public void DeplacerHaut()
    {
        manoir.posAspiY -= 1;
    }
    public void DeplacerBas()
    {
        manoir.posAspiY += 1;
    }
}
