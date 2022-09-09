using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public int gridX;
    public int gridY;

    public bool IsLimits;
    public Vector3 Position;

    public Node Parent;

    public int gCost;
    public int hCost;

    public int FCost { get { return gCost + hCost; } }

    public Node(bool _isLimits, Vector3 _Pos, int _gridX, int _gridY)
    {
        IsLimits = _isLimits;
        Position = _Pos;
        gridX = _gridX;
        gridY = _gridY;
    }

}
