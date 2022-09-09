using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public Transform StartPosition;
    public LayerMask LimitsMask;
    public Vector2 gridWorldSize;
    public float nodeRadius;
    public float Distance;

    Node[,] gridArray;
    public List<Node> FinalPath;

    float nodeDiameter;
    int gridSizeX, gridSizeY;

    private void Start()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);        
        CreateGrid();
    }

    void CreateGrid()
    {
        gridArray = new Node[gridSizeX, gridSizeY];
        Vector3 bottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.forward * gridWorldSize.y / 2;
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                    Vector3 worldPoint = bottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);
                    bool Wall = true;

                    if (Physics.CheckSphere(worldPoint, nodeRadius, LimitsMask))
                        Wall = false;

                    gridArray[x, y] = new Node(Wall, worldPoint, x, y);
                               
            }
        }
    }

    public List<Node> GetNeighborNodes(Node _node)
    {
        List<Node> NeighborNodes = new List<Node>();
        int checkX;
        int checkY;

        //Right Side
        checkX = _node.gridX + 1;
        checkY = _node.gridY;

        if (checkX >= 0 && checkX < gridSizeX)
        {
            if (checkY >= 0 && checkY < gridSizeY)
            {
                NeighborNodes.Add(gridArray[checkX, checkY]);
            }
        }

        //Left Side
        checkX = _node.gridX - 1;
        checkY = _node.gridY;

        if (checkX >= 0 && checkX < gridSizeX)
        {
            if (checkY >= 0 && checkY < gridSizeY)
            {
                NeighborNodes.Add(gridArray[checkX, checkY]);
            }
        }

        //Top Side
        checkX = _node.gridX;
        checkY = _node.gridY + 1;

        if (checkX >= 0 && checkX < gridSizeX)
        {
            if (checkY >= 0 && checkY < gridSizeY)
            {
                NeighborNodes.Add(gridArray[checkX, checkY]);
            }
        }

        //Bottom Side
        checkX = _node.gridX;
        checkY = _node.gridY - 1;

        if (checkX >= 0 && checkX < gridSizeX)
        {
            if (checkY >= 0 && checkY < gridSizeY)
            {
                NeighborNodes.Add(gridArray[checkX, checkY]);
            }
        }

        return NeighborNodes;
    }

    public Node NodeFromWorldPosition(Vector3 _worldPosition)
    {
        float pointX = ((_worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x);
        float pointY = ((_worldPosition.z + gridWorldSize.y / 2) / gridWorldSize.y);

        pointX = Mathf.Clamp01(pointX);
        pointY = Mathf.Clamp01(pointY);

        int x = Mathf.RoundToInt((gridSizeX - 1) * pointX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * pointY);

        return gridArray[x, y];

    }


    //Grid Debug
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 1, gridWorldSize.y)); //Draw grid debug

        if (gridArray != null)
        {
            foreach (Node node in gridArray) //Loop through every node in the grid
            {
                if (node.IsLimits)
                {
                    Gizmos.color = Color.white;
                }
                else
                {
                    Gizmos.color = Color.red;
                }

                if (FinalPath != null)
                {
                    if (FinalPath.Contains(node)) //If the current node is in the final path
                        Gizmos.color = Color.blue; //Draw the color of that node
                }

                Gizmos.DrawCube(node.Position, Vector3.one * (nodeDiameter - Distance)); //Draw the node athe the position of the node
            }
        }
    }
}
