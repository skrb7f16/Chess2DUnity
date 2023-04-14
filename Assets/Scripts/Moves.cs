
public class Moves
{
    public static byte COLOR_BLACK = 0;
    public static byte COLOR_WHITE = 1;

    public static int[] whitePawn = { 1, 0 };
    public static int[] blackPawn = { -1, 0 };

    public static int[,] KingMovement = { {1,0 },{ 0,1},{ -1,0},{ 0,-1},{ 1,1},{ -1,-1},{ 1,-1},{ -1,1} };

    public static int[] GetPawnMove(int r,int c,int color)
    {
        if (color == COLOR_BLACK)
        {
            return new int[] { r+blackPawn[0],c+blackPawn[1]};
        }
        else
        {
            return new int[] { r + whitePawn[0], c + whitePawn[1] };
        }
    }


    public static int[,] GetKingMove(int r,int c)
    {
        int[,] temp = (int[,])KingMovement.Clone();
        
        return temp;

    }



    
}
