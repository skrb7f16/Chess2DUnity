
public class Moves
{
    public static byte COLOR_BLACK = 0;
    public static byte COLOR_WHITE = 1;

    public static int[] whitePawn = { 1, 0 };
    public static int[] blackPawn = { -1, 0 };

    public static int[,] KingMovement = { {1,0 },{ 0,1},{ -1,0},{ 0,-1},{ 1,1},{ -1,-1},{ 1,-1},{ -1,1} };
    public static int[,] KnightMovement = { {2,1 },{ 2,-1},{ 1,2},{ 1,-2},{ -1,2},{ -1,-2},{ -2,-1},{ -2,1} };


    //north,south,west,east,northwest,northeast,southwest,southeast
    public static int[] MoveDirAllowed = { 1, 1, 1, 1,1,1,1,1 };

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
                if (!pos.HasPiece())
                {
                    Board.instance.positionsToMove.Add(pos);
                }
                else if (pos.GetCharacter().color != color)
                {
                    pos.SetEnemyToBeKilled(true);
                    Board.instance.positionsToMove.Add(pos);
                }
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


    public static void GetElephantMove(int r,int c,int color)
    {
        MoveDirAllowed[4] = 0;
        MoveDirAllowed[5] = 0;
        MoveDirAllowed[6] = 0;
        MoveDirAllowed[7] = 0;
        for(int i = 1; i < 8; i++)
        {
            int[] up = { r + i, c };
            int[] down = { r - i, c };
            int[] left = { r, c - i };
            int[] right = { r, c + i };
            
            if(MoveDirAllowed[0]==1)
            CheckAndAddForNextMove(up, color,0);
            if(MoveDirAllowed[1]==1)
            CheckAndAddForNextMove(down, color,1);
            if(MoveDirAllowed[2]==1)
            CheckAndAddForNextMove(left, color,2);
            if(MoveDirAllowed[3]==1)
            CheckAndAddForNextMove(right, color,3);

        }
        for (int i = 0; i < 8; i++) MoveDirAllowed[i] = 1;
    }


    public static void GetBishopMove(int r,int c,int color)
    {
        MoveDirAllowed[0] = 0;
        MoveDirAllowed[1] = 0;
        MoveDirAllowed[2] = 0;
        MoveDirAllowed[3] = 0;
        for(int i = 1; i < 8; i++)
        {
            int[] nw = { r + i, c - i };
            int[] ne = { r + i, c + i };
            int[] sw = { r - i, c - i };
            int[] se = { r - i, c + i };

            if (MoveDirAllowed[4] == 1)
                CheckAndAddForNextMove(nw, color, 4);
            if (MoveDirAllowed[5] == 1)
                CheckAndAddForNextMove(ne, color, 5);
            if (MoveDirAllowed[6] == 1)
                CheckAndAddForNextMove(sw, color, 6);
            if (MoveDirAllowed[7] == 1)
                CheckAndAddForNextMove(se, color, 7);

        }

        for (int i = 0; i < 8; i++) MoveDirAllowed[i] = 1;
    }

    public static void GetQueenMove(int r,int c,int color)
    {
        GetElephantMove(r, c, color);
        GetBishopMove(r, c, color);
    }


    public static void CheckAndAddForNextMove(int[] temp,int color,int indexOfDir)
    {
        


        if (temp[1] < 8 && temp[ 1] >= 0 && temp[ 0] < 8 && temp[ 0] >= 0)
        {
            Position pos = Board.instance.GetPositionForNextMove(temp[ 0], temp[ 1]);
            if (!pos.HasPiece())
            {
                Board.instance.positionsToMove.Add(pos);
            }
            else if (pos.GetCharacter().color != color)
            {
                pos.SetEnemyToBeKilled(true);
                Board.instance.positionsToMove.Add(pos);
                MoveDirAllowed[indexOfDir] = 0;
            }
            else
            {
                MoveDirAllowed[indexOfDir] = 0;
            }

        }
    }





    
}
