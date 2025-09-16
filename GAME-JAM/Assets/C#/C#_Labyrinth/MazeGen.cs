using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    [SerializeField] MazeCell nodePrefab;
    [SerializeField] Vector2Int mazeSize;
    [SerializeField] GameObject badGuy;
    [SerializeField] GameObject[] noFloorEnemies;

    static int enemyCount;

    public static List<MazeCell> nodes = new List<MazeCell>();
    private List<MazeCell> occupiedCells = new List<MazeCell>();
    int i = 0;

    private void Awake()
    {
        GenerateMazeInstant(mazeSize);
        occupiedCells.Add(nodes[1]);
        occupiedCells.Add(nodes[1 + mazeSize.y]);
        occupiedCells.Add(nodes[1 + mazeSize.x]);
        SpawnEnemies();
    }
    void SpawnEnemies()
    {
        for(int i = 0; i < nodes.Count - 1;  i++)
        {
            Vector3 spawnPoint = new Vector3(nodes[i].transform.position.x, nodes[i].transform.position.y, 0);
            if(UnityEngine.Random.value < 0.10f && nodes[i].HasFloor(3) && !occupiedCells.Contains(nodes[i]))
            {
                Instantiate(badGuy, spawnPoint, Quaternion.identity);
                occupiedCells.Add(nodes[i]);
                enemyCount++;
            }
            else if (UnityEngine.Random.value < 0.15f && !occupiedCells.Contains(nodes[i]))
            {
                Instantiate(noFloorEnemies[UnityEngine.Random.Range(0, noFloorEnemies.Length)], spawnPoint, Quaternion.identity);
                occupiedCells.Add(nodes[i]);
                enemyCount++;
            }
        }
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

            MazeCell lastCell = nodes[currentNodeIndex];
            

            if (possibleDirections.Count == 0 && i == 0)             //check if dead end
            {
                if (lastCell.HasFloor(3))                           //check if dead end has floor 
                {
                    i++;
                    Vector3 deadEnd = new Vector3(nodes[currentNodeIndex].transform.position.x, nodes[currentNodeIndex].transform.position.y, 0);
                    Instantiate(badGuy, deadEnd, Quaternion.identity);
                    occupiedCells.Add(lastCell);



                }
            }
            else if (possibleDirections.Count > 0)
                i = 0;
        }

    }
}
