using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class MazeCell : MonoBehaviour
{
    [SerializeField] GameObject[] _walls;
    [SerializeField] GameObject floor;
    
    public void RemoveWall(int wallIndex)
    {
        WallMenager.RemoveNode(_walls[wallIndex]);
        Destroy(_walls[wallIndex].gameObject);
        WallMenager.RemoveNode(_walls[wallIndex]);
    }
    public void DeactivateWall(int dir)
    {
            WallMenager.AddNode(_walls[dir]);
            _walls[dir].SetActive(false);
    }
}

