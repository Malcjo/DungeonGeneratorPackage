using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TileCalculator
{
    Cell[,] grid;
    Vector2 min;
    Vector2 max;
    public TileCalculator(Cell[,] Grid, Vector2 min, Vector2 max)
    {
        this.min = min;
        this.max = max;
        grid = Grid;
    }

    //placeholder way, I think these could be recursive as well
    public PieceType CheckPiece(int x, int y)
    {
        if (isDoorWayInMiddleV2(new Vector2Int(x, y)))
        {
            return PieceType.Doorway;
        }
        else if (isDoorwayOnRightCorner(new Vector2Int(x, y)))
        {
            return PieceType.DoorwayRightCorner;
        }
        else if (isDoorwayOnLeftCorner(new Vector2Int(x, y)))
        {
            return PieceType.DoorwayLeftCorner;
        }
        else if (IsHallway(new Vector2Int(x, y)))
        {
            return PieceType.Hallway;
        }
        else if (IsCorner(new Vector2Int(x, y)))
        {
            return PieceType.Corner;
        }
        else if (IsEdge(new Vector2Int(x, y)))
        {
            return PieceType.Edge;
        }
        return PieceType.Empty;
    }

    // Potentially replace with all Vector2Int (unless we need floats, then Vector2)
    // to render this.
    bool IsPieceV2(Vector2Int coordinates)
    {
        return IsPiece(coordinates);

    }

    bool IsPiece(Vector2Int coordinates)
    {
        if (coordinates.x < min.x || coordinates.y < min.y || coordinates.x > max.x - 1 || coordinates.y > max.y - 1)
        {
            return false;
        }
        else if (grid[coordinates.x, coordinates.y].content == Cell.Contents.Tile)
        {
            return true;
        }
        else if ((grid[coordinates.x, coordinates.y].content == Cell.Contents.Empty))
        {
            return false;
        }
        return false;
    }

    bool isDoorwayOnRightCorner(Vector2Int startCoord)
    {
        //Checking if tile is at a corner
        if (IsPieceV2(startCoord + Vector2Int.up) &&
            IsPieceV2(startCoord + Vector2Int.down) &&
            IsPieceV2(startCoord + Vector2Int.right) &&
            !IsPieceV2(startCoord + Vector2Int.left)) // nothin on LEFT
        {
            if (!IsPieceV2(startCoord + (Vector2Int.left + Vector2Int.up)) && //Checking the TOP Tile has pieces on the left and right
                !IsPieceV2(startCoord + (Vector2Int.right + Vector2Int.up)))
            {
                grid[startCoord.x, startCoord.y].rotation = 180;
                return true;
            }
        }
        else if (!IsPieceV2(startCoord + Vector2Int.up) &&
            IsPieceV2(startCoord + Vector2Int.down) &&
            IsPieceV2(startCoord + Vector2Int.right) &&
            IsPieceV2(startCoord + Vector2Int.left)) //TOP
        {
            if (!IsPieceV2(startCoord + (Vector2Int.right + Vector2Int.up)) && //Checking the RIGHT Tile has pieces on the left and right
                !IsPieceV2(startCoord + (Vector2Int.right + Vector2Int.down)))
            {
                grid[startCoord.x, startCoord.y].rotation = 90;
                return true;
            }
        }
        else if (IsPieceV2(startCoord + Vector2Int.up) &&
            IsPieceV2(startCoord + Vector2Int.down) &&
            !IsPieceV2(startCoord + Vector2Int.right) &&
            IsPieceV2(startCoord + Vector2Int.left)) //RIGHT
        {
            if (!IsPieceV2(startCoord + (Vector2Int.right + Vector2Int.down)) && //Checking the BOTTOM Tile has pieces on the left and right
                !IsPieceV2(startCoord + (Vector2Int.left + Vector2Int.down)))
            {
                grid[startCoord.x, startCoord.y].rotation = 0;
                return true;
            }
        }
        else if (IsPieceV2(startCoord + Vector2Int.up) &&
            !IsPieceV2(startCoord + Vector2Int.down) &&
            IsPieceV2(startCoord + Vector2Int.right) &&
            IsPieceV2(startCoord + Vector2Int.left)) //BOTTOM
        {
            if (!IsPieceV2(startCoord + (Vector2Int.left + Vector2Int.up)) && //Checking the LEFT Tile has pieces on the left and right
                !IsPieceV2(startCoord + (Vector2Int.left + Vector2Int.down)))
            {
                grid[startCoord.x, startCoord.y].rotation = 270;
                return true;
            }
        }
        return false;
    }

    bool isDoorwayOnLeftCorner(Vector2Int startCoord)
    {
        //Checking if tile is at a corner
        if (IsPieceV2(startCoord + Vector2Int.up) &&
            IsPieceV2(startCoord + Vector2Int.down) &&
            IsPieceV2(startCoord + Vector2Int.right) &&
            !IsPieceV2(startCoord + Vector2Int.left)) // LEFT
        {
            if (!IsPieceV2(startCoord + (Vector2Int.left + Vector2Int.down)) && //Checking the Bottom Tile has pieces on the left and right
                !IsPieceV2(startCoord + (Vector2Int.right + Vector2Int.down)))
            {
                grid[startCoord.x, startCoord.y].rotation = 0;
                return true;
            }
        }
        else if (!IsPieceV2(startCoord + Vector2Int.up) &&
            IsPieceV2(startCoord + Vector2Int.down) &&
            IsPieceV2(startCoord + Vector2Int.right) &&
            IsPieceV2(startCoord + Vector2Int.left)) //TOP
        {
            if (!IsPieceV2(startCoord + (Vector2Int.left + Vector2Int.up)) && //Checking the RIGHT Tile has pieces on the left and right
                !IsPieceV2(startCoord + (Vector2Int.left + Vector2Int.down)))
            {
                grid[startCoord.x, startCoord.y].rotation = 270;
                return true;
            }
        }
        else if (IsPieceV2(startCoord + Vector2Int.up) &&
        IsPieceV2(startCoord + Vector2Int.down) &&
        !IsPieceV2(startCoord + Vector2Int.right) &&
        IsPieceV2(startCoord + Vector2Int.left)) //RIGHT
        {
            if (!IsPieceV2(startCoord + (Vector2Int.right + Vector2Int.up)) && //Checking the BOTTOM Tile has pieces on the left and right
                !IsPieceV2(startCoord + (Vector2Int.left + Vector2Int.up)))
            {
                grid[startCoord.x, startCoord.y].rotation = 180;
                return true;
            }
        }
        else if (IsPieceV2(startCoord + Vector2Int.up) &&
        !IsPieceV2(startCoord + Vector2Int.down) &&
        IsPieceV2(startCoord + Vector2Int.right) &&
        IsPieceV2(startCoord + Vector2Int.left)) //BOTTOM
        {
            if (!IsPieceV2(startCoord + (Vector2Int.right + Vector2Int.up)) && //Checking the LEFT Tile has pieces on the left and right
                !IsPieceV2(startCoord + (Vector2Int.right + Vector2Int.down)))
            {
                grid[startCoord.x, startCoord.y].rotation = 90;
                return true;
            }
        }
        return false;
    }

    /**
     * Terrible name, but trying to determine if we have
     * something like.
     *    
     *      X
     *     XxX
     *      X
     */
    bool isCrossCenter(Vector2Int startCoord)
    {
        return (
            IsPieceV2(startCoord + Vector2Int.left) &&
            IsPieceV2(startCoord + Vector2Int.right) &&
            IsPieceV2(startCoord + Vector2Int.up) &&
            IsPieceV2(startCoord + Vector2Int.down)
            );
    }


    bool isDoorWayInMiddleV2(Vector2Int coordinates)
    {
        if (isCrossCenter(coordinates))
        {
            return CheckIfDoorIsNextToHallway(coordinates);
        }
        else
        {
            return false;
        }
    }

    bool CheckIfDoorIsNextToHallway(Vector2Int startCoord)
    {
        if (IsPieceV2(startCoord + Vector2Int.up) &&
            !IsPieceV2(startCoord + (Vector2Int.up + Vector2Int.left)) &&
            !IsPieceV2(startCoord + (Vector2Int.up + Vector2Int.right)))
        {
            if (IsPieceV2(startCoord + Vector2Int.down) &&
                !IsPieceV2(startCoord + (Vector2Int.down + Vector2Int.left)) &&
                !IsPieceV2(startCoord + (Vector2Int.down + Vector2Int.right)))
            {
                return false;
            }
            grid[startCoord.x, startCoord.y].rotation = 180;
        }
        else if (IsPieceV2(startCoord + (Vector2Int.right)) &&
            !IsPieceV2(startCoord + (Vector2Int.right + Vector2Int.up)) &&
            !IsPieceV2(startCoord + (Vector2Int.right + Vector2Int.down)))
        {
            if (IsPieceV2(startCoord + (Vector2Int.left)) &&
            !IsPieceV2(startCoord + (Vector2Int.left + Vector2Int.up)) &&
            !IsPieceV2(startCoord + (Vector2Int.left + Vector2Int.down)))
            {
                return false;
            }
            grid[startCoord.x, startCoord.y].rotation = 90;
            return true;
        }
        else if (IsPieceV2(startCoord + (Vector2Int.down)) &&
            !IsPieceV2(startCoord + (Vector2Int.down + Vector2Int.right)) &&
            !IsPieceV2(startCoord + (Vector2Int.down + Vector2Int.left)))
        {
            if (IsPieceV2(startCoord + (Vector2Int.up)) &&
                !IsPieceV2(startCoord + (Vector2Int.up + Vector2Int.right)) &&
                !IsPieceV2(startCoord + (Vector2Int.up + Vector2Int.left)))
            {
                return false;
            }
            grid[startCoord.x, startCoord.y].rotation = 0;
            return true;
        }
        else if (IsPieceV2(startCoord + (Vector2Int.left)) &&
                !IsPieceV2(startCoord + (Vector2Int.left + Vector2Int.up)) &&
                !IsPieceV2(startCoord + (Vector2Int.left + Vector2Int.down)))
        {
            if (IsPieceV2(startCoord + (Vector2Int.right)) &&
                !IsPieceV2(startCoord + (Vector2Int.right + Vector2Int.up)) &&
                !IsPieceV2(startCoord + (Vector2Int.right + Vector2Int.down)))
            {
                return false;
            }
            grid[startCoord.x, startCoord.y].rotation = 270;
            return true;
        }
        return false;
    }
    bool IsHallway(Vector2Int startCoord)
    {
        if (!IsPieceV2(startCoord + Vector2Int.right) &&
            !IsPieceV2(startCoord + Vector2Int.left) &&
            IsPieceV2(startCoord + Vector2Int.up) &&
            IsPieceV2(startCoord + Vector2Int.down))
        {
            grid[startCoord.x, startCoord.y].rotation = 0;
            return true;
        }
        else if (IsPiece(startCoord + Vector2Int.right) && //Checking Left and Right tiles
            IsPiece(startCoord + Vector2Int.left) &&
            !IsPiece(startCoord + Vector2Int.up) &&
            !IsPiece(startCoord + Vector2Int.down))
        {
            grid[startCoord.x, startCoord.y].rotation = 90;
            return true;
        }
        return false;
    }
    bool IsCorner(Vector2Int startCoord)
    {
        if (IsPieceV2(startCoord + Vector2Int.right) &&
            !IsPieceV2(startCoord + Vector2Int.left) &&
            IsPieceV2(startCoord + Vector2Int.up) &&
            !IsPieceV2(startCoord + Vector2Int.down)) //Top Left x+1 y, x y+1
        {
            grid[startCoord.x, startCoord.y].rotation = 0;
            return true;
        }
        else if (!IsPieceV2(startCoord + Vector2Int.right) &&
            IsPieceV2(startCoord + Vector2Int.left) &&
            IsPieceV2(startCoord + Vector2Int.up) &&
            !IsPieceV2(startCoord + Vector2Int.down)) //Top Right x y+1, x-1 y
        {
            grid[startCoord.x, startCoord.y].rotation = 90;
            return true;
        }
        else if (!IsPieceV2(startCoord + Vector2Int.right) &&
            IsPieceV2(startCoord + Vector2Int.left) &&
            !IsPieceV2(startCoord + Vector2Int.up) &&
            IsPieceV2(startCoord + Vector2Int.down))//Bottom Left x-1 y, x y-1
        {
            grid[startCoord.x, startCoord.y].rotation = 180;
            return true;
        }
        else if (IsPieceV2(startCoord + Vector2Int.right) &&
            !IsPieceV2(startCoord + Vector2Int.left) &&
            !IsPieceV2(startCoord + Vector2Int.up) &&
            IsPieceV2(startCoord + Vector2Int.down))//Bottom Right x y-1, x+1 y
        {
            grid[startCoord.x, startCoord.y].rotation = 270;
            return true;
        }
        return false;
    }

    bool IsEdge(Vector2Int startCoord)
    {
        if (IsPieceV2(startCoord + Vector2Int.right) &&
            !IsPieceV2(startCoord + Vector2Int.left) &&
            IsPieceV2(startCoord + Vector2Int.up) &&
            IsPieceV2(startCoord + Vector2Int.down))
        {
            grid[startCoord.x, startCoord.y].rotation = 0;
            return true;
        }
        else if (IsPieceV2(startCoord + Vector2Int.right) &&
            IsPieceV2(startCoord + Vector2Int.left) &&
            IsPieceV2(startCoord + Vector2Int.up) &&
            !IsPieceV2(startCoord + Vector2Int.down))
        {
            grid[startCoord.x, startCoord.y].rotation = 90;
            return true;
        }
        else if (!IsPieceV2(startCoord + Vector2Int.right) &&
            IsPieceV2(startCoord + Vector2Int.left) &&
            IsPieceV2(startCoord + Vector2Int.up) &&
            IsPieceV2(startCoord + Vector2Int.down))
        {
            grid[startCoord.x, startCoord.y].rotation = 180;
            return true;
        }
        else if (IsPieceV2(startCoord + Vector2Int.right) &&
            IsPieceV2(startCoord + Vector2Int.left) &&
            !IsPieceV2(startCoord + Vector2Int.up) &&
            IsPieceV2(startCoord + Vector2Int.down))
        {
            grid[startCoord.x, startCoord.y].rotation = 270;
            return true;
        }
        return false;
    }

}
