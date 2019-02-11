using System;

public class Effecteur
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

    public  void SeDeplacerRandom() // Pour tester
    {
        Random rnd = new Random();
        int direction = rnd.Next(0, 5);
        //manoir.WriteConsole("Direction:" + direction);

        if (direction == 1 && manoir.posAspiX != 0) DeplacerGauche(); // GAUCHE
        else if (direction == 2 && manoir.posAspiY != 0) DeplacerHaut(); // HAUT
        else if (direction == 3 && manoir.posAspiX != manoir.NBPIECESLIGNE - 1) DeplacerDroite(); // DROITE
        else if (direction == 4 && manoir.posAspiY != manoir.NBPIECESLIGNE - 1) DeplacerBas(); // BAS
        // Si 0 pas bouger !
    }
}
