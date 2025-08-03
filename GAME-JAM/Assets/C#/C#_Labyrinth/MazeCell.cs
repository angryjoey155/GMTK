using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.UI;
using UnityEngine;

public class MazeCell : MonoBehaviour
{
    [SerializeField] GameObject[] _walls;
    [SerializeField] GameObject _ground;
    public void RemoveWall(int wallIndex)
    {
        Destroy(_walls[wallIndex].gameObject);
        _walls[wallIndex] = null;
        if (wallIndex == 3)
            _ground = null;
    }
    public bool HasFloor()
    {
        if (_ground == null) return false;
        else return true;
    }
}

