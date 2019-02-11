using System;

public class Pièce
{
    public int coordsX;
    public int coordsY;
    public bool estSale = false;
    public bool contientBijoux = false;

    public Pièce(int x , int y)
	{
        coordsX = x;
        coordsY = y;
	}

    public Pièce(int x , int y , bool estSale,bool contientBijoux)
    {
        coordsX = x;
        coordsY = y;
        this.estSale = estSale;
        this.contientBijoux = contientBijoux;
    }
}
