using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMenager : MonoBehaviour
{
    int round;
    private static List<GameObject> node = new List<GameObject>();
    public static List<GameObject> nodeFloor = new List<GameObject>();
    [SerializeField] MazeGenerator mazeGen;

    public static void AddNode(GameObject cell)
    {
        node.Add(cell);
    }
    public static void AddFloor(GameObject cell)
    {
        nodeFloor.Add(cell);
    }
    public static void RemoveNode(GameObject cell)
    {
        if(node.Contains(cell))
        {
            node.Remove(cell);
        }
    }

    private void Update()
    {
    }
    private void Start()
    {
    }
}
