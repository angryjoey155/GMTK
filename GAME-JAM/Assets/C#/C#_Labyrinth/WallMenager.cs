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
    
    public void Button()
    {
        for (int i = 0; i <= node.Count - 1; i++)
        {
            if (node[i] != null)
            {
                node[i].SetActive(true);
                node.Remove(node[i]);
            }
            else
                node.Remove(node[i]);
        }
        Debug.Log(node.Count);

    }

    private void Update()
    {
        Debug.Log(nodeFloor.Count);
    }
    private void Start()
    {
    }
}
