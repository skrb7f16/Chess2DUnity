
public class Moves
{
    public static byte COLOR_BLACK = 0;
    public static byte COLOR_WHITE = 1;

    public static int[] whitePawn = { 1, 0 };
    public static int[] blackPawn = { -1, 0 };

    public static int[,] KingMovement = { {1,0 },{ 0,1},{ -1,0},{ 0,-1},{ 1,1},{ -1,-1},{ 1,-1},{ -1,1} };
    public static int[,] KnightMovement = { {2,1 },{ 2,-1},{ 1,2},{ 1,-2},{ -1,2},{ -1,-2},{ -2,-1},{ -2,1} };

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


    public static void GetKingMove(int r,int c,int color)
    {
        int[,] temp = (int[,])KingMovement.Clone();
        for (int i = 0; i < 8; i++)
        {

            temp[i, 0] += r;
            temp[i, 1] += c;
            if (temp[i, 1] < 8 && temp[i, 1] >= 0 && temp[i, 0] < 8 && temp[i, 0] >= 0)
            {
                Position pos = Board.instance.GetPositionForNextMove(temp[i, 0], temp[i, 1]);
                if (!pos.HasPiece() || pos.GetCharacter().color != color)
                    Board.instance.positionsToMove.Add(pos);
            }

        }

        

    }

    public static void GetKnightMove(int r,int c,int color)
    {
        int[,] temp = (int[,])KnightMovement.Clone();
        for (int i = 0; i < 8; i++)
        {

            temp[i, 0] += r;
            temp[i, 1] += c;
            if (temp[i, 1] < 8 && temp[i, 1] >= 0 && temp[i, 0] < 8 && temp[i, 0] >= 0)
            {
                Position pos = Board.instance.GetPositionForNextMove(temp[i, 0], temp[i, 1]);
                if (!pos.HasPiece())
                {
                    Board.instance.positionsToMove.Add(pos);
                }else if(pos.GetCharacter().color != color)
                {
                    pos.SetEnemyToBeKilled(true);
                    Board.instance.positionsToMove.Add(pos);
                }
                    
            }

        }

    }



    
}
