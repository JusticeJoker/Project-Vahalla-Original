using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
    Grid grid;

    public Transform StartPosition;
    public Transform TargetPosition;

    private void Awake()
    {
        grid = GetComponent<Grid>();
    }

    private void Update()
    {
        FindPath(StartPosition.position, TargetPosition.position);
    }

    void FindPath (Vector3 _startPos, Vector3 _targetPos)
    {
        Node StartNode = grid.NodeFromWorldPosition(_startPos);
        Node TargetNode = grid.NodeFromWorldPosition(_targetPos);

        List<Node> OpenList = new List<Node>();
        HashSet<Node> ClosedList = new HashSet<Node>();

        OpenList.Add(StartNode);

        while (OpenList.Count > 0)
        {
            Node CurrentNode = OpenList[0];
            for (int i = 0; i < OpenList.Count; i++)
            {
                if (OpenList[i].FCost < CurrentNode.FCost || OpenList[i].FCost ==
                    CurrentNode.FCost && OpenList[i].hCost < CurrentNode.hCost)
                        CurrentNode = OpenList[i];
            }

            OpenList.Remove(CurrentNode);
            ClosedList.Add(CurrentNode);

            if (CurrentNode == TargetNode)
                GetFinalPath(StartNode, TargetNode);

            foreach (Node NeighborNode in grid.GetNeighborNodes(CurrentNode))
            {
                if (!NeighborNode.IsLimits || ClosedList.Contains(NeighborNode))
                    continue;

                int MoveCost = CurrentNode.gCost + GetManhattanDistance(CurrentNode, NeighborNode);

                if (MoveCost < NeighborNode.gCost || !OpenList.Contains(NeighborNode))
                {
                    NeighborNode.gCost = MoveCost;
                    NeighborNode.hCost = GetManhattanDistance(NeighborNode, TargetNode);
                    NeighborNode.Parent = CurrentNode;

                    if (!OpenList.Contains(NeighborNode))
                    {
                        OpenList.Add(NeighborNode);
                    }
                }
            }

        }
    }

    void GetFinalPath(Node _startNode, Node _finalNode)
    {
        List<Node> FinalPath = new List<Node>();
        Node CurrentNode = _finalNode;

        while (CurrentNode != _startNode)
        {
            FinalPath.Add(CurrentNode);
            CurrentNode = CurrentNode.Parent;
        }

        FinalPath.Reverse();

        grid.FinalPath = FinalPath;
    }

    int GetManhattanDistance(Node _nodeA, Node _nodeB)
    {
        int disX = Mathf.Abs(_nodeA.gridX - _nodeB.gridX);
        int disY = Mathf.Abs(_nodeA.gridY - _nodeB.gridY);

        return disX + disY;
    }
}
