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
}
