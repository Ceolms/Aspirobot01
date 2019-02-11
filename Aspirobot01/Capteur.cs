using System;

public class Capteur
{
    ManoirEnvironnement manoir;
	public Capteur(ManoirEnvironnement m)
	{
        manoir = m;
	}

    public  Pièce[,] getEnvironnement() // récupère unne copie de l'environnement actuel
    {
        Pièce[,] manoirOriginal =  manoir.getEnvironnement();
        Pièce[,] manoirCopie = new Pièce[manoir.NBPIECESLIGNE, manoir.NBPIECESLIGNE];

        for (int i = 0; i < manoir.NBPIECESLIGNE; i++)
        {
            for (int j = 0; j < manoir.NBPIECESLIGNE; j++)
            {
                manoirCopie[i, j] = new Pièce(i, j);
                manoirCopie[i, j].contientBijoux = manoirOriginal[i, j].contientBijoux;
                manoirCopie[i, j].estSale = manoirOriginal[i, j].estSale;
                
            }
        }
        return manoirCopie;
    }

    public int getNBLignes()
    {
        return manoir.NBPIECESLIGNE;
    }

    public int getPosX()
    {
        return manoir.posAspiX;
    }

    public int getPosY()
    {
        return manoir.posAspiY;
    }

    public int getPerformance()
    {
        return manoir.getPerformance();
    }
    
}
