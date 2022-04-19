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
        //if (isDoorWayInMiddle(x, y))
        //{
        //    return PieceType.Doorway;
        //}
        if (isDoorWayInMiddleV2(new Vector2Int(x, y)))
        {
            return PieceType.Doorway;
        }
        else if (isDoorwayOnRightCorner(x, y))
        {
            return PieceType.DoorwayRightCorner;
        }
        else if (isDoorwayOnLeftCorner(x, y))
        {
            return PieceType.DoorwayLeftCorner;
        }
        else if (IsHallway(x, y))
        {
            return PieceType.Hallway;
        }
        else if (IsCorner(x, y))
        {
            return PieceType.Corner;
        }
        else if (IsEdge(x, y))
        {
            return PieceType.Edge;
        }
        return PieceType.Empty;
    }

    // Potentially replace with all Vector2Int (unless we need floats, then Vector2)
    // to render this.
    bool IsPieceV2(Vector2Int coordinates)
    {
        return IsPiece(coordinates.x, coordinates.y);

    }

    bool IsPiece(int x, int y)
    {
        if (x < min.x || y < min.y || x > (int)max.x - 1 || y > (int)max.y - 1)
        {
            return false;
        }
        else if (grid[(int)x, (int)y].content == Cell.Contents.Tile)
        {
            return true;
        }
        else if ((grid[(int)x, (int)y].content == Cell.Contents.Empty))
        {
            return false;
        }
        return false;
    }

    bool isDoorwayOnRightCorner(int x, int y)
    {
        //Checking if tile is at a corner
        if (IsPiece(x, y + 1) &&
            IsPiece(x, y - 1) &&
            IsPiece(x + 1, y) &&
            !IsPiece(x - 1, y)) // nothin on LEFT
        {
            if (!IsPiece(x - 1, y + 1) && //Checking the TOP Tile has pieces on the left and right
                !IsPiece(x + 1, y + 1))
            {
                grid[x, y].rotation = 180;
                return true;
            }
        }
        else if (!IsPiece(x, y + 1) &&
            IsPiece(x, y - 1) &&
            IsPiece(x + 1, y) &&
            IsPiece(x - 1, y)) //TOP
        {
            if (!IsPiece(x + 1, y + 1) && //Checking the RIGHT Tile has pieces on the left and right
                !IsPiece(x + 1, y - 1))
            {
                grid[x, y].rotation = 90;
                return true;
            }
        }
        else if (IsPiece(x, y + 1) &&
            IsPiece(x, y - 1) &&
            !IsPiece(x + 1, y) &&
            IsPiece(x - 1, y)) //RIGHT
        {
            if (!IsPiece(x + 1, y - 1) && //Checking the BOTTOM Tile has pieces on the left and right
                !IsPiece(x - 1, y - 1))
            {
                grid[x, y].rotation = 0;
                return true;
            }
        }
        else if (IsPiece(x, y + 1) &&
            !IsPiece(x, y - 1) &&
            IsPiece(x + 1, y) &&
            IsPiece(x - 1, y)) //BOTTOM
        {
            if (!IsPiece(x - 1, y + 1) && //Checking the LEFT Tile has pieces on the left and right
                !IsPiece(x - 1, y - 1))
            {
                grid[x, y].rotation = 270;
                return true;
            }
        }
        return false;
    }

    bool isDoorwayOnLeftCorner(int x, int y)
    {
        //Checking if tile is at a corner
        if (IsPiece(x, y + 1) &&
            IsPiece(x, y - 1) &&
            IsPiece(x + 1, y) &&
            !IsPiece(x - 1, y)) // LEFT
        {
            if (!IsPiece(x - 1, y - 1) && //Checking the Bottom Tile has pieces on the left and right
                !IsPiece(x + 1, y - 1))
            {
                grid[x, y].rotation = 0;
                return true;
            }
        }
        else if (!IsPiece(x, y + 1) &&
            IsPiece(x, y - 1) &&
            IsPiece(x + 1, y) &&
            IsPiece(x - 1, y)) //TOP
        {
            if (!IsPiece(x - 1, y + 1) && //Checking the RIGHT Tile has pieces on the left and right
                !IsPiece(x - 1, y - 1))
            {
                grid[x, y].rotation = 270;
                return true;
            }
        }
        else if (IsPiece(x, y + 1) &&
        IsPiece(x, y - 1) &&
        !IsPiece(x + 1, y) &&
        IsPiece(x - 1, y)) //RIGHT
        {
            if (!IsPiece(x + 1, y + 1) && //Checking the BOTTOM Tile has pieces on the left and right
                !IsPiece(x - 1, y + 1))
            {
                grid[x, y].rotation = 180;
                return true;
            }
        }
        else if (IsPiece(x, y + 1) &&
        !IsPiece(x, y - 1) &&
        IsPiece(x + 1, y) &&
        IsPiece(x - 1, y)) //BOTTOM
        {
            if (!IsPiece(x + 1, y + 1) && //Checking the LEFT Tile has pieces on the left and right
                !IsPiece(x + 1, y - 1))
            {
                grid[x, y].rotation = 90;
                return true;
            }
        }
        return false;
    }
    bool isDoorWayInMiddle(int x, int y)
    {
        if (IsPiece(x, y + 1) && 
            IsPiece(x, y - 1) && 
            IsPiece(x + 1, y) && 
            IsPiece(x - 1, y)) //Checking if tile is surround by all 4 sides
        {
            return CheckIfDoorIsNextToHallway(x, y);
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
            return CheckIfDoorIsNextToHallway(coordinates.x, coordinates.y);
        } else
        {
            return false;
        }

        //if (IsPiece(x, y + 1) &&
        //    IsPiece(x, y - 1) &&
        //    IsPiece(x + 1, y) &&
        //    IsPiece(x - 1, y)) //Checking if tile is surround by all 4 sides
        //{
        //    return CheckIfDoorIsNextToHallway(x, y);
        //}
        //return false;
    }

    bool CheckIfDoorIsNextToHallway(int x, int y)
    {
        //checking adjacent tiles for ~TOP~ tile
        if (IsPiece(x, y + 1) &&
            !IsPiece(x - 1, y + 1) &&
            !IsPiece(x + 1, y + 1))
        {
            //checking opposite facing tile just in case we're looking in a long hallway
            if (IsPiece(x, y - 1) &&
                !IsPiece(x - 1, y - 1) &&
                !IsPiece(x + 1, y - 1))
            {
                return false;
            }
            grid[x, y].rotation = 180;
            return true;
        }
        //checking adjacent tiles for ~RIGHT~ tile
        else if (IsPiece(x + 1, y) &&
                !IsPiece(x + 1, y + 1) &&
                !IsPiece(x + 1, y - 1))
        {
            //checking opposite facing tile just in case we're looking in a long hallway
            if (IsPiece(x - 1, y) &&
                !IsPiece(x - 1, y + 1) &&
                !IsPiece(x - 1, y - 1))
            {
                return false;
            }
            grid[x, y].rotation = 90;
            return true;
        }
        //checking adjacent tiles for ~BOTTOM~ tile
        else if (IsPiece(x, y - 1) &&
                !IsPiece(x + 1, y - 1) &&
                !IsPiece(x - 1, y - 1))
        {
            //checking opposite facing tile just in case we're looking in a long hallway
            if (IsPiece(x, y + 1) &&
                !IsPiece(x + 1, y + 1) &&
                !IsPiece(x - 1, y + 1))
            {
                return false;
            }
            grid[x, y].rotation = 0;
            return true;
        }
        //checking adjacent tiles for ~LEFT~ tile
        else if (IsPiece(x - 1, y) &&
                !IsPiece(x - 1, y + 1) &&
                !IsPiece(x - 1, y - 1))
        {
            if (IsPiece(x + 1, y) &&
                !IsPiece(x + 1, y + 1) &&
                !IsPiece(x + 1, y - 1))
            {
                return false;
            }
            grid[x, y].rotation = 270;
            return true;
        }
        return false;
    }
    bool IsHallway(int x, int y)
    {
        //Checking Top and Bottom tiles
        if (!IsPiece(x + 1, y) && 
            !IsPiece(x - 1, y) && 
            IsPiece(x, y + 1) && 
            IsPiece(x, y - 1))
        {
            grid[x, y].rotation = 0;
            return true;
        }
        else if (IsPiece(x + 1, y) && //Checking Left and Right tiles
            IsPiece(x - 1, y) && 
            !IsPiece(x, y + 1) && 
            !IsPiece(x, y - 1))
        {
            grid[x, y].rotation = 90;
            return true;
        }
        return false;
    }
    bool IsCorner(int x, int y)
    {
        if (IsPiece(x + 1, y) && 
            !IsPiece(x - 1, y) && 
            IsPiece(x, y + 1) && 
            !IsPiece(x, y - 1)) //Top Left x+1 y, x y+1
        {
            grid[x, y].rotation = 0;
            return true;
        }
        else if (!IsPiece(x + 1, y) && 
            IsPiece(x - 1, y) && 
            IsPiece(x, y + 1) && 
            !IsPiece(x, y - 1)) //Top Right x y+1, x-1 y
        {
            grid[x, y].rotation = 90;
            return true;
        }
        else if (!IsPiece(x + 1, y) && 
            IsPiece(x - 1, y) && 
            !IsPiece(x, y + 1) && 
            IsPiece(x, y - 1))//Bottom Left x-1 y, x y-1
        {
            grid[x, y].rotation = 180;
            return true;
        }
        else if (IsPiece(x + 1, y) && 
            !IsPiece(x - 1, y) && 
            !IsPiece(x, y + 1) && 
            IsPiece(x, y - 1))//Bottom Right x y-1, x+1 y
        {
            grid[x, y].rotation = 270;
            return true;
        }
        return false;
    }

    bool IsEdge(int x, int y)
    {
        if (IsPiece(x + 1, y) && 
            !IsPiece(x - 1, y) && 
            IsPiece(x, y + 1) && 
            IsPiece(x, y - 1))
        {
            grid[x, y].rotation = 0;
            return true;
        }
        else if (IsPiece(x + 1, y) && 
            IsPiece(x - 1, y) && 
            IsPiece(x, y + 1) && 
            !IsPiece(x, y - 1))
        {
            grid[x, y].rotation = 90;
            return true;
        }
        else if (!IsPiece(x + 1, y) && 
            IsPiece(x - 1, y) && 
            IsPiece(x, y + 1) && 
            IsPiece(x, y - 1))
        {
            grid[x, y].rotation = 180;
            return true;
        }
        else if (IsPiece(x + 1, y) && 
            IsPiece(x - 1, y) && 
            !IsPiece(x, y + 1) && 
            IsPiece(x, y - 1))
        {
            grid[x, y].rotation = 270;
            return true;
        }
        return false;
    }

}
