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
    
    public int LevelIndex;
    public LoadPointDir Direction;
}
