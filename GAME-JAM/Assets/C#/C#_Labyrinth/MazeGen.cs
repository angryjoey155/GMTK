using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    [SerializeField] GameObject _player;
    [SerializeField] MazeCell nodePrefab;
    [SerializeField] Vector2Int mazeSize;
    float removeChance = 0.50f;
    public static List<MazeCell> nodes = new List<MazeCell>();

    private void Start()
    {
        GenerateMazeInstant(mazeSize);
        DisableValls(mazeSize);
    }

    void GenerateMazeInstant(Vector2Int size)
    {

        // Create nodes
        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                Vector3 nodePos = new Vector3(3*x - (size.x / 2f), 3*y - (size.y / 2f), 0);
                MazeCell newNode = Instantiate(nodePrefab, nodePos, Quaternion.identity, transform);
                nodes.Add(newNode);
            }
        }
        List<MazeCell> currentPath = new List<MazeCell>();
        List<MazeCell> completedNodes = new List<MazeCell>();

        // Choose starting node
        currentPath.Add(nodes[UnityEngine.Random.Range(0, nodes.Count)]);

        while (completedNodes.Count < nodes.Count)
        {
            // Check nodes next to the current node
            List<int> possibleNextNodes = new List<int>();
            List<int> possibleDirections = new List<int>();

            int currentNodeIndex = nodes.IndexOf(currentPath[currentPath.Count - 1]);
            int currentNodeX = currentNodeIndex / size.y;
            int currentNodeY = currentNodeIndex % size.y;

            if (currentNodeX < size.x - 1)
            {
                // Check node to the right of the current node
                if (!completedNodes.Contains(nodes[currentNodeIndex + size.y]) &&
                    !currentPath.Contains(nodes[currentNodeIndex + size.y]))
                {
                    possibleDirections.Add(1);
                    possibleNextNodes.Add(currentNodeIndex + size.y);
                }
            }
            if (currentNodeX > 0)
            {
                // Check node to the left of the current node
                if (!completedNodes.Contains(nodes[currentNodeIndex - size.y]) &&
                    !currentPath.Contains(nodes[currentNodeIndex - size.y]))
                {
                    possibleDirections.Add(2);
                    possibleNextNodes.Add(currentNodeIndex - size.y);
                }
            }
            if (currentNodeY < size.y - 1)
            {
                // Check node above the current node
                if (!completedNodes.Contains(nodes[currentNodeIndex + 1]) &&
                    !currentPath.Contains(nodes[currentNodeIndex + 1]))
                {
                    possibleDirections.Add(3);
                    possibleNextNodes.Add(currentNodeIndex + 1);
                }
            }
            if (currentNodeY > 0)
            {
                // Check node below the current node
                if (!completedNodes.Contains(nodes[currentNodeIndex - 1]) &&
                    !currentPath.Contains(nodes[currentNodeIndex - 1]))
                {
                    possibleDirections.Add(4);
                    possibleNextNodes.Add(currentNodeIndex - 1);
                }
            }

            // Choose next node
            if (possibleDirections.Count > 0)
            {
                int chosenDirection = UnityEngine.Random.Range(0, possibleDirections.Count);
                MazeCell chosenNode = nodes[possibleNextNodes[chosenDirection]];



                switch (possibleDirections[chosenDirection])
                {
                    case 1:
                        chosenNode.RemoveWall(1);
                        currentPath[currentPath.Count - 1].RemoveWall(0);
                        break;
                    case 2:
                        chosenNode.RemoveWall(0);
                        currentPath[currentPath.Count - 1].RemoveWall(1);
                        break;
                    case 3:
                        chosenNode.RemoveWall(3);
                        currentPath[currentPath.Count - 1].RemoveWall(2);
                        break;
                    case 4:
                        chosenNode.RemoveWall(2);
                        currentPath[currentPath.Count - 1].RemoveWall(3);
                        break;
                }

                currentPath.Add(chosenNode);
            }
            else
            {
                completedNodes.Add(currentPath[currentPath.Count - 1]);

                currentPath.RemoveAt(currentPath.Count - 1);
            }
        }

    }

    private void DisableValls(Vector2Int size)
    {
        List<MazeCell> deactivatedWalls = new List<MazeCell>();
        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                int index = x * size.y + y;
                MazeCell cell = nodes[index];

                // Check each direction
                for (int dir = 0; dir < 4; dir++)
                {
                    // Randomly skip with some chance
                    if (UnityEngine.Random.value < removeChance)
                    {
                        // Get neighboring cell based on direction
                        int neighborX = x, neighborY = y;

                        switch (dir)
                        {
                            case 0: neighborX -= 1; break; // Left
                            case 1: neighborX += 1; break; // Right
                            case 2: neighborY -= 1; break; // Down
                            case 3: neighborY += 1; break; // Up
                        }

                        // Ensure the neighbor is inside the grid
                        if (neighborX >= 0 && neighborX < size.x && neighborY >= 0 && neighborY < size.y)
                        {
                            int neighborIndex = neighborX * size.y + neighborY;
                            MazeCell neighbor = nodes[neighborIndex];
                            // Remove wall between current and neighbor
                            cell.DeactivateWall(dir);
                            // Remove opposite wall on neighbor
                            neighbor.DeactivateWall((dir + 2) % 4);
                        }
                    }
                }
            }
        }
    }
}
