using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadPoint : MonoBehaviour
{
    public enum LoadPointDir
    {
        NORTH,
        SOUTH,
        WEST,
        EAST
    }

    public static LoadPointDir GetOpposite(LoadPointDir dir)
    {
        switch (dir)
        {
            case LoadPointDir.NORTH: return LoadPointDir.SOUTH;
            case LoadPointDir.SOUTH: return LoadPointDir.NORTH;
            case LoadPointDir.EAST: return LoadPointDir.WEST;
            case LoadPointDir.WEST: return LoadPointDir.EAST;
            default: return dir;
        }
    }
    
    public int LevelIndex;
    public LoadPointDir Direction;
}
